using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace HelloEEG
{
    public class Light
    {
        public static string BridgeIP = "192.168.0.16";
        public static string Usercode;

        private const string APIAddressTemplate = "http://{0}/api";
        private const string BodyTemplate = "{{\"devicetype\":\"{0}\"}}";
    


        public static string ConnectBridge(string username)
        {
            return PostRequestToBridge(
                 string.Format(APIAddressTemplate, BridgeIP),
                 string.Format(BodyTemplate, username));
        }

        public static string PostRequestToBridge(string uri, string data, string contentType = "application/json", string method = "POST")
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.ContentLength = dataBytes.Length;
            request.ContentType = contentType;
            request.Method = method;

            using (Stream requestBody = request.GetRequestStream())
            {
                requestBody.Write(dataBytes, 0, dataBytes.Length);
            }

            //search for easier way
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string json = reader.ReadToEnd();

                var result = ResultHelper.FromJson(json);
                Usercode = result.First().Success.Username;

                return json;
            }

        }

        public static string GetRequestToBridge(string fullUrl)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fullUrl);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }
        public static void PutRequestToBridge(string fullUri, string data, string contentType = "application/json", string method = "PUT")
        {
            using (var client = new System.Net.WebClient())
            {
                client.UploadData(fullUri, method, Encoding.UTF8.GetBytes(data));
            }
        }

        
    }
    public partial class ResultHelper
    {
        [JsonProperty("success")]
        public Success Success { get; set; }
    }

    public partial class Success
    {
        [JsonProperty("username")]
        public string Username { get; set; }
    }

    public partial class ResultHelper
    {
        public static ResultHelper[] FromJson(string json) => JsonConvert.DeserializeObject<ResultHelper[]>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this ResultHelper[] self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}

