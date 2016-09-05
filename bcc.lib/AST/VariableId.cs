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
        bool arroptpresent = false;
        bool initoptpres = false;

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
            var arropt = this.ParseNode.ChildNodes.FirstOrDefault(c => c.Term.Name == "VariableArrayDeclOpt");
            if (arropt != null)
            {
                arroptpresent = arropt.ChildNodes.Count>0;
            }
            var initopt = this.ParseNode.ChildNodes.FirstOrDefault(c => c.Term.Name == "VariableInitOpt");
            if (initopt != null)
                initoptpres = initopt.ChildNodes.Count > 0;

            base.StepIn(context);
        }

        public override void BeforeVisitChild(IContext context, Node child)
        {
            base.BeforeVisitChild(context, child);
            if (child.ParseNode.Term.Name == "VariableArrayDeclOpt" && arroptpresent && initoptpres)
            {
                context.IgnoreEmit = true;
            }
        }

        public override void AfterVisitChild(IContext context, Node child)
        {
            base.AfterVisitChild(context, child);
            context.IgnoreEmit = false;
        }

        public override void StepOut(IContext context)
        {

            var vars = (Variables)context.Cache["vars"];
            var typeSpec = this.ParentNode.ParentNode.ParseNode.ChildNodes.First(c => c.Term.Name.EndsWith("TypeSpecifier"));
            var currType = ((Node)typeSpec.ChildNodes.First().AstNode).NodeType;
            var currName = name;

            if (arroptpresent)
                currType = new ArrayTypeDescriptor(currType);

            vars.Add(currName, currType);

            var iltype = currType.IlType;
            context.Emit(opcode: ".locals init ("+ iltype + ")", comment: ""+currType+" "+currName);

            if (arroptpresent && !initoptpres)
            {
                var newarrtype = ((ArrayTypeDescriptor)currType).NewArrType;
                context.Emit(opcode: "newarr "+ newarrtype);
                var v = vars[currName];
                context.Emit(opcode: "stloc "+ v.IlNo, comment: ""+currName+"=");
            }

            if (initoptpres)
            {
                if (currType is ArrayTypeDescriptor && currType.PrimitiveType != VariableType.Char)
                    throw new Exception("Currently initialization of array other than char is not allowed");

                if (vars.ContainsKey(currName))
                {
                    var v = vars[currName];
                    context.Emit(opcode: "stloc "+ v.IlNo, comment: "" + currName + "=");
                }
                else throw new ArgumentOutOfRangeException("Unknown variable '"+ currName + "'");
            }

        }
    }
}
