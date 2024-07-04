using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

public class CartService
{
    public List<CartItem> CartItems { get; private set; } = new List<CartItem>();

    public void AddToCart(Product product)
    {
        var existingItem = CartItems.FirstOrDefault(item => item.Product.Name == product.Name);
        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            CartItems.Add(new CartItem { Product = product, Quantity = 1 });
        }
    }

    public async Task AddToCartAsync(Product product)
    {
        await Task.Run(() => AddToCart(product)); // Simulación de operación asíncrona
    }

    public void UpdateQuantity(Product product, int quantity)
    {
        var existingItem = CartItems.FirstOrDefault(item => item.Product.Name == product.Name);
        if (existingItem != null)
        {
            existingItem.Quantity = quantity;
        }
    }

    public decimal CalcularTotal()
    {
        return CartItems.Sum(item => item.Product.Price * item.Quantity);
    }

    public decimal GetTotal()
    {
        return CartItems.Sum(item => item.Product.Price * item.Quantity);
    }

    public string GetTotalFormatted()
    {
        return GetTotal().ToString("C", CultureInfo.CreateSpecificCulture("es-ES"));
    }
}

public class CartItem
{
    public Product Product { get; set; }
    public int Quantity { get; set; }
}

public class Product
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}
