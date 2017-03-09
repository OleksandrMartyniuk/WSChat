using AuthServer;
using Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NunitTest
{
    //public abstract class DataProviderBase
    //{
    //    public string[] sources = new string[6];
    //    public string[] Create = new string[6];
    //    public Person[] CreatePerson = new Person[6];
    //    public LinkedList<Person>[] Read = new LinkedList<Person>[6];
    //    public Person[] UpdPerson = new Person[6];
    //    public string[] Delete = new string[6];
    //    public Person[] DelPerson = new Person[6];
    //    string Temp;
    //    public ISerializeDao dao;
    //    protected void initLinkedList()
    //    {
    //        FileMock Mock = new FileMock();
    //        LinkedList<Person> pp = Mock.Read();
    //        Read[(int)FileCase.empty] = new LinkedList<Person>();
    //        Read[(int)FileCase.noFile] = new LinkedList<Person>();
    //        Read[(int)FileCase.noItems] = new LinkedList<Person>();
    //        Read[(int)FileCase.oneItem] = new LinkedList<Person>();
    //        Read[(int)FileCase.oneItem].AddFirst(pp.First.Value);
    //        Read[(int)FileCase.two] = new LinkedList<Person>();
    //        Read[(int)FileCase.two].AddLast(pp.First.Value);
    //        Read[(int)FileCase.two].AddLast(pp.First.Next.Value);
    //        Read[(int)FileCase.many] = new LinkedList<Person>(pp);
    //    }

    //    protected void initCreatePersons()
    //    {
    //        FileMock Mock = new FileMock();
    //        LinkedList<Person> pp = Mock.Read();
    //        CreatePerson[(int)FileCase.empty] = pp.First.Value;
    //        CreatePerson[(int)FileCase.noFile] = pp.First.Value;
    //        CreatePerson[(int)FileCase.noItems] = pp.First.Value;
    //        CreatePerson[(int)FileCase.oneItem] = pp.First.Next.Value;
    //        CreatePerson[(int)FileCase.two] = pp.First.Next.Next.Value;
    //        CreatePerson[(int)FileCase.many] = new Person("1", "2", "emp@mail.ru");
    //    }
    //    public string GetFileCopy(string path)
    //    {
    //        Temp = path.Insert(path.LastIndexOf('.') - 1, "-Copy");
    //        if (!File.Exists(path))
    //        {
    //            return path;
    //        }
    //        File.Copy(path, Temp);
    //        return Temp;
    //    }
    //    public void DeleteTempFile()
    //    {
    //        File.Delete(Temp);
    //    }
    //}
    //public enum FileCase
    //{
    //    noFile,
    //    empty,
    //    noItems,
    //    oneItem,
    //    two,
    //    many
    //}
    //public class FileProveder : DataProviderBase
    //{
    //    public FileProveder()
    //    {
    //        dao = new Filedb();
    //        initSources();
    //        initCreate();
    //        initCreatePersons();
    //        initDelete();   
    //        initLinkedList();
    //    }
    //    string dataFolder = @"\Data\";
    //    void initSources()
    //    {
    //        sources[(int)FileCase.noFile] = dataFolder + "NoFile.txt";
    //        sources[(int)FileCase.empty] = dataFolder + "Empty.txt";
    //        sources[(int)FileCase.noItems] = dataFolder + "EmptyMarkup.txt";
    //        sources[(int)FileCase.oneItem] = dataFolder + "One.txt";
    //        sources[(int)FileCase.two] = dataFolder + "Two.txt";
    //        sources[(int)FileCase.many] = dataFolder + "Many.txt";
    //    }
    //    void initCreate()
    //    {
    //        Create[(int)FileCase.noFile] = dataFolder + "One.txt";
    //        Create[(int)FileCase.empty] = dataFolder + "One.txt";
    //        Create[(int)FileCase.noItems] = dataFolder + "One.txt";
    //        Create[(int)FileCase.oneItem] = dataFolder + "Two.txt";
    //        Create[(int)FileCase.two] = dataFolder + @"create\Three.txt";
    //        Create[(int)FileCase.many] = dataFolder + @"create\Many.txt";
    //    }
      
    //    void initDelete()
    //    {
    //        Delete[(int)FileCase.noFile] = dataFolder + "EmptyMarkup.txt";
    //        Delete[(int)FileCase.empty] = dataFolder + "EmptyMarkup.txt";
    //        Delete[(int)FileCase.noItems] = dataFolder + "EmptyMarkup.txt";
    //        Delete[(int)FileCase.oneItem] = dataFolder + "EmptyMarkup.txt";
    //        Delete[(int)FileCase.two] = dataFolder + "One.txt";
    //        Delete[(int)FileCase.many] = dataFolder + @"delete\Many.txt";
    //    }
    //}
}
