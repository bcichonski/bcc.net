using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcc.lib
{
    public enum VariableType
    {
        @Int32,
        @Char
    }
    public class VariableInfo
    {
        public string Name { get; set; }
        public int IlNo { get; set; }
        public VariableType Type { get; set; }
    }
}
