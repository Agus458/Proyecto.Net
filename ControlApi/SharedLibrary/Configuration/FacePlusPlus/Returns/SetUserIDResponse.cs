using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Configuration.FacePlusPlus.Returns
{
    public class SetUserIDResponse
    {
        public string user_id { get; set; }
        public string request_id { get; set; }
        public int time_used { get; set; }
        public string error_message { get; set; }
        public string face_token { get; set; }
    }
}
