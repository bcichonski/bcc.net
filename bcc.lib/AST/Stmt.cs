using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcc.lib.AST
{
    public class Stmt : Node
    {
        public override void StepOut(IContext context)
        {
            var first = this.ParseNode.ChildNodes.First();
            if (first.Term != null && first.Term.Name == "return")
            {
                context.Emit(opcode: "ret");
            }
            else
            if (first.Term != null && first.Term.Name == "write")
            {
                WriteStatement(context);
            }
            else
                if (first.Term != null && first.Term.Name == "read")
            {
                ReadStatement(context);
            }
            base.StepOut(context);
        }

        private void ReadStatement(IContext context)
        {
            var next = this.ParseNode.ChildNodes.FirstOrDefault(cn => cn.Term != null && cn.Term.Name == "identifier");
            if (next != null)
            {
                var varid = next.Token.ValueString;
                var vars = (Variables)context.Cache["vars"];
                VariableInfo vi;
                if (vars.TryGetValue(varid, out vi))
                {
                    if (vi.Type == VariableType.Char)
                    {
                        context.Emit(opcode: "call string [mscorlib]System.Console::ReadLine()");
                        context.Emit(opcode: "char [mscorlib]System.Convert::ToChar(string)");
                        StoreVariableValue(context, varid);
                    }
                    else if (vi.Type == VariableType.Int32)
                    {
                        context.Emit(opcode: "call string [mscorlib]System.Console::ReadLine()");
                        context.Emit(opcode: "call int32 [mscorlib]System.Int32::Parse(string)");
                        StoreVariableValue(context, varid);
                    }
                    else throw new ArgumentException("Internal error: untyped expression");
                }
            }
            else throw new ArgumentException("Expression to read not found");
        }

        private void WriteStatement(IContext context)
        {
            var next = this.ParseNode.ChildNodes.FirstOrDefault(cn => cn.Term != null && cn.Term.Name == "Expr");
            if (next != null)
            {
                var type = ((Node)next.AstNode).NodeType;
                if (type == VariableType.Char)
                    context.Emit(opcode: "call void [mscorlib]System.Console::Write(char)");
                else
                    if (type == VariableType.Int32)
                    context.Emit(opcode: "call void [mscorlib]System.Console::Write(int32)");
                else
                    if (type is ArrayTypeDescriptor)
                {
                    var arrtype = (ArrayTypeDescriptor)type;
                    if (arrtype.PrimitiveType == VariableType.Char)
                    {
                        context.Emit(opcode: "call void [mscorlib]System.Console::Write(char[])");
                    }
                    else
                    {
                        throw new ArgumentException("Internal error: only array of char can be written.");
                    }
                }
                else
                    throw new ArgumentException("Internal error: untyped expression");
            }
            else throw new ArgumentException("Expression to write not found");
        }
    }
}
