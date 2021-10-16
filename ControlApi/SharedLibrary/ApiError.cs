using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
    public class ApiError
    {
        public dynamic Description { get; set; }

        public ApiError(dynamic Description)
        {
            this.Description = Description;
        }
    }
}
