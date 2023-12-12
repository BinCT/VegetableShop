using Microsoft.EntityFrameworkCore;
using VegetableShop.Data.Catalog.Manager;
using VegetableShop.Data.Common;
using VegetableShop.Data.EF;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ICustomerManager, CustomerManager>();
builder.Services.AddScoped<IBillManager, BillManager>();
builder.Services.AddScoped<IBillDetailManager, BillDetailManager>();
builder.Services.AddScoped<ICategoryManager, CategoryManager>();
builder.Services.AddScoped<IProductManager,ProductManager>(); 
builder.Services.AddScoped<IStorageService, StorageService>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<VegetableShopDbContext>
	(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("VegetableShopDb")));

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
	pattern: "{controller=Vegetable}/{action=Index}/{id?}");

app.Run();
