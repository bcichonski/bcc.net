using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcc.lib.AST
{
    public class TypeSpecifier : Node
    {
        public override void StepIn(IContext context)
        {
            var name = this.ParseNode.ChildNodes.First().Token.ValueString;
            this.NodeType = new TypeDescriptor(name);
            base.StepIn(context);
        }
    }
}
