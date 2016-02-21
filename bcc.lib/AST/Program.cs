using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcc.lib.AST
{
    public class Program : Node
    {
        public override void StepIn(IContext context)
        {
            var programName = (string)context.Cache["filename"];
            var id = this.ParseNode.ChildNodes.First(v => v.Term is IdentifierTerminal);
            var mainMethodName = id.Token.ValueString;

            context.Emit(label: ".assembly extern mscorlib {}");
            context.Emit(label: $".assembly {programName} {{ }}");
            context.Emit(label: $".module {programName}.exe");
            context.Emit(label: $".method static int32 {mainMethodName}() cil managed");
            context.Emit(label: "{");
            context.Emit(opcode: ".entrypoint");
            base.StepIn(context);
        }

        public override void StepOut(IContext context)
        {
            context.Emit(label: "}");
            base.StepOut(context);
        }
    }
}
