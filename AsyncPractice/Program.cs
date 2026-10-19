using System;
using System.Diagnostics;
using System.Threading.Tasks;

var stopwatch = Stopwatch.StartNew();

Task<string> task1 = FetchDataAsync("first");   // starts running immediately, not awaited yet
Task<string> task2 = FetchDataAsync("second");  // also starts running immediately

string result1 = await task1;  // now wait for both to finish
string result2 = await task2;

stopwatch.Stop();
Console.WriteLine($"Got: {result1}, {result2}");
Console.WriteLine($"Took: {stopwatch.ElapsedMilliseconds}ms");

async Task<string> FetchDataAsync(string label)
{
    await Task.Delay(2000);
    return $"Data ({label})";
}