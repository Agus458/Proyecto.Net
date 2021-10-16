using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Institutions
{
    public record CreateInstitutionRequestDataType
    {
        [Required]
        [MaxLength(12)]
        public string Rut { get; init; }

        [Required]
        [MaxLength(200)]
        public string SocialReason { get; init; }

        [Required]
        [EmailAddress]
        public string Email { get; init; }

        [Required]
        public string Password { get; init; }
    }
}
