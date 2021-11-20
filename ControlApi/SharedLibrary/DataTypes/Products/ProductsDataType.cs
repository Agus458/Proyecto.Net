using SharedLibrary.DataTypes.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes.Products
{
  public record ProductsDataType
    {
        public Guid Id { get; init; }
        public string nombre { set; get; }

        public float precio { set; get; }

        public BuildingDataType Building { get; init; }
        //Agregar una coleccion de precios
        // public List<DataPrecio>
    }
}
