using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class RoomObj
    {
        public RoomObj()
        {
            clients = new List<string>();
        }

        public RoomObj(string Name)
        {
            this.Name = Name;
            clients = new List<string>();
        }
        public string Name { get; set; }
        public List<string> clients { get; set; }

        public override bool Equals(object obj)
        {
            RoomObj toCompare = obj as RoomObj;
            if(toCompare == null)
            {
                return false;
            }
            if(toCompare.Name != Name)
            {
                return false;
            }
            return true; 
        }
    }
}
