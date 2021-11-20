using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DataTypes
{
    public class PaginationDataType<T>
    {
        public IEnumerable<T> Collection { get; set; }
        public int Size { get; set; }
    }
}
