using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer
{
    public interface Idatabase
    {
        void Create(Person user);
        LinkedList<Person> Read();
        void Update(string login, string password);
        void Delete();

    }
    public interface ISerializeDao : Idatabase
    {
        string path { get; set; }
    }
}
