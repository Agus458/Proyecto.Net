using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Products
{
  public record ProductsDataType
    {
        public string nombre { set; get; }

        //Agregar una coleccion de precios
       // public List<DataPrecio>
    }
}
