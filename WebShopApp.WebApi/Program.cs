using Microsoft.EntityFrameworkCore;
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