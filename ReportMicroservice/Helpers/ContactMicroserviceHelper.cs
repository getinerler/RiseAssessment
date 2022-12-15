using Newtonsoft.Json;
using ReportMicroservice.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;

namespace ReportMicroservice.Helpers
{
    public class ContactMicroserviceHelper
    {
        private readonly string url = "https://localhost:44375/PhoneBook/";

        public List<PhoneBookItem> GetPhoneBookItems(RabbitMqMessage message)
        {
            string info = Get($"{url}ReportInfo", "");
            List<PhoneBookItem> items = JsonConvert.DeserializeObject<List<PhoneBookItem>>(info);
            return items;
        }

        private string Get(string uri, string parameters)
        {
            using (var client = new HttpClient(new HttpClientHandler 
                { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri(uri);
                HttpResponseMessage response = client.GetAsync(parameters).Result;
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
        }
    }
}
