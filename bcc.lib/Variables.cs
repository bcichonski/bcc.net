using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcc.lib
{
    public class Variables : Dictionary<string, VariableInfo>
    {
        public int Counter { get; set; }

        public void Add(string name, TypeDescriptor type)
        {
            var vi = new VariableInfo() { IlNo = Counter++, Name = name, Type = type };
            this.Add(name, vi);
        }
    }
}
