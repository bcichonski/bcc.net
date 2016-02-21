using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcc.lib.AST
{
    public class VariableInitOpt : Node
    {
        public override void StepOut(IContext context)
        {
            context.Cache["variableInitOpt"] = this.ParseNode.ChildNodes.Count>0;
            base.StepOut(context);
        }
    }
}
