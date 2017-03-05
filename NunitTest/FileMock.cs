using AuthServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace NunitTest
{
    public class FileMock : Idatabase
    {
        LinkedList<Person> pp = null;
        public FileMock()
        {
            pp = new LinkedList<Person>();
            pp.AddLast(new Person("1111", "1111", "bel1@mail.ru"));
            pp.AddLast(new Person("2222", "2222", "bel2@mail.ru"));
            pp.AddLast(new Person("3333", "3333", "bel3@mail.ru"));
            pp.AddLast(new Person("4444", "4444", "bel4@mail.ru"));
            pp.AddLast(new Person("5555", "5555", "bel5@mail.ru"));
            pp.AddLast(new Person("6666", "6666", "bel6@mail.ru"));
            pp.AddLast(new Person("7777", "7777", "bel7@mail.ru"));
            pp.AddLast(new Person("8888", "8888", "bel8@mail.ru"));
            pp.AddLast(new Person("9999", "9999", "bel9@mail.ru"));
            pp.AddLast(new Person("1111", "1111", "bel10@mail.ru"));
            pp.AddLast(new Person("2222", "2222", "bel11@mail.ru"));
        }
        public void Create(Person p)
        {
            pp.AddLast(new Person(p.name, p.password, p.email));
        }
        
        public LinkedList<Person> Read()
        {
            return pp;
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }
        public void Update(string login, string password)
        {
            throw new NotImplementedException();
        }
    }
}
