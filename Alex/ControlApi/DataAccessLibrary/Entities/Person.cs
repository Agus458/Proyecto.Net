using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Entities
{
    /// <summary>
    /// Person entity implementation. The people that enter to the building of an institution.
    /// </summary>
    public class Person : MustHaveTenantEntity
    {
        /// <summary>
        /// Document of the person.
        /// </summary>
        [MaxLength(200)]
        public string Document { get; set; }

        /// <summary>
        /// Type of the document of the person.
        /// </summary>
        [MaxLength(200)]
        public string DocumentType { get; set; }

        /// <summary>
        /// Name of the person.
        /// </summary>
        [MaxLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// Last Name of the person.
        /// </summary>
        [MaxLength(200)]
        public string LastName { get; set; }

        /// <summary>
        /// Phone of the person.
        /// </summary>
        [MaxLength(200)]
        public string Phone { get; set; }

        /// <summary>
        /// Email of the person.
        /// </summary>
        [MaxLength(200)]
        public string Email { get; set; }
    }
}
