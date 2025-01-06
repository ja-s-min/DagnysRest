using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dagnys_api.Entities
{
    public class Recipe
    {

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int RawMaterialId { get; set; }
        public float Quantity { get; set; }

    }
}