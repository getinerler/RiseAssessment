using Newtonsoft.Json;
using ReportMicroservice.Dtos;
using System.IO;
using System.Net;

namespace ReportMicroservice.Helpers
{
    public class ContactMicroserviceHelper
    {
        private readonly string url = "localhost:3000/ContactMicroservice/";

        public ContactMicroserviceReportItem[] GetInfo()
        {
            string info = Get(url + "/GetReportInfo");

            ContactMicroserviceReportItem[] items =
                JsonConvert.DeserializeObject<ContactMicroserviceReportItem[]>(info);
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
