using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using WebShopApp.Business.DataProtection;
using WebShopApp.Business.Operations.User;
using WebShopApp.Data.Context;
using WebShopApp.Data.Repositories;
using WebShopApp.Data.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// db context
builder.Services.AddDbContext<WebShopAppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("Default")
));

// Service Lifetimes
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // Generic olduğu için typof kullanıldı
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserManager>();


builder.Services.AddScoped<IDataProtection, DataProtection>();
var keysDirectory = new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "App_Data", "Keys"));
builder.Services.AddDataProtection().SetApplicationName("WebShopApp").PersistKeysToFileSystem(keysDirectory); // sadece 1 bilgisayarda çalışır. birden fazla bilgisayarda çalışması için AddDataProtection().PersistKeysToSqlServer() kullanılmalıdır.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.Run();