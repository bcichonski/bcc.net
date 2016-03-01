using Irony.Parsing;
using bcc.lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bcc.lib.AST;

namespace bcc.net
{
    class Program
    {
        static void Main(string[] args)
        {
            var fullfilepath = args.LastOrDefault();
            var filename = Path.GetFileNameWithoutExtension(fullfilepath);
            var ast = Parse(fullfilepath);
            Compile(ast, filename, fullfilepath);
        }

        public static ParseTree Parse(string file)
        {
            var source = File.ReadAllText(file);
            BartqCGrammar grammar = new BartqCGrammar();
            Parser parser = new Parser(grammar);
            var res = parser.Parse(source, Path.GetFileName(file));
            return res;
        }

        public static void Compile(ParseTree ast, string filename, string full)
        {
            Node root = null;
            if (ast!=null && ast.Root!=null)
              root = (Node)ast.Root.AstNode;
            if (root != null)
            {
                var context = new CompileContext();
                context.Cache["filename"] = filename;
                context.Cache["vars"] = new Variables();
                context.Cache["labels"] = 0;
                root.Visit(context);
                File.WriteAllText(Path.ChangeExtension(full, "il"), context.Code.ToString());
            } else
            {
                foreach(var msg in ast.ParserMessages)
                {
                    Console.Error.WriteLine("Error " + msg.ParserState + " at line " + msg.Location.Line + " column " + msg.Location.Column);
                    Console.Error.WriteLine(msg.Message);
                }
                Console.ReadKey();
            }
        }
    }
}
