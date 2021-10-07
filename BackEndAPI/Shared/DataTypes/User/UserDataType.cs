using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.DataTypes.User
{
    public record UserDataType
    {
        public Guid Id { get; init; }

        public string Email { get; init; }

        public string UserName { get; init; }
    }
}
