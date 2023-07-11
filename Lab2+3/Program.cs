using Lab2_3.Bussiness.IRepository;
using Lab2_3.Bussiness.Mapping;
using Lab2_3.Bussiness.Repository;
using Lab2_3.DataAccess.Models;
using Lab2_3.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<NorthwindContext>(opt => builder.Configuration.GetConnectionString("Northwind"));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapHub<CartHub>("/cartHub");

app.UseAuthorization();

app.MapRazorPages();

app.Run();
