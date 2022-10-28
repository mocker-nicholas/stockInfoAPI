using Microsoft.EntityFrameworkCore;
using stockInfoApi.DAL.ControllerFeatures;
using stockInfoApi.DAL.Data;
using stockInfoApi.DAL.Interfaces;
using stockInfoApi.DAL.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DevDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("stockInfoApi")
    );
});

// Add something to IOC singlton = 1, scoped = per req, transient = every reference
builder.Services.AddScoped<IAccountFeatures, AccountFeatures>();
builder.Services.AddScoped<IStocksFeatures, StocksFeatures>();
builder.Services.AddScoped<ITransactionsFeatures, TransactionsFeatures>();
builder.Services.AddScoped<StockQuotes, StockQuotes>();

builder.Services.AddControllers().AddJsonOptions(options =>
{ options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
