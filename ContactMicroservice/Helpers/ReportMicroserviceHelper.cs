using ContactMicroservice.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace ContactMicroservice.Helpers
{
    public class ReportMicroserviceHelper
    {
        private readonly static string url = "https://localhost:44375/PhoneBook/";

        public static Guid RequestReport()
        {
            string response = Post(url);
            return Guid.Parse(response);
        }

        public static ReportInfoDto GetInfo(Guid guid)
        {
            string response = Get(url, "?guid=" + guid.ToString());
            ReportInfoDto info = JsonConvert.DeserializeObject<ReportInfoDto>(response);
            return info;
        }

        public static List<ReportForListItemDto> GetReports()
        {
            string response = Get($"{url}reports", string.Empty);
            List<ReportForListItemDto> info =
                JsonConvert.DeserializeObject<List<ReportForListItemDto>>(response);
            return info;
        }

        private static string Get(string uri, string parameters)
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

        private static string Post(string uri)
        {
            using (var client = new HttpClient(new HttpClientHandler
            { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri(uri);
                HttpResponseMessage response = client.PostAsync(uri, null).Result;
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
        }
    }
}
