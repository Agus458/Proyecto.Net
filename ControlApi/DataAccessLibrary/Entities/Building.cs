using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Entities
{
    /// <summary>
    /// Building entity implementation.
    /// </summary>
    public class Building : BaseEntity
    {
        public string Latitude { get; set; }
        
        public string Length { get; set; }
    }
}
