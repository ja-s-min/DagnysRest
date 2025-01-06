using System.Text.Json;
using dagnys_api.Entities;

namespace dagnys_api.Data;

        public static class Seed
        {
    
            public static async Task LoadProducts(DataContext context)
            {
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            };

            if(context.Products.Any()) return;

            var json = File.ReadAllText("Data/json/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(json, options);

            if(products is not null && products.Count > 0){
                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }


          public static async Task LoadSupplier(DataContext context)
            {
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            };

            if(context.Suppliers.Any()) return;

            var json = File.ReadAllText("Data/json/suppliers.json");
            var suppliers = JsonSerializer.Deserialize<List<Supplier>>(json, options);

            if(suppliers is not null && suppliers.Count > 0){
                await context.Suppliers.AddRangeAsync(suppliers);
                await context.SaveChangesAsync();
            }
        }

         public static async Task LoadRecipe(DataContext context)
            {
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            };

            if(context.Recipes.Any()) return;

            var json = File.ReadAllText("Data/json/recipes.json");
            var recipes = JsonSerializer.Deserialize<List<Recipe>>(json, options);

            if(recipes is not null && recipes.Count > 0){
                await context.Recipes.AddRangeAsync(recipes);
                await context.SaveChangesAsync();
            }
        }


        
         public static async Task LoadPurchase(DataContext context)
            {
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            };

            if(context.Purchases.Any()) return;

            var json = File.ReadAllText("Data/json/purchases.json");
            var purchases = JsonSerializer.Deserialize<List<Purchase>>(json, options);

            if(purchases is not null && purchases.Count > 0){
                await context.Purchases.AddRangeAsync(purchases);
                await context.SaveChangesAsync();
            }
        }


           public static async Task LoadRawMaterial(DataContext context)
            {
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            };

            if(context.RawMaterials.Any()) return;

            var json = File.ReadAllText("Data/json/materials.json");
            var materials = JsonSerializer.Deserialize<List<RawMaterial>>(json, options);

            if(materials is not null && materials.Count > 0){
                await context.RawMaterials.AddRangeAsync(materials);
                await context.SaveChangesAsync();
            }
        }
        public static async Task LoadSupplierRawMaterial(DataContext context)
        {
            var options = new JsonSerializerOptions{
                 PropertyNameCaseInsensitive = true 
                 };
            if (context.SupplierRawMaterials.Any()) return;
            var json = File.ReadAllText("Data/json/supplierRawMaterials.json");
            var supplierRawMaterials = JsonSerializer.Deserialize<List<SupplierRawMaterial>>(json, options);
            if (supplierRawMaterials is not null && supplierRawMaterials.Count > 0)
            {
                await context.SupplierRawMaterials.AddRangeAsync(supplierRawMaterials);
                await context.SaveChangesAsync();
                }
                
            }

    }
    
