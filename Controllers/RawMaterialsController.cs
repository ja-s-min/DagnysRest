
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dagnys_api.Data;
using dagnys_api.Entities;
using dagnys_api.ViewModels;

namespace dagnys_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RawMaterialsController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context; [HttpGet()]
        public async Task<ActionResult> GetAllRawMaterials()
        {
            var rawMaterials = await _context.RawMaterials
                .Include(r => r.SupplierRawMaterials)
                .ThenInclude(srm => srm.Supplier)
                .Select(r => new
                {
                    r.Name,
                    Suppliers = r.SupplierRawMaterials.Select(srm => new
                    {
                        srm.Supplier.SupplierName,
                        srm.PricePerKg
                    })
                })
                .ToListAsync();


            return Ok(new { success = true, statusCode = 200, data = rawMaterials });
         
        }


       
        [HttpGet("{id}")]
        public async Task<ActionResult> GetRawMaterialById(int id)
        {
            var rawMaterial = await _context.RawMaterials
                .Include(r => r.SupplierRawMaterials)
                .ThenInclude(srm => srm.Supplier)
                .Where(r => r.Id == id)
                .Select(r => new
                {
                    r.Name,
                    Suppliers = r.SupplierRawMaterials.Select(srm => new
                    {
                        srm.Supplier.SupplierName,
                        srm.PricePerKg
                    })
                })
                .FirstOrDefaultAsync();


            if (rawMaterial == null)
                return NotFound(new { success = false, statusCode = 404, message = $"Vi kunde ej hitta: {id}" });


            return Ok(new { success = true, statusCode = 200, data = rawMaterial });
        }


       
        [HttpGet("supplier/{supplierId}")]
        public async Task<ActionResult> GetProductsBySupplier(int supplierId)
        {
            var supplier = await _context.Suppliers
                .Include(s => s.SupplierRawMaterials)
                .ThenInclude(srm => srm.RawMaterial)
                .Where(s => s.SupplierId == supplierId)
                .Select(s => new
                {
                    s.SupplierName,
                    Products = s.SupplierRawMaterials.Select(srm => new
                    {
                        srm.RawMaterial.Name,
                        srm.PricePerKg
                    })
                })
                .FirstOrDefaultAsync();


            if (supplier == null)
                return NotFound();


            return Ok(supplier);
        }


       
        [HttpPost("add")]
        public async Task<ActionResult> AddRawMaterialToSupplier(int supplierId, string name, float pricePerKg)
        {
            var supplier = await _context.Suppliers.FindAsync(supplierId);
            if (supplier == null)
                return NotFound("Kunde ej hitta leverantören.");


            var rawMaterial = await _context.RawMaterials.FirstOrDefaultAsync(r => r.Name == name);
            if (rawMaterial == null)
            {
                rawMaterial = new RawMaterial { Name = name };
                await _context.RawMaterials.AddAsync(rawMaterial);
            }


            var supplierRawMaterial = new SupplierRawMaterial
            {
                Supplier = supplier,
                RawMaterial = rawMaterial,
                PricePerKg = pricePerKg
            };


            await _context.SupplierRawMaterials.AddAsync(supplierRawMaterial);
            await _context.SaveChangesAsync();


            return Ok("Råvaran har lagts till hos leverantören");
        }


       
        [HttpPatch("updateprice")]
        public async Task<ActionResult> UpdatePrice(int supplierId, int rawMaterialId, float newPrice)
        {
            var supplierRawMaterial = await _context.SupplierRawMaterials
                .FirstOrDefaultAsync(srm => srm.SupplierId == supplierId && srm.RawMaterialId == rawMaterialId);


            if (supplierRawMaterial == null)
                return NotFound("Kunde ej hitta råvaran");


            supplierRawMaterial.PricePerKg = newPrice;
            await _context.SaveChangesAsync();


            return Ok("Priset har nu ändrats.");
        }}
