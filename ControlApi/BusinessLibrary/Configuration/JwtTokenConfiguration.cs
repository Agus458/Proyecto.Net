using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Configuration
{
    public class JwtTokenConfiguration
    {
        public SecurityKey Key { get; set; }

        public JwtTokenConfiguration(string Key)
        {
            this.Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
        }
    }
}
