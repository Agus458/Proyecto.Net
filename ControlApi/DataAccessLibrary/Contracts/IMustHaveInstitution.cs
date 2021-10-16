using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Contracts
{
    /// <summary>
    /// Allows specifing which entities will have to have the Institution id in them.
    /// </summary>
    public abstract class IMustHaveInstitution
    {
        /// <summary>
        /// Identifies the Institution in the database to which the entity is part.
        /// </summary>
        [ForeignKey("InstitutionId")]
        public Guid InstitutionId { get; set; }
    }
}
