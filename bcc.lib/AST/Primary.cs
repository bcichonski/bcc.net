using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcc.lib.AST
{
    public class Primary : Node
    {
        private bool arroptpresent;
        string varid;
        VariableInfo arrayVar;

        public override void StepIn(IContext context)
        {
            var child = this.ParseNode.ChildNodes.First();
            if (child.Term.Name == "identifier")
            {
                var arropt = this.ParseNode.ChildNodes.First(c => c.Term.Name == "VariableArrayDeclOpt");
                varid = this.ParseNode.ChildNodes.First().Token.ValueString;
                arroptpresent = arropt.ChildNodes.Count > 0;
                if (arroptpresent)
                {
                    var vars = (Variables)context.Cache["vars"];
                    if (vars.TryGetValue(varid, out arrayVar))
                    {
                        context.Emit(opcode: $"ldloc {arrayVar.IlNo}", comment: $"variable array {arrayVar.Name}");
                    }
                    else throw new Exception($"Array {varid} missing it's declaration.");
                }
            }
            base.StepIn(context);
        }

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
            else if (child.Term.Name == "StringConstant")
            {
                var val = this.ParseNode.ChildNodes.First().Token.Value;
                context.Emit(opcode: $"ldstr \"{val}\"", comment: $"constant string");
                context.Emit(opcode: $"callvirt instance char[][mscorlib] System.String::ToCharArray()", comment: $"string to char array");

                this.NodeType = new ArrayTypeDescriptor(VariableType.Char);
            }
            else if (child.Term.Name == "identifier")
            {              
                var vars = (Variables)context.Cache["vars"];
                VariableInfo vi;
                if (vars.TryGetValue(varid, out vi))
                {
                    if (arroptpresent)
                    {
                        string ldelem = ((ArrayTypeDescriptor)arrayVar.Type).ArrayElemSuffix;
                        context.Emit(opcode: $"ldelem.{ldelem}", comment: $"load elem of {arrayVar.Name}");
                        this.NodeType = vi.Type.PrimitiveType;
                    }
                    else {
                        context.Emit(opcode: $"ldloc {vi.IlNo}", comment: $"variable value {vi.Name}");
                        this.NodeType = vi.Type;
                    }
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
