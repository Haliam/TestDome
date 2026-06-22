Console formatting examples (C#)

Below are ten commonly used console formatting techniques and short C# examples for each. Start at the top for the most commonly used patterns.

1. Basic writes and newlines

```csharp
Console.WriteLine("Hello, world!");
Console.Write("Enter name: "); // no newline
var name = Console.ReadLine();
Console.WriteLine($"Hello {name}");
```

2. Foreground color (ConsoleColor)

```csharp
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Success: operation completed.");
Console.ResetColor();
```

3. Background color

```csharp
Console.BackgroundColor = ConsoleColor.DarkBlue;
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine(" Notice with background color ");
Console.ResetColor();
```

4. ANSI escape codes (useful in modern terminals)

```csharp
// Windows 10+ terminal and most UNIX terminals support ANSI sequences
Console.Write("\u001b[31mThis is red using ANSI\u001b[0m\n");
```

5. String interpolation and composite formatting

```csharp
int items = 3;
double price = 1234.56;
Console.WriteLine($"Items: {items}, Total: {price:C}");
// Composite formatting / alignment
Console.WriteLine("{0,-20} {1,8:C}", "Product A", 19.99);
```

6. Numeric and Date/Time formatting

```csharp
double value = 0.12345;
Console.WriteLine(value.ToString("P2")); // "12.35 %"
Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
```

7. Padding, alignment and fixed-width columns

```csharp
string left = "left";
string right = "right";
Console.WriteLine(left.PadRight(10) + "|" + right.PadLeft(10));
// Interpolation with alignment
Console.WriteLine($"{"Name",-20}{"Qty",6}{"Price",10}");
```

8. Simple table output

```csharp
var rows = new[] {
	("Apple", 10, 0.5),
	("Banana", 5, 0.25),
};
Console.WriteLine("{0,-10} {1,6} {2,8}", "Item", "Qty", "Price");
foreach (var r in rows)
	Console.WriteLine("{0,-10} {1,6} {2,8:C}", r.Item1, r.Item2, r.Item3);
```

9. Progress bar (single-line, using \r)

```csharp
for (int i = 0; i <= 100; i++)
{
	Console.Write($"Progress: {i,3}% [");
	int filled = i / 5;
	Console.Write(new string('#', filled));
	Console.Write(new string('-', 20 - filled));
	Console.Write($"]\r");
	Thread.Sleep(20);
}
Console.WriteLine();
```

10. Timestamped / leveled logging (simple approach)

```csharp
void Log(string level, string message)
{
	var now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
	switch (level)
	{
		case "ERROR": Console.ForegroundColor = ConsoleColor.Red; break;
		case "WARN": Console.ForegroundColor = ConsoleColor.Yellow; break;
		case "INFO": Console.ForegroundColor = ConsoleColor.Cyan; break;
		default: Console.ResetColor(); break;
	}
	Console.WriteLine($"[{now}] {level}: {message}");
	Console.ResetColor();
}

Log("INFO", "Application started");
Log("WARN", "Low disk space");
Log("ERROR", "Unhandled exception");
```

Extras and tips
- Use `Console.CursorVisible = false` when drawing transient UI like progress bars.
- Prefer `ConsoleColor` for quick cross-platform color support; use ANSI when you need more control or 24-bit color.
- For production logging, prefer `Microsoft.Extensions.Logging` or a dedicated logging library to control sinks and formatting.

If you want these examples translated into Spanish, or ported to another language (Python, Node.js), tell me which ones and I will add them.