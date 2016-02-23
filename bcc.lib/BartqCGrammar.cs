using bcc.lib.AST;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcc.lib
{
    public class BartqCGrammar : Grammar
    {
        public BartqCGrammar()
        {
            // 1. Terminals
            var number = new NumberLiteral("number", NumberOptions.Default, typeof(Node));
            number.DefaultIntTypes = new TypeCode[] { TypeCode.Int32 };
            var identifier = new IdentifierTerminal("identifier");
            identifier.AstConfig.NodeType = typeof(Node);
            var comment = new CommentTerminal("comment", "//", "\n", "\r");
            comment.AstConfig.NodeType = typeof(Node);
            var charConst = new StringLiteral("CharConstant", "'", StringOptions.IsChar, typeof(Node));
            base.NonGrammarTerminals.Add(comment);

            // 2. Non-terminals
            var Primary = new NonTerminal("Primary", typeof(Primary));
            var Factor = new NonTerminal("Factor", typeof(Factor));
            var Term = new NonTerminal("Term", typeof(Term));
            var Sum = new NonTerminal("Sum", typeof(Sum));
            var Relation = new NonTerminal("Relation", typeof(Relation));
            var Comparision = new NonTerminal("Comparision", typeof(Comparision));
            var Conjunction = new NonTerminal("Conjunction", typeof(Conjunction));
            var Disjunction = new NonTerminal("Disjunction", typeof(Disjunction));
            var Expr = new NonTerminal("Expr", typeof(Expression));
            var WhileStmt = new NonTerminal("WhileStmt", typeof(WhileStmt));
            var CondStmt = new NonTerminal("CondStmt", typeof(CondStmt));
            var ElseStmtOpt = new NonTerminal("ElseStmtOpt", typeof(ElseStmtOpt));
            var Stmt = new NonTerminal("Stmt", typeof(Stmt));
            var VariableId = new NonTerminal("VariableId", typeof(VariableId));

            var VariableInitOpt = new NonTerminal("VariableInitOpt", typeof(VariableInitOpt));
            var VariableArrayDeclOpt = new NonTerminal("VariableArrayDeclOpt", typeof(VariableArrayDeclOpt));
            var VariableDeclList = new NonTerminal("VariableDeclList", typeof(VariableDeclList));
            var VariableDecl = new NonTerminal("VariableDecl", typeof(VariableDecl));
            var VariableDeclarations = new NonTerminal("VariableDeclarations", typeof(VariableDeclarations));
            var Statements = new NonTerminal("Statements", typeof(Statements));
            var CompoundStmt = new NonTerminal("CompoundStmt", typeof(Expression));
            var BlockStatement = new NonTerminal("BlockStatement", typeof(BlockStatement));
            //var ParamDecl = new NonTerminal("ParamDecl");
            //var ParamDeclList = new NonTerminal("ParamDeclList");
            var TypeSpecifier = new NonTerminal("TypeSpecifier", typeof(TypeSpecifier));
            var SmallCProgram = new NonTerminal("SmallCProgram", typeof(Program));

            //Rules
            this.Root = SmallCProgram;

            SmallCProgram.Rule = TypeSpecifier + identifier + "("
                //+ ParamDeclList 
                + ")" + BlockStatement;

            TypeSpecifier.Rule = ToTerm("char") | "int";

            //ParamDeclList.Rule = MakeStarRule(ParamDeclList, ToTerm(","), ParamDecl);

            //ParamDecl.Rule = TypeSpecifier + identifier;

            BlockStatement.Rule = "{" + CompoundStmt + "}";

            CompoundStmt.Rule = VariableDeclarations + Statements;

            VariableDeclarations.Rule = MakeStarRule(VariableDeclarations, VariableDecl);

            Statements.Rule = MakeStarRule(Statements, Stmt);

            VariableDecl.Rule = TypeSpecifier + VariableDeclList + ";";

            VariableDeclList.Rule = MakeStarRule(VariableDeclList, ToTerm(","), VariableId);

            VariableId.Rule = identifier + VariableArrayDeclOpt + VariableInitOpt;

            VariableInitOpt.Rule = "=" + Expr
                | this.Empty;

            VariableArrayDeclOpt.Rule = "[" + Expr + "]"
                | this.Empty;

            Stmt.Rule = //(ToTerm("break") + ";")
                //| (ToTerm("continue") + ";")
                (ToTerm("return") + Expr + ";")
                | (ToTerm("read") + "(" + identifier + ")" + ";")
                | (ToTerm("write") + "(" + Expr + ")" + ";")
                | CondStmt
                | WhileStmt
                | BlockStatement
                | Expr + ";";

            ElseStmtOpt.Rule = (ToTerm("else") + Stmt)
                | this.Empty;

            CondStmt.Rule = ToTerm("if") + "(" + Expr + ")" + Stmt + ElseStmtOpt;

            WhileStmt.Rule = ToTerm("while") + "(" + Expr + ")" + Stmt;

            Expr.Rule = (identifier + "=" + Expr)
                | Disjunction;

            Disjunction.Rule = Conjunction
                | (Disjunction + "||" + Conjunction);

            Conjunction.Rule = Comparision
                | (Conjunction + "&&" + Comparision);

            Comparision.Rule = Relation
                | Relation + "==" + Relation;

            Relation.Rule = Sum
                | Sum + "<" + Sum
                | Sum + ">" + Sum;

            Sum.Rule = (Sum + "+" + Term)
                | (Sum + "-" + Term)
                | Term;

            Term.Rule = (Term + "*" + Factor)
                | (Term + "/" + Factor)
                | (Term + "%" + Factor)
                | Factor;

            Factor.Rule = ("!" + Factor)
                | ("-" + Factor)
                | Primary;

            Primary.Rule = number
                | charConst
                | identifier
                | ("(" + Expr + ")");

            //Reserved words
            this.MarkReservedWords("break", "continue", "else",
            "if", "int", "char", "return", "while", "read", "write");

            this.LanguageFlags = LanguageFlags.CreateAst;
        }
    }
}
