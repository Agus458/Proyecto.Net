using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Configuration.FacePlusPlus.Returns
{
    public class FaceRectangle
    {
        public int top { get; set; }
        public int left { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Face
    {
        public string face_token { get; set; }
        public FaceRectangle face_rectangle { get; set; }
    }

    public class DetectResponse
    {
        public string request_id { get; set; }
        public int time_used { get; set; }
        public List<Face> faces { get; set; }
        public string image_id { get; set; }
        public int face_num { get; set; }
        public string error_message { get; set; }
    }
}
