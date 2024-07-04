using ERP.Api;
using MinimalAPIERP.Api;
using MinimalAPIERP.Data;
using MinimalAPIERP.Models;
using Microsoft.EntityFrameworkCore;
using MinimalAPIERP.MinimalAPIERP.Api;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddAntiforgery();
builder.Services
    .AddCustomSqlServerDb(builder.Configuration)
    .AddCustomHealthCheck(builder.Configuration)
    .AddCustomOpenApi(builder.Configuration);

var app = builder.Build();

// Asegurar que la base de datos esté creada
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

// Configuración de middleware y endpoints
app.MapCustomHealthCheck(builder.Configuration);
app.UseAntiforgery();
app.DatabaseInit();
app.ConfigureSwagger();
app.MapStoreApi();
app.MapRaincheckApi();

// Integración de CarItemAPI
var carItemAPI = new CarItemAPI();
carItemAPI.MapEndpoints(app);

app.Run();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});
