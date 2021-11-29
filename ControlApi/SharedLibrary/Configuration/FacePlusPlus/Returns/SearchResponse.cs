using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Configuration.FacePlusPlus.Returns
{
    public class Thresholds
    {
        [JsonProperty("1e-3")]
        public double _1e3 { get; set; }

        [JsonProperty("1e-5")]
        public double _1e5 { get; set; }

        [JsonProperty("1e-4")]
        public double _1e4 { get; set; }
    }

    public class Result
    {
        public double confidence { get; set; }
        public string user_id { get; set; }
        public string face_token { get; set; }
    }

    public class SearchResponse
    {
        public string request_id { get; set; }
        public int time_used { get; set; }
        public Thresholds thresholds { get; set; }
        public List<Result> results { get; set; }
        public string error_message { get; set; }
    }
}
