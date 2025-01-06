using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dagnys_api.Entities
{
    public class SupplierRawMaterial
    {
        
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public int RawMaterialId { get; set; }
        public RawMaterial RawMaterial { get; set; }
        public float PricePerKg { get; set; }
    }
    }
