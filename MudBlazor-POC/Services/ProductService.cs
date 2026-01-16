using MudBlazor_POC.Models;

namespace MudBlazor_POC.Services;

public class ProductService
{
    private List<Product> products = new();
    private int nextId = 1;

    public ProductService()
    {
        // Initialize with sample data
        products = new List<Product>
        {
            new Product
            {
                Id = nextId++,
                Name = "Wireless Gaming Mouse",
                Description = "High-precision optical sensor with customizable RGB lighting",
                Price = 79.99m,
                Stock = 45,
                Category = "Peripherals",
                CreationDate = DateTime.Now.AddDays(-30)
            },
            new Product
            {
                Id = nextId++,
                Name = "Mechanical Keyboard",
                Description = "Cherry MX switches with per-key RGB and aluminum frame",
                Price = 149.99m,
                Stock = 32,
                Category = "Peripherals",
                CreationDate = DateTime.Now.AddDays(-25)
            },
            new Product
            {
                Id = nextId++,
                Name = "27\" 4K Monitor",
                Description = "IPS panel, 144Hz refresh rate, HDR400 certified",
                Price = 499.99m,
                Stock = 15,
                Category = "Monitors",
                CreationDate = DateTime.Now.AddDays(-20)
            },
            new Product
            {
                Id = nextId++,
                Name = "USB-C Hub",
                Description = "7-in-1 adapter with HDMI, USB 3.0, and SD card reader",
                Price = 39.99m,
                Stock = 120,
                Category = "Accessories",
                CreationDate = DateTime.Now.AddDays(-15)
            },
            new Product
            {
                Id = nextId++,
                Name = "Wireless Headphones",
                Description = "Active noise cancellation, 30-hour battery life",
                Price = 299.99m,
                Stock = 8,
                Category = "Audio",
                CreationDate = DateTime.Now.AddDays(-10)
            },
            new Product
            {
                Id = nextId++,
                Name = "Portable SSD 1TB",
                Description = "NVMe technology, up to 1050MB/s read speed",
                Price = 129.99m,
                Stock = 0,
                Category = "Storage",
                CreationDate = DateTime.Now.AddDays(-5)
            },
            new Product
            {
                Id = nextId++,
                Name = "Webcam 1080p",
                Description = "Full HD with auto-focus and built-in microphone",
                Price = 89.99m,
                Stock = 55,
                Category = "Electronics",
                CreationDate = DateTime.Now.AddDays(-3)
            },
            new Product
            {
                Id = nextId++,
                Name = "Laptop Stand",
                Description = "Adjustable aluminum stand with ergonomic design",
                Price = 49.99m,
                Stock = 67,
                Category = "Accessories",
                CreationDate = DateTime.Now.AddDays(-1)
            }
        };
    }

    public List<Product> GetAll() => products.OrderByDescending(p => p.CreationDate).ToList();

    public Product? GetById(int id) => products.FirstOrDefault(p => p.Id == id);

    public void Create(Product product)
    {
        product.Id = nextId++;
        product.CreationDate = DateTime.Now;
        products.Add(product);
    }

    public void Update(Product product)
    {
        var existing = products.FirstOrDefault(p => p.Id == product.Id);
        if (existing != null)
        {
            existing.Name = product.Name;
            existing.Description = product.Description;
            existing.Price = product.Price;
            existing.Stock = product.Stock;
            existing.Category = product.Category;
        }
    }

    public void Delete(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            products.Remove(product);
        }
    }

    public List<Product> Search(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return GetAll();

        return products
            .Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                       p.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                       p.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(p => p.CreationDate)
            .ToList();
    }

    public List<Product> GetByCategory(string? category)
    {
        if (string.IsNullOrWhiteSpace(category))
            return GetAll();

        return products
            .Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(p => p.CreationDate)
            .ToList();
    }

    public List<string> GetCategories()
    {
        return products
            .Select(p => p.Category)
            .Distinct()
            .OrderBy(c => c)
            .ToList();
    }

    public int GetTotalProducts() => products.Count;

    public decimal GetTotalValue() => products.Sum(p => p.Price * p.Stock);

    public int GetLowStockCount() => products.Count(p => p.Stock < 10);
}
