using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableNative
{
    public class Person: RealmObject
    {
        public Person()
        {
        }

        public Person(int id, string Fname, string LName, int Age)
        {
            this.id = id;
            this.FirstName = Fname;
            this.LastName = LName;
            this.Age = Age;
        }
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
