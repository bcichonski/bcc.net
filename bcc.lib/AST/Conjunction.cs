using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcc.lib.AST
{
    public class Conjunction : Node
    {
        public override void StepOut(IContext context)
        {
            if (this.ParseNode.ChildNodes.Count > 1)
            {
                var oper = this.ParseNode.ChildNodes.First(c => c.Term.Name == "&&");
                context.Emit(opcode: "and");
                NodeType = VariableType.Int32;
            } else SetNodeTypeFromChildren();
            base.StepOut(context);
        }
    }
}
