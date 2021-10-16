using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Entities
{
    /// <summary>
    /// Provides all the properties that are repeated across all the enities in the system.
    /// </summary>
    public abstract class BaseEntity
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

        public Guid TenantId { get; set; }
    }
}
