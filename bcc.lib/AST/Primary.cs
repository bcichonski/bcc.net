using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcc.lib.AST
{
    public class Primary : Node
    {
        public override void StepOut(IContext context)
        {
            var child = this.ParseNode.ChildNodes.First();
            if (child.Term.Name == "number")
            {
                var val = Convert.ToInt32(this.ParseNode.ChildNodes.First().Token.Value);
                context.Emit(opcode: $"ldc.i4 {val}", comment: $"constant {val}");
                this.NodeType = VariableType.Int32;
            }
            else if (child.Term.Name == "CharConstant")
            {
                var val = Convert.ToChar(this.ParseNode.ChildNodes.First().Token.Value);
                var intval = Convert.ToByte(val);
                context.Emit(opcode: $"ldc.i4.s {intval}", comment: $"constant '{val}'");
                this.NodeType = VariableType.Char;
            }
            else if (child.Term.Name == "identifier")
            {
                var varid = this.ParseNode.ChildNodes.First().Token.ValueString;
                var vars = (Variables)context.Cache["vars"];
                VariableInfo vi;
                if (vars.TryGetValue(varid, out vi))
                {
                    context.Emit(opcode: $"ldloc {vi.IlNo}", comment: $"variable value {vi.Name}");
                    this.NodeType = vi.Type;
                }
                else
                    throw new ArgumentOutOfRangeException("Undeclared variable " + varid);
            }
            else
            if (child.Term.Name == "(")
            {
                //to jest expression
                SetNodeTypeFromChildren();
            }
            else
                throw new NotImplementedException();
            base.StepOut(context);
        }
    }
}
