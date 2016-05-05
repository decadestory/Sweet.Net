using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sweet.Net.Component
{
    public static class Lbs
    {
         /// <summary>
        /// 根据ip获取地址长串
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static BaiduLocation GetLocationByIp(string ip)
        {
            var url = "http://api.map.baidu.com/location/ip?ak=F454f8a5efe5e577997931cc01de3974&ip=" + ip + "&coor=bd09l";
            var wc = new System.Net.WebClient { Encoding = Encoding.Default };

            var json = wc.DownloadString(url);

            return Json.DeserializeObject<BaiduLocation>(json);

        }
    }

    #region 返回地理位置类
    [DataContract]
    public class BaiduLocation
    {

        [DataMember(Name = "address")]
        public string Address { get; set; }

        [DataMember(Name = "content")]
        public Content Content { get; set; }

        [DataMember(Name = "status")]
        public int Status { get; set; }
    }

    [DataContract]
    public class Content
    {
        [DataMember(Name = "address")]
        public string Address { get; set; }

        [DataMember(Name = "address_detail")]
        public AddressDetail AddressDetail { get; set; }

        [DataMember(Name = "point")]
        public Point Point { get; set; }
    }

    [DataContract]
    public class AddressDetail
    {

        [DataMember(Name = "city")]
        public string City { get; set; }

        [DataMember(Name = "city_code")]
        public int CityCode { get; set; }

        [DataMember(Name = "district")]
        public string District { get; set; }

        [DataMember(Name = "province")]
        public string Province { get; set; }

        [DataMember(Name = "street")]
        public string Street { get; set; }

        [DataMember(Name = "street_number")]
        public string StreetNumber { get; set; }
    }
   
    [DataContract]
    public class Point
    {

        [DataMember(Name = "x")]
        public string X { get; set; }

        [DataMember(Name = "y")]
        public string Y { get; set; }
    }
    #endregion

}
