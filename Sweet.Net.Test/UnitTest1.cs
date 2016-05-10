using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweet.Net.API;
using Sweet.Net.Component;
using Sweet.Net.Security;

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

        [TestMethod]
        public void TestMethodEncrypt()
        {
            //var res = AesEncrypt.Encrypt("你好世界你好世界你好世界你好世界你好世界你好世界你好世界你好世界");
            //var res2 = AesEncrypt.Decrypt(res);

            var jia = RsaEncrypt.Encrypt("Hello World");
            var jie = RsaEncrypt.Decrypt("r1naGY+ZV4Hq6i7rQGlpiAO3UD2WNv1Vp+8dMcZB22CeKaA7wMzQayfd7YFlbcVBqRONuu6YO7MaI5If+JZjUGzKMPMyH8/Bx20jaEjgOJOTfukRNXnF+CK62jMOWJhbdc7Z82vz0zvbJbJqdThduBMl8CegGA/NXFfLuTlk+/c=");
            Component.Logger.Error(jie);
            Assert.IsFalse(false);
        }

        [TestMethod]
        public void TestMethodLogger()
        {
            Logger.Error("helloError");
            Logger.Info("helloInfo");
            Logger.Warn("helloWarn");
        }

    }

}
