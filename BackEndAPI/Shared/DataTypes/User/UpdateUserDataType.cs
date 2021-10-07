using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTypes.User
{
    public record UpdateUserDataType
    {
        [EmailAddress]
        public string Email { get; init; }

        public string UserName { get; init; }

        public string Password { get; init; }
    }
}
