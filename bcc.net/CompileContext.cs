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

        public StringBuilder Code { get; private set; }
        public bool IgnoreEmit { get; set; }

        public CompileContext() {
          Code = new StringBuilder();
          Cache = new Dictionary<string, object>();
        }

        public void Emit(string label = "", string opcode = "", string comment = "")
        {
            if (IgnoreEmit)
                return;
            if (string.IsNullOrWhiteSpace(label))
                label = TAB;
            if (!string.IsNullOrWhiteSpace(comment))
                comment = "//" + comment;
            Code.AppendFormat("{0,-8}{1,-32}{2}\n",label,opcode,comment);
        }
    }
}