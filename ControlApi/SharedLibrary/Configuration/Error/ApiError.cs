using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Error
{
    public class ApiError : Exception
    {
        public int StatusCode { get; set; }

        public ApiError(string Message, int StatusCode = 500) : base(Message)
        {
            this.StatusCode = StatusCode;
        }
    }
}
