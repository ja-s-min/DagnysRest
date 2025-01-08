using Microsoft.EntityFrameworkCore;
using dagnys_api;
using dagnys_api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>{
    options.UseSqlite(builder.Configuration.GetConnectionString("DevConnection"));
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if(app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
var context = services.GetRequiredService<DataContext>();
await context.Database.MigrateAsync();
await Seed.LoadProducts(context);
await Seed.LoadSupplier(context);
await Seed.LoadRecipe(context);
await Seed.LoadPurchase(context);
await Seed.LoadRawMaterial(context);
await Seed.LoadSupplierRawMaterial(context);
}
catch (Exception ex)
{Console.WriteLine("{0}", ex.Message);
throw;
}

app.MapControllers();

app.Run();
