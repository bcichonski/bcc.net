﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcc.lib.AST
{
    public class Relation : Node
    {
        public override void StepOut(IContext context)
        {
            if (this.ParseNode.ChildNodes.Count > 1)
            {
                var oper = this.ParseNode.ChildNodes.First(c => c.Term.Name == "<" ||
                    c.Term.Name == ">"
                );
                if (oper.Term.Name == "<")
                {
                    context.Emit(opcode: "clt");
                }
                else
                    if (oper.Term.Name == ">")
                {
                    context.Emit(opcode: "cgt");
                }
                NodeType = VariableType.Int32;
            } else SetNodeTypeFromChildren();
            base.StepOut(context);
        }
    }
}
