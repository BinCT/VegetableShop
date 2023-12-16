using Microsoft.EntityFrameworkCore;
using VegetableShop.Data.Catalog.Manager;
using VegetableShop.Data.Common;
using VegetableShop.Data.EF;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IProductManager, ProductManager>();
builder.Services.AddScoped<IStorageService, StorageService>();

builder.Services.AddDbContext<VegetableShopDbContext>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("VegetableShopDb")));
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=VegetableShop}/{action=Index}/{id?}");

app.Run();
