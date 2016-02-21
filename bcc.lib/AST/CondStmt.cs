using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcc.lib.AST
{
    public class CondStmt : Node
    {
        private int counter;
        private bool elsePresent;
        public override void BeforeVisitChild(IContext context, Node child)
        {
            counter = (int)context.Cache["labels"];
            if (child.ParseNode.Term.Name == "Stmt")
            {
                counter++;
                context.Cache["labels"] = counter;
                context.Emit(opcode: $"brfalse Label{counter}", comment: "if then");
            }
            else if (child.ParseNode.Term.Name == "ElseStmtOpt")
            {
                
                elsePresent = child.ParseNode.ChildNodes.Count > 0;
                var currCounter = counter;
                if (elsePresent)
                {
                    context.Emit(opcode: $"br Label{++counter}");
                }
                context.Emit(label: $"Label{currCounter}:", opcode: "nop", comment: "else");
                counter++;
                context.Cache["labels"] = counter;
            }
            base.BeforeVisitChild(context, child);
        }

        public override void StepOut(IContext context)
        {
            if (elsePresent)
            {
                counter = (int)context.Cache["labels"];
                context.Emit(label: $"Label{counter - 1}:", opcode: "nop", comment: "end if");
            }
            base.StepOut(context);
        }
    }
}
