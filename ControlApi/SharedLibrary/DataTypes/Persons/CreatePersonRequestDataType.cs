using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Persons
{
    public record CreatePersonRequestDataType
    {
        /// <summary>
        /// Document of the document of the person.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Document { get; init; }

        /// <summary>
        /// Type of the document of the person.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string DocumentType { get; init; }

        /// <summary>
        /// Name of the person.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Name { get; init; }

        /// <summary>
        /// Last Name of the person.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string LastName { get; init; }

        /// <summary>
        /// Phone of the person.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Phone { get; init; }

        /// <summary>
        /// Email of the person.
        /// </summary>
        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; init; }

        public IFormFile FileImage { get; init; }
    }
}
