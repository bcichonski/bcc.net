using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcc.lib.AST
{
    public class WhileStmt : Node
    {
        private int whileTestLabel;
        private int whileEndLabel;

        public override void StepIn(IContext context)
        {
            var counter = (int)context.Cache["labels"];
            whileTestLabel = counter++;
            whileEndLabel = counter++;
            context.Emit(label:$"Label{whileTestLabel}", opcode: "nop", comment: "while");
            context.Cache["labels"] = counter;
            base.StepIn(context);
        }

        public override void AfterVisitChild(IContext context, Node child)
        {
            if (child.ParseNode.Term.Name == "Expr")
            {
                context.Emit(opcode: $"brfalse Label{whileEndLabel}", comment: "while");
            }
            base.BeforeVisitChild(context, child);
        }


        public override void StepOut(IContext context)
        {
            base.StepOut(context);
            context.Emit(opcode: $"br Label{whileTestLabel}", comment: "while");
            context.Emit(label: $"Label{whileEndLabel}", opcode: "nop", comment: "while");
        }
    }
}
