using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweet.Net.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var tt = Sweet.Net.Component.Lbs.GetLocationByIp("111.13.101.208");
            Assert.IsFalse(true);
        }
    }
}
