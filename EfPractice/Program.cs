using Microsoft.EntityFrameworkCore;

using var db = new ShopContext();

// Applies any migrations the database hasn't seen yet
await db.Database.MigrateAsync();

// Only seed on the first run, so rows don't duplicate
if (!await db.Products.AnyAsync())
{
    db.Products.Add(new Product { Name = "Keyboard", Category = "Pc Parts", Price = 45.99 });
    db.Products.Add(new Product { Name = "Mouse", Category = "Electronics", Price = 19.99 });
    db.Products.Add(new Product { Name = "Monitor", Category = "Electronics", Price = 189.00 });
    await db.SaveChangesAsync();
}

var all = await db.Products.OrderBy(p => p.Name).ToListAsync();
foreach (var p in all)
    Console.WriteLine($"{p.Id}: {p.Name} {p.Stock} ({p.Category}) - £{p.Price}");

class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public double Price { get; set; }
    public string Category { get; set; } = "";
    public int Stock {get; set; }
}

class ShopContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=shop.db");
}