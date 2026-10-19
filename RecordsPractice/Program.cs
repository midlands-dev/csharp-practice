var a = new Product("Keyboard", 45.99m);
var b = new Product("Keyboard", 45.99m);

// 1. Printing and equality
Console.WriteLine(a);
Console.WriteLine($"Record equal: {a == b}");

// 2. Copy with a change, original untouched
var c = a with { Price = 39.99m };
Console.WriteLine(c);
Console.WriteLine(a);

// 3. Deconstruction
var (name, price) = a;
Console.WriteLine($"{name} costs £{price}");

// 4. Same data in a normal class, for comparison
var oldA = new OldProduct("Keyboard", 45.99m);
var oldB = new OldProduct("Keyboard", 45.99m);
Console.WriteLine($"Class equal: {oldA == oldB}");

record Product(string Name, decimal Price);

class OldProduct
{
    public string Name { get; }
    public decimal Price { get; }

    public OldProduct(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}