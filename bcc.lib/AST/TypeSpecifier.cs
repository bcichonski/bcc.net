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
            context.Cache["currtype"] = VarType(name);
            base.StepIn(context);
        }

        public static string IlType(VariableType type)
        {
            if (type == VariableType.Char)
                return "char";
            if (type == VariableType.Int32)
                return "int32";
            throw new ArgumentException("Unknown type " + type);
        }

        public static VariableType VarType(string type)
        {
            if (type == "char")
                return VariableType.Char;
            if (type == "int")
                return VariableType.Int32;
            throw new ArgumentException("Unknown type " + type);
        }
    }
}
