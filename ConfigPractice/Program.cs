using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);

// Bind the "Shop" section of appsettings.json to the ShopSettings class

builder.Services.Configure<ShopSettings>(builder.Configuration.GetSection("Shop"));
builder.Services.AddTransient<OrderService>();

using var host = builder.Build();

var orders = host.Services.GetRequiredService<OrderService>();
await orders.PlaceAsync("Keyboard", 2);
await orders.PlaceAsync("Mouse", 5);

class ShopSettings
{
    public string Name { get; set; } = "";
    public int MaxItemsPerOrder { get; set; }
}

class OrderService
{
    private readonly ShopSettings _settings;

    // IOptions<T> is how the container hands you a bound settings object

    public OrderService(IOptions<ShopSettings> options) => _settings = options.Value;

    public async Task PlaceAsync(string item, int quantity)
    {
        await Task.Delay(500); // pretend this is a database call

        if (quantity > _settings.MaxItemsPerOrder)
        {
            Console.WriteLine($"[{_settings.Name}] {item} rejected: {quantity} is over the limit of {_settings.MaxItemsPerOrder}");
            return;
        }

        Console.WriteLine($"[{_settings.Name}] Order placed: {quantity} x {item}");
    }
}