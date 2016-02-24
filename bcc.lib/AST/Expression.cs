using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcc.lib.AST
{
    public class Expression : Node
    {
        private bool arroptpresent;
        string currName;
        VariableInfo arrayVar;

        public override void StepIn(IContext context)
        {
            if (this.ParseNode.ChildNodes.Count == 4)
            {
                currName = this.ParseNode.ChildNodes.First().Token.ValueString;
                var arropt = this.ParseNode.ChildNodes.First(c => c.Term.Name == "VariableArrayDeclOpt");
                arroptpresent = arropt.ChildNodes.Count > 0;
                if (arroptpresent)
                {
                    var vars = (Variables)context.Cache["vars"];
                    if (vars.TryGetValue(currName, out arrayVar))
                    {
                        context.Emit(opcode: $"ldloc {arrayVar.IlNo}", comment: $"variable array {arrayVar.Name}");
                    }
                    else throw new Exception($"Array {currName} missing it's declaration.");
                }
            }
            base.StepIn(context);
        }

        public override void StepOut(IContext context)
        {
            if (this.ParseNode.ChildNodes.Count == 4)
            {
                if (arroptpresent)
                {
                    string stelem = ((ArrayTypeDescriptor)arrayVar.Type).ArrayElemSuffix;
                    context.Emit(opcode: $"stelem.{stelem}", comment: $"store elem of {arrayVar.Name}");
                }
                else
                    StoreVariableValue(context, currName);
            }
            else
            {
                SetNodeTypeFromChildren();
            }
            base.StepOut(context);
        }
    }
}
