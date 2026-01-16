using Blazorise_POC.Models;

namespace Blazorise_POC.Services;

public class ProductService
{
    private List<Product> _products = new();
    private int _nextId = 1;

    public ProductService()
    {
        // Sample data
        _products = new List<Product>
        {
            new Product { Id = _nextId++, Name = "Dell XPS 15 Laptop", Description = "High performance laptop", Price = 1299.99m, Stock = 15, Category = "Electronics", CreationDate = DateTime.Now.AddDays(-30) },
            new Product { Id = _nextId++, Name = "Logitech MX Master Mouse", Description = "Ergonomic wireless mouse", Price = 99.99m, Stock = 45, Category = "Peripherals", CreationDate = DateTime.Now.AddDays(-25) },
            new Product { Id = _nextId++, Name = "Keychron K2 Mechanical Keyboard", Description = "Compact mechanical keyboard", Price = 89.99m, Stock = 30, Category = "Peripherals", CreationDate = DateTime.Now.AddDays(-20) },
            new Product { Id = _nextId++, Name = "LG UltraWide 34\" Monitor", Description = "Curved ultrawide monitor", Price = 599.99m, Stock = 8, Category = "Monitors", CreationDate = DateTime.Now.AddDays(-15) },
            new Product { Id = _nextId++, Name = "Logitech C920 Webcam", Description = "Full HD webcam", Price = 79.99m, Stock = 25, Category = "Peripherals", CreationDate = DateTime.Now.AddDays(-10) },
            new Product { Id = _nextId++, Name = "Sony WH-1000XM5 Headphones", Description = "Noise canceling headphones", Price = 399.99m, Stock = 12, Category = "Audio", CreationDate = DateTime.Now.AddDays(-5) },
            new Product { Id = _nextId++, Name = "Samsung 1TB SSD", Description = "NVMe solid state drive", Price = 129.99m, Stock = 50, Category = "Storage", CreationDate = DateTime.Now.AddDays(-3) },
            new Product { Id = _nextId++, Name = "Anker USB-C Hub", Description = "Multi-port USB-C hub", Price = 49.99m, Stock = 35, Category = "Accessories", CreationDate = DateTime.Now.AddDays(-1) }
        };
    }

    public List<Product> GetAll()
    {
        return _products.OrderByDescending(p => p.CreationDate).ToList();
    }

    public Product? GetById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public Product Create(Product product)
    {
        product.Id = _nextId++;
        product.CreationDate = DateTime.Now;
        _products.Add(product);
        return product;
    }

    public bool Update(Product product)
    {
        var existing = GetById(product.Id);
        if (existing == null) return false;

        existing.Name = product.Name;
        existing.Description = product.Description;
        existing.Price = product.Price;
        existing.Stock = product.Stock;
        existing.Category = product.Category;
        return true;
    }

    public bool Delete(int id)
    {
        var product = GetById(id);
        if (product == null) return false;

        _products.Remove(product);
        return true;
    }

    public List<Product> Search(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return GetAll();

        searchTerm = searchTerm.ToLower();
        return _products
            .Where(p => p.Name.ToLower().Contains(searchTerm) ||
                       p.Description.ToLower().Contains(searchTerm) ||
                       p.Category.ToLower().Contains(searchTerm))
            .OrderByDescending(p => p.CreationDate)
            .ToList();
    }

    public List<string> GetCategories()
    {
        return _products.Select(p => p.Category).Distinct().OrderBy(c => c).ToList();
    }
}
