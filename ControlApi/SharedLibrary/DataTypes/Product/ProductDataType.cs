using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Product
{
    public record ProductDataType
    {
        public Guid Id { get; init; }
        public string Nombre{ get; init; }

      //  public IList<>

    }
}
