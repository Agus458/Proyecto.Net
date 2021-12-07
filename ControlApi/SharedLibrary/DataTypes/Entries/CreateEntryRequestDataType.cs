using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Entries
{
    public record CreateEntryRequestDataType
    {
        [Required]
        public Guid PersonId { get; init; }
    }
}
