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
        public RequestObject(string module, string cmd, object args)
        {
            this.Module = module;
            this.Cmd = cmd;
            this.args = args;
        }

        public string Module { get; set; }
        public string Cmd { get; set; }
        public object args { get; set; }
    }
}
