using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dagnys_api.ViewModels
{
    public class RawMaterialViewModel
    {
    public int Id { get; set; }
    public string Name { get; set; }
    public List<SupplierInfoViewModel> Suppliers { get; set; }
    }
}