using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcc.lib.AST
{
    public class Factor : Node
    {
        public override void StepOut(IContext context)
        {
            if (this.ParseNode.ChildNodes.Count > 1)
            {
                var child = this.ParseNode.ChildNodes.First();
                if(child.Term.Name=="-")
                    context.Emit(opcode: "neg");
                else if (child.Term.Name == "!")
                {
                    context.Emit(opcode: "ldc.i4.0");
                    context.Emit(opcode: "ceq");
                }
                this.NodeType = ((Node)this.ParseNode.ChildNodes.Last().AstNode).NodeType;
            } else
            {
                this.NodeType = ((Node)this.ParseNode.ChildNodes.First().AstNode).NodeType;
            }
            base.StepOut(context);
        }
    }
}
