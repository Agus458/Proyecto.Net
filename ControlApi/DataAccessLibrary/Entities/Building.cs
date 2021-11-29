using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Entities
{
    /// <summary>
    /// Building entity implementation.
    /// </summary>
    public class Building : MustHaveTenantEntity
    {
        [MaxLength(200)]
        public string Name { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }
    }
}
