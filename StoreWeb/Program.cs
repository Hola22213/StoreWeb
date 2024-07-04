using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;
using System.Net.Http;
using System.Threading.Tasks;   
using Microsoft.Extensions.DependencyInjection;
using StoreWeb.Services;
using Microsoft.AspNetCore.Components.Web;
using static StoreWeb.Services.ProductoService;
using StoreWeb;
using StoreWeb.Pages; // Asegúrate de ajustar el espacio de nombres según tu aplicación

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Añadir servicios necesarios
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<ProductoService>(); // Añade cualquier otro servicio necesario aquí
builder.Services.AddScoped<CestaService>(); // Registrar CestaService u otros servicios necesarios
builder.Services.AddScoped<FinalizarPago>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://tu-api-url") });

builder.Services.AddScoped<CartService>();

// Configurar el root component y otros componentes necesarios
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Construir y ejecutar la aplicación
var host = builder.Build();
await host.RunAsync();
