using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Sweet.Net.Component
{
    public static class Json
    {
        //序列化
        public static string SerializeObject<T>(T obj)
        {
            var ser = new DataContractJsonSerializer(typeof(T));
            var stream = new MemoryStream();
            ser.WriteObject(stream, obj);
            var db = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(db, 0, (int)stream.Length);
            var dataString = Encoding.UTF8.GetString(db);
            return dataString;
        }

        //反序列化
        public static T DeserializeObject<T>(string json)
        {
            var ser = new DataContractJsonSerializer(typeof(T));
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)) {Position = 0};
            return (T)ser.ReadObject(stream);
        }

      
    }
}
