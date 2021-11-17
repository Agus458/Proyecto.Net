using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Entities
{
    public class Assignment : BaseEntity
    {
        public Guid DoorId { get; set; }
        public Door Door { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
