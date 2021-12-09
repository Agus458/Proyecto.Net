using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Entities
{
    public class Room : MustHaveBuildingEntity
    {
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
