using ERP.Api;
using MinimalAPIERP.Api;
using MinimalAPIERP.Data;
using MinimalAPIERP.Models;
using Microsoft.EntityFrameworkCore;
using MinimalAPIERP.MinimalAPIERP.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAntiforgery();
builder.Services
    .AddCustomSqlServerDb(builder.Configuration)
    .AddCustomHealthCheck(builder.Configuration)
    .AddCustomOpenApi(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

app.MapCustomHealthCheck(builder.Configuration);
app.UseAntiforgery();
app.DatabaseInit();
app.ConfigureSwagger();
app.MapStoreApi();
app.MapRaincheckApi();

// Integración de PaymentApi
app.MapPaymentApi();

// Integración de CarItemAPI
var carItemAPI = new CarItemAPI();
carItemAPI.MapEndpoints(app);

app.Run();
