using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcc.lib.AST
{
    public class CondStmt : Node
    {
        private int ifLabel;
        private int elseLabel;
        private bool elsePresent;

        public override void BeforeVisitChild(IContext context, Node child)
        {
            var counter = (int)context.Cache["labels"];
            if (child.ParseNode.Term.Name == "Stmt")
            {
                ifLabel = counter++;               
                context.Emit(opcode: $"brfalse Label{ifLabel}", comment: "if then");
            }
            else if (child.ParseNode.Term.Name == "ElseStmtOpt")
            {
                
                elsePresent = child.ParseNode.ChildNodes.Count > 0;                
                if (elsePresent)
                {
                    elseLabel = counter++;
                    context.Emit(opcode: $"br Label{elseLabel}");
                    context.Emit(label: $"Label{ifLabel}:", opcode: "nop", comment: "else");
                }               
            }
            context.Cache["labels"] = counter;
            base.BeforeVisitChild(context, child);
        }

        public override void StepOut(IContext context)
        {
            if (elsePresent)
            {
                context.Emit(label: $"Label{elseLabel}:", opcode: "nop", comment: "end if");
            } else
            {
                context.Emit(label: $"Label{ifLabel}:", opcode: "nop", comment: "end if");
            }
            base.StepOut(context);
        }
    }
}
