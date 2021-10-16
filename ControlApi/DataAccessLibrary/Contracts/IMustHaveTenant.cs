using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Contracts
{
    /// <summary>
    /// Allows specifing which entities will have to have the Tenant id in them.
    /// </summary>
    public abstract class IMustHaveTenant
    {
        /// <summary>
        /// The Tenant that the data will belong to.
        /// </summary>
        [ForeignKey("TenantId")]
        public Guid TenantId { get; set; }
    }
}
