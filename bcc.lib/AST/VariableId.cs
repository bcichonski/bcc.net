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
                
            } else
            {
                name = this.ParseNode.ChildNodes.First(c => c.Term.Name == "identifier").Token.ValueString;
            }
            context.Cache["currid"] = name;

            base.StepIn(context);
        }

        public override void StepOut(IContext context)
        {
            var vars = (Variables)context.Cache["vars"];
            var currType = (VariableType)context.Cache["currtype"];
            var currName = name;
            vars.Add(currName, currType);
            var iltype = TypeSpecifier.IlType(currType);
            context.Emit(opcode: $".locals init ({iltype})", comment: $"{currType} {currName}");

            var initopt = this.ParseNode.ChildNodes.FirstOrDefault(c => c.Term.Name == "VariableInitOpt");
            if (initopt != null)
            {
                var val = ((VariableInitOpt)initopt.AstNode).Present;
                if (val)
                {
                    if (vars.ContainsKey(currName))
                    {
                        var v = vars[currName];
                        context.Emit(opcode: $"stloc {v.IlNo}", comment: $"{currName}=");
                    }
                    else throw new ArgumentOutOfRangeException($"Unknown variable '{currName}'");
                }
                context.Cache.Remove("variableInitOpt");
            }
        }
    }
}
