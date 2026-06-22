using TestDome.Library;

//Console.WriteLine("Collections demo outputs:");
//foreach (var line in CollectionsDemo.RunAll())
//{
//    Console.WriteLine(line);
//}

//MEGA STORE TESTS
Console.WriteLine($"Price with discount = {MegaStore.GetDiscountedPrice(12, 100, MegaStore.DiscountType.Weight)}");
Console.WriteLine($"Price with discount = {MegaStore.GetDiscountedPriceII(12, 100, MegaStore.DiscountType.Weight)}");

var array = new int[] { 1, 2, 3, 4, 5 };

int[] array2 = new int[] { };

var lenght = array.Length;

// Direct calls to verify new examples
// (diagnostic checks removed)
