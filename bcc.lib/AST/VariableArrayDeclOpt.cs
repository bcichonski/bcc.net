using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcc.lib.AST
{
    public class VariableArrayDeclOpt : Node
    {
        public bool Present { get; set; }
        public override void StepOut(IContext context)
        {
            this.Present = this.ParseNode.ChildNodes.Count > 0;
            base.StepOut(context);
        }
    }
}
