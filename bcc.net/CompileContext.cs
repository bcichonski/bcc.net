using System;
using System.Collections.Generic;
using bcc.lib;
using System.Text;

namespace bcc.net
{
    internal class CompileContext : IContext
    {
        const string TAB = "    ";
        public Dictionary<string, object> Cache { get; private set; } 
            = new Dictionary<string, object>();

        public StringBuilder Code { get; private set; } = new StringBuilder();
        public bool IgnoreEmit { get; set; } = false;

        public void Emit(string label = "", string opcode = "", string comment = "")
        {
            if (IgnoreEmit)
                return;
            if (string.IsNullOrWhiteSpace(label))
                label = TAB;
            if (!string.IsNullOrWhiteSpace(comment))
                comment = "//" + comment;
            Code.AppendLine($"{label,-8}{opcode,-32}{comment}");
        }
    }
}