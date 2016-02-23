using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcc.lib.AST
{
    public class VariableId : Node
    {
        private string name;

        public override void StepIn(IContext context)
        {
            if (this.ParseNode.Token != null)
            {
                name = this.ParseNode.Token.ValueString;

            }
            else
            {
                name = this.ParseNode.ChildNodes.First(c => c.Term.Name == "identifier").Token.ValueString;
            }
            context.Cache["currid"] = name;

            base.StepIn(context);
        }

        public override void StepOut(IContext context)
        {
            var arropt = this.ParseNode.ChildNodes.FirstOrDefault(c => c.Term.Name == "VariableArrayDeclOpt");
            var vars = (Variables)context.Cache["vars"];
            var typeSpec = this.ParentNode.ParentNode.ParseNode.ChildNodes.First(c => c.Term.Name == "TypeSpecifier");
            var currType = ((Node)typeSpec.AstNode).NodeType;
            var currName = name;
            bool arroptpresent = false;

            if (arropt != null)
            {
                arroptpresent = ((VariableArrayDeclOpt)arropt.AstNode).Present;
                if (arroptpresent)
                    currType = new ArrayTypeDescriptor(currType);
            }

            vars.Add(currName, currType);

            var iltype = currType.IlType;
            context.Emit(opcode: $".locals init ({iltype})", comment: $"{currType} {currName}");

            if (arroptpresent)
            {
                var newarrtype = ((ArrayTypeDescriptor)currType).NewArrType;
                context.Emit(opcode: $"newarr {newarrtype}");
                var v = vars[currName];
                context.Emit(opcode: $"stloc {v.IlNo}", comment: $"{currName}=");
            }

            var initopt = this.ParseNode.ChildNodes.FirstOrDefault(c => c.Term.Name == "VariableInitOpt");
            if (initopt != null)
            {
                var initoptpres = ((VariableInitOpt)initopt.AstNode).Present;
                if (initoptpres)
                {
                    if (currType is ArrayTypeDescriptor)
                        throw new Exception("Currently initialization of array is not allowed");

                    if (vars.ContainsKey(currName))
                    {
                        var v = vars[currName];
                        context.Emit(opcode: $"stloc {v.IlNo}", comment: $"{currName}=");
                    }
                    else throw new ArgumentOutOfRangeException($"Unknown variable '{currName}'");
                }
            }
        }
    }
}
