var products = new List<Product>
{
  new("Keyboard", 45.99m),
  new("Mouse", 19.99m),
};

Product? found = products.FirstOrDefault(p => p.Name == "Mouse");

// Your turn: print the found product's name, or "Not found" if it's null.
// Use either an if/else or the ?. and ?? operators.

if(found != null)
  Console.WriteLine(found.Name);
else
  Console.WriteLine("Not Found");

Console.WriteLine(found?.Name ?? "Not found");

record Product(string Name, decimal Price);


