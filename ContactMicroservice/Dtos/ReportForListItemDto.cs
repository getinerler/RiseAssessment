using Newtonsoft.Json;
using System;

namespace ContactMicroservice.Dtos
{
    public class ReportForListItemDto
    {
        [JsonProperty("guid")]
        public Guid Guid { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }
    }
}
