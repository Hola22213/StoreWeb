using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StoreWeb.Services
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string ImagenUrl { get; set; }
    }

    public class ProductoService
    {
        private readonly HttpClient _httpClient;
        private readonly CestaService _cestaService;

        public ProductoService(HttpClient httpClient, CestaService cestaService)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _cestaService = cestaService ?? throw new ArgumentNullException(nameof(cestaService));
        }

        public async Task<List<Producto>> GetProductosAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Producto>>("api/productos");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error al obtener productos: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Producto>> GetProductosAsync(int pagina, int cantidadPorPagina)
        {
            try
            {
                var productos = await _httpClient.GetFromJsonAsync<List<Producto>>("api/productos");
                return productos.Skip((pagina - 1) * cantidadPorPagina).Take(cantidadPorPagina).ToList();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error al obtener productos paginados: {ex.Message}");
                throw;
            }
        }

        public async Task AñadirACestaAsync(int productoId)
        {
            try
            {
                await _httpClient.PostAsJsonAsync("api/cesta", new { ProductoId = productoId });
                var producto = await GetProductoByIdAsync(productoId);
                if (producto != null)
                {
                    _cestaService.AñadirProducto(producto);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error al añadir producto a la cesta: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Producto>> GetCestaAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Producto>>("api/cesta");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error al obtener productos de la cesta: {ex.Message}");
                throw;
            }
        }

        public async Task AñadirAlPedidoAsync()
        {
            try
            {
                await _httpClient.PostAsync("api/pedido", null);
                _cestaService.VaciarCesta();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error al añadir pedido: {ex.Message}");
                throw;
            }
        }

        private async Task<Producto> GetProductoByIdAsync(int productoId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Producto>($"api/productos/{productoId}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error al obtener producto por ID: {ex.Message}");
                return null;
            }
        }

        public List<Producto> ObtenerProductosEnCesta()
        {
            return _cestaService.GetProductosEnCesta();
        }
    }

    public class CestaService
    {
        public List<Producto> ProductosEnCesta { get; private set; } = new List<Producto>();

        public void AñadirProducto(Producto producto)
        {
            ProductosEnCesta.Add(producto);
        }

        public List<Producto> GetProductosEnCesta()
        {
            return ProductosEnCesta;
        }

        public void VaciarCesta()
        {
            ProductosEnCesta.Clear();
        }
        public class ProductService
        {
            private readonly HttpClient _httpClient;

            public ProductService(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            public async Task<List<Product>> GetProductsAsync()
            {
                return await _httpClient.GetFromJsonAsync<List<Product>>("erp/products");
            }
        }

    }
}
