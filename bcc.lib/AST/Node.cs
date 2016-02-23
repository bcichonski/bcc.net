using System;
using Irony.Ast;
using Irony.Parsing;
using System.Linq;

namespace bcc.lib.AST
{
    public class Node : IAstNodeInit
    {
        protected internal ParseTreeNode ParseNode { get; set; }
        public TypeDescriptor NodeType { get; set; } = null;

        public virtual void Init(AstContext context, ParseTreeNode parseNode)
        {
            this.ParseNode = parseNode;
        }

        public void Visit(IContext context)
        {
            StepIn(context);
            if(ParseNode.ChildNodes?.Count > 0)
            {
                foreach(var childnode in ParseNode.ChildNodes)
                    if(childnode.AstNode != null)
                    {
                        Node astnode = (Node)childnode.AstNode;
                        BeforeVisitChild(context, astnode);
                        astnode.Visit(context);
                        AfterVisitChild(context, astnode);
                    }
            }
            StepOut(context);
        }

        public virtual void StepIn(IContext context) { }
        public virtual void StepOut(IContext context) { }
        public virtual void BeforeVisitChild(IContext context, Node child) { }
        public virtual void AfterVisitChild(IContext context, Node child) { }

        protected void SetNodeTypeFromChildren()
        {
            NodeType = ((Node)this.ParseNode.ChildNodes.First(cn => cn.AstNode!=null).AstNode).NodeType;
            if (!this.ParseNode.ChildNodes
                .Where(cn=>cn.AstNode != null)
                .Select(cn => cn.AstNode)
                .Cast<Node>()
                .All(n => n.NodeType == this.NodeType))
            {
                throw new Exception("Type mismatch");
            }
        }

        protected void StoreVariableValue(IContext context, string currName)
        {
            var vars = (Variables)context.Cache["vars"];
            if (vars.ContainsKey(currName))
            {
                var v = vars[currName];
                context.Emit(opcode: $"stloc {v.IlNo}", comment: $"{currName}=");
                NodeType = v.Type;
            }
            else throw new ArgumentOutOfRangeException($"Unknown variable '{currName}'");
        }
    }
}