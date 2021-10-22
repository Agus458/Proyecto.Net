using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Configuration.PayPal
{
    public class PayPalApiConfiguration
    {
        public string ApiAppName { get; set; }
        public string Account { get; set; }
        public string ClientID { get; set; }
        public string Secret { get; set; }
        public string UrlApi { get; set; }
        public string ReturnUrl { get; set; }
        public string CancelUrl { get; set; }
    }
}
