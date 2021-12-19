using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Precio
{
    public record UpdatePreciosRequestDataType
    {
        public float Amount { get; init; }

        public DateTime ValidDate { get; init; }
    }
}
