Console.WriteLine(Describe(5));
Console.WriteLine(Describe(-3));
Console.WriteLine(Describe("hello"));
Console.WriteLine(Describe(null));
Console.WriteLine(Describe(new Product("Mouse", 19.99m)));
Console.WriteLine(Grade(85));
Console.WriteLine(Grade(45));

string Describe(object? thing) => thing switch
{
    null => "nothing",
    int n when n < 0 => $"negative number {n}",
    int n => $"number {n}",
    string s => $"text of length {s.Length}",
    Product { Price: > 15 } p => $"{p.Name}, a pricey product",
    Product p => $"{p.Name}, a cheap product",
    _ => "something else"
};

string Grade(int score) => score switch
{
    >= 70 => "Distinction",
    >= 50 => "Pass",
    >= 40 and < 50 => "Close",
    _ => "Fail"
};

record Product(string Name, decimal Price);