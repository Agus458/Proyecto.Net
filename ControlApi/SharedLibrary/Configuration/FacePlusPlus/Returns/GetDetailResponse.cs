using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Configuration.FacePlusPlus.Returns
{
    public class GetDetailResponse
    {
        public string faceset_token { get; set; }
        public string display_name { get; set; }
        public List<string> face_tokens { get; set; }
        public int time_used { get; set; }
        public string tags { get; set; }
        public string user_data { get; set; }
        public int face_count { get; set; }
        public string request_id { get; set; }
        public string error_message { get; set; }
        public string outer_id { get; set; }
    }
}
