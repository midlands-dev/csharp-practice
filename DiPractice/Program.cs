using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddTransient<TransientThing>();
builder.Services.AddScoped<ScopedThing>();
builder.Services.AddSingleton<SingletonThing>();

using var host = builder.Build();

for (int i = 1; i <= 2; i++)
{
    // Each loop is like one web request getting its own scope

    using var scope = host.Services.CreateScope();
    Console.WriteLine($"--- Scope {i} ---");

    for (int j = 1; j <= 2; j++)
    {
        var t = scope.ServiceProvider.GetRequiredService<TransientThing>();
        var s = scope.ServiceProvider.GetRequiredService<ScopedThing>();
        var g = scope.ServiceProvider.GetRequiredService<SingletonThing>();
        Console.WriteLine($"Ask {j}: Transient {t.Id} | Scoped {s.Id} | Singleton {g.Id}");
    }
}

class TransientThing 
{ 
  public string Id { get; } = Guid.NewGuid().ToString()[..4]; 
}

class ScopedThing 
{ 
  public string Id { get; } = Guid.NewGuid().ToString()[..4]; 
}

class SingletonThing 
{ 
  public string Id { get; } = Guid.NewGuid().ToString()[..4]; 
}