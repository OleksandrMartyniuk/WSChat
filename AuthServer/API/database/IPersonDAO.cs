using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer
{
    public interface IPersonDAO
    {
        void Create(Person user);
        LinkedList<Person> Read();
        void Create(params string[] message);
    }
  
}
