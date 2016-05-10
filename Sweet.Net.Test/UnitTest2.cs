using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Sweet.Net.Component;
using System.Collections.Generic;

namespace Sweet.Net.Test
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            var list = new List<People>();
            for (int i = 0; i < 10000; i++)
                list.Add(new People { Id = i, Name = "Jerry" + i, Age = 10 + i });

            Excel.SaveToExcel<People>(list, "E:\\test.xlsx");
        }

        [TestMethod]
        public void TestMethodR()
        {
            var list = Excel.LoadFromExcel<People>("E:\\test.xls");
            Assert.IsTrue(true);
        }
    }

    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
