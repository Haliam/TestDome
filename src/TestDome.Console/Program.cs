using TestDome.Library;


//Console.WriteLine("Collections demo outputs:");
//foreach (var line in CollectionsDemo.RunAll())
//{
//    Console.WriteLine(line);
//}

//MEGA STORE TESTS
Console.WriteLine($"Price with discount = {MegaStore.GetDiscountedPrice(12, 100, MegaStore.DiscountType.Weight)}");
Console.WriteLine($"Price with discount = {MegaStore.GetDiscountedPriceII(12, 100, MegaStore.DiscountType.Weight)}");

//GAME PLATFORM TESTS
Console.WriteLine($"Final speed = {GamePlatform.CalculateFinalSpeed(60, new int[] { 0, 30, 0, -45, 0 })}");










