using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Entities
{
    /// <summary>
    /// Institution Entity implementation.
    /// </summary>
    public class Tenant
    {
        /// <summary>
        /// Identifies the Entity in the table.
        /// </summary>
        [MaxLength(36)]
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// The date in which the new entity was created.
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; }

        /// <summary>
        /// The date in which the new entity was updated.
        /// </summary>
        public DateTimeOffset UpdatedDate { get; set; }

        /// <summary>
        /// The name that the Institution, "Tenant" will use to access to the system.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string SocialReason { get; set; }

        /// <summary>
        /// The rut of the Institution.
        /// </summary>
        [Required]
        [MaxLength(12)]
        public string Rut { get; set; }
    }
}
