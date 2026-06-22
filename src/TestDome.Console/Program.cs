using TestDome.Library;

//Console.WriteLine("Collections demo outputs:");
//foreach (var line in CollectionsDemo.RunAll())
//{
//    Console.WriteLine(line);
//}

//MEGA STORE TESTS
Console.WriteLine($"Price with discount = {MegaStore.GetDiscountedPrice(12, 100, MegaStore.DiscountType.Weight)}");
Console.WriteLine($"Price with discount = {MegaStore.GetDiscountedPriceII(12, 100, MegaStore.DiscountType.Weight)}");

int items = 3;
double price = 1234.56;
Console.WriteLine($"Items: {items}, Total: {price:C}");
// Composite formatting / alignment
Console.WriteLine("{0,-20} {1,8:C}", "Product A", 19.99);

// Direct calls to verify new examples
// (diagnostic checks removed)
