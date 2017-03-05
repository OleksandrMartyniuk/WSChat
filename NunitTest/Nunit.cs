using System;
using NUnit.Framework;
using AuthServer;
using System.IO;
using Core;
using System.Collections.Generic;
using System.Linq;

namespace NunitTest
{
    [TestFixture(typeof(Destop))]
    public class TestAuthentication<T> where T:DataProviderBase, new()
    {
        DataProviderBase dp = new T();

        [TearDown]
        public void Cleanup()
        {
            dp.DeleteTempFile();
        }

        [TestCase(new string[] { }, new string[] { }, TestName = "TestAuth")]
        [TestCase(new string[] { "0" }, new string[] { "0" }, TestName = "StringEnumerator2")]
        [TestCase(new string[] { "0", "1" }, new string[] { "0", "1" }, TestName = "StringEnumerator3")]
        public void TestAuth()
        {

        }

        string Temp;
        public string GetFileCopy(string path)
        {
            Temp = path.Insert(path.LastIndexOf('.') - 1, "-Copy");
            if (!File.Exists(path))
            {
                return path;
            }
            File.Copy(path, Temp);
            return Temp;
        }
        public void DeleteTempFile()
        {
            File.Delete(Temp);
        }

        [TestCase(FileCase.noFile)]
        [TestCase(FileCase.empty)]
        [TestCase(FileCase.noItems)]
        [TestCase(FileCase.oneItem)]
        [TestCase(FileCase.two)]
        [TestCase(FileCase.many)]
        public void Read(FileCase fcase)
        {
            string src = dp. sources[(int)fcase];
            LinkedList<Person> res = dp.Read[(int)fcase];
            dp.dao.path = dp.GetFileCopy(src);
            LinkedList<Person> act = dp.dao.Read();
            CollectionAssert.AreEqual(res.ToArray<Person>(), act.ToArray<Person>());
        }

        [TestCase(FileCase.noFile)]
        [TestCase(FileCase.empty)]
        [TestCase(FileCase.noItems)]
        [TestCase(FileCase.oneItem)]
        [TestCase(FileCase.two)]
        [TestCase(FileCase.many)]
        public void Create(FileCase fcase)
        {
            string src = dp.sources[(int)fcase];
            string exp = dp.Create[(int)fcase];
            Person p = dp.CreatePerson[(int)fcase];
            dp.dao.path = dp.GetFileCopy(src);
            dp.dao.Create(p);
            LinkedList<Person> act = dp.dao.Read();
            dp.dao.path = exp;
            LinkedList<Person> expList = dp.dao.Read();
            CollectionAssert.AreEqual(expList.ToArray<Person>(), act.ToArray<Person>());
        }
    }
}
