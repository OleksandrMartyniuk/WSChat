using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class RequestObject
    {
        public RequestObject()
        {

        }
        public RequestObject(string Module, string Cmd, object Args)
        {
            this.Module = Module;
            this.Cmd = Cmd;
            this.Args = Args;
        }

        public string Module { get; set; }
        public string Cmd { get; set; }
        public object Args { get; set; }
    }
}
