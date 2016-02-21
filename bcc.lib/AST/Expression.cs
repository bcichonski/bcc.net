using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcc.lib.AST
{
    public class Expression : Node
    {
        public override void StepOut(IContext context)
        {
            if(this.ParseNode.ChildNodes.Count==3)
            {
                var currName = this.ParseNode.ChildNodes.First().Token.ValueString;

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
