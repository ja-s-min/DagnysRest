using dagnys_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace dagnys_api.Data;

    
    public class DataContext : DbContext
{
    public DbSet<Product>Products { get; set; }
    public DbSet<Supplier>Suppliers { get; set; }
    public DbSet<Recipe>Recipes { get; set; }
    public DbSet<Purchase>Purchases { get; set; }
    public DbSet<RawMaterial>RawMaterials { get; set; }
    public DbSet<SupplierRawMaterial>SupplierRawMaterials { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
        }    

         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SupplierRawMaterial>()
                .HasKey(srm => new { srm.SupplierId, srm.RawMaterialId });

            modelBuilder.Entity<SupplierRawMaterial>()
                .HasOne(srm => srm.Supplier)
                .WithMany(s => s.SupplierRawMaterials)
                .HasForeignKey(srm => srm.SupplierId);

            modelBuilder.Entity<SupplierRawMaterial>()
                .HasOne(srm => srm.RawMaterial)
                .WithMany(r => r.SupplierRawMaterials)
                .HasForeignKey(srm => srm.RawMaterialId);
        }
}
