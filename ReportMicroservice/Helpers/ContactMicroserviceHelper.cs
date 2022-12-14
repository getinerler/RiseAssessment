using Newtonsoft.Json;
using ReportMicroservice.Dtos;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace ReportMicroservice.Helpers
{
    public class ContactMicroserviceHelper
    {
        private readonly string url = "localhost:3000/ContactMicroservice/";

        public List<PhoneBookItem> GetPhoneBookItems(RabbitMqMessage message)
        {
            string info = Get($"{url}/GetPhoneBookItems?country={message.Country}&city={message.City}");
            List<PhoneBookItem> items = JsonConvert.DeserializeObject<List<PhoneBookItem>>(info);
            return items;
        }

        private string Get(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
