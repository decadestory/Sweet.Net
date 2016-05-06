using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweet.Net.API;

namespace Sweet.Net.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var tt = Sweet.Net.Component.Lbs.GetLocationByIp("111.13.101.208");
            Assert.IsFalse(false);
        }

        [TestMethod]
        public void TestMethod2()
        {
            //var ttget = Sweet.Net.Net.Http.HttpPost("http://192.168.10.122:9900/Api/MqConf",);

            var param = new List<KeyValue>
            {
                new KeyValue {Key = "env", Value = "dev"}
            };

            var tt = Sweet.Net.Net.Http.HttpPost("http://192.168.10.122:9900/Api/MqConf", param);
            Assert.IsFalse(false);
        }
    }

}
