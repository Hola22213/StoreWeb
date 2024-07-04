using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;
using System.Net.Http;
using System.Threading.Tasks;   
using Microsoft.Extensions.DependencyInjection;
using StoreWeb.Services;
using Microsoft.AspNetCore.Components.Web;
using static StoreWeb.Services.ProductoService;
using StoreWeb;
using StoreWeb.Pages; // Aseg�rate de ajustar el espacio de nombres seg�n tu aplicaci�n

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// A�adir servicios necesarios
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<ProductoService>(); // A�ade cualquier otro servicio necesario aqu�
builder.Services.AddScoped<CestaService>(); // Registrar CestaService u otros servicios necesarios
builder.Services.AddScoped<FinalizarPago>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://tu-api-url") });

builder.Services.AddScoped<CartService>();

// Configurar el root component y otros componentes necesarios
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Construir y ejecutar la aplicaci�n
var host = builder.Build();
await host.RunAsync();
