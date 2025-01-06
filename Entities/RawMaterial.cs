using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dagnys_api.Entities
{
    public class RawMaterial
    {
        public int Id { get; set; }
        public string Name { get; set; }
        

         public IList<SupplierRawMaterial> SupplierRawMaterials { get; set; } = new List<SupplierRawMaterial>();
      
    }
}