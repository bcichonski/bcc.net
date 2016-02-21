using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcc.lib
{
    public interface IContext
    {
        void Emit(string label = "", string opcode = "", string comment = "");

        Dictionary<string,object> Cache { get; }
    }
}
