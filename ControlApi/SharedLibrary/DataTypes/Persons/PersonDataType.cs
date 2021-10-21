using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Persons
{
    public record PersonDataType
    {
        /// <summary>
        /// Document of the document of the person.
        /// </summary>
        public string Document { get; set; }

        /// <summary>
        /// Type of the document of the person.
        /// </summary>
        public string DocumentType { get; set; }

        /// <summary>
        /// Name of the person.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Last Name of the person.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Phone of the person.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Email of the person.
        /// </summary>
        public string Email { get; set; }
    }
}
