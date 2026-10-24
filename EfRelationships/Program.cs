using Microsoft.EntityFrameworkCore;

using var db = new ShopContext();
await db.Database.EnsureDeletedAsync();
await db.Database.EnsureCreatedAsync();

var electronics = new Category { Name = "Electronics" };
var pcParts = new Category { Name = "PC Parts" };

db.Products.AddRange(
    new Product { Name = "Mouse", Price = 19.99, Category = electronics },
    new Product { Name = "Monitor", Price = 189.00, Category = electronics },
    new Product { Name = "Keyboard", Price = 45.99, Category = pcParts }
);
await db.SaveChangesAsync(); // EF Core saves the categories too, because the products point at them

// 1. Include: load each category WITH its products (this becomes a JOIN)
var categories = await db.Categories.Include(c => c.Products).ToListAsync();
foreach (var c in categories)
{
    Console.WriteLine($"{c.Name} ({c.Products.Count} products)");
    foreach (var p in c.Products)
        Console.WriteLine($"   {p.Name} - £{p.Price}");
}

// 2. Go the other way: filter products by their category's name
var cheapElectronics = await db.Products
    .Where(p => p.Category.Name == "Electronics" && p.Price < 100)
    .ToListAsync();
Console.WriteLine($"\nCheap electronics: {string.Join(", ", cheapElectronics.Select(p => p.Name))}");

Console.WriteLine(db.Products.Include(p=>p.Category).ToQueryString());

class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public List<Product> Products { get; set; } = new();
}

class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public double Price { get; set; }
    public int CategoryId { get; set; }                 // foreign key
    public Category Category { get; set; } = null!;     // navigation property
}

class ShopContext : DbContext
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=relationships.db");
}