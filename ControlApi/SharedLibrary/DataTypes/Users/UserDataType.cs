using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Users
{
    public record UserDataType
    {
        public string Id { get; init; }

        public string Email { get; init; }

        public string Name { get; init; }

        public string LastName { get; init; }

        public IList<string> Roles { get; set; }
    }
}
