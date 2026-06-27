using TestDome.Library;

//Console.WriteLine("Collections demo outputs:");
//foreach (var line in CollectionsDemo.RunAll())
//{
//    Console.WriteLine(line);
//}

//MEGA STORE TESTS
//Console.WriteLine($"Price with discount = {MegaStore.GetDiscountedPrice(12, 100, MegaStore.DiscountType.Weight)}");
//Console.WriteLine($"Price with discount = {MegaStore.GetDiscountedPriceII(12, 100, MegaStore.DiscountType.Weight)}");

//GAME PLATFORM TESTS
//Console.WriteLine($"Final speed = {GamePlatform.CalculateFinalSpeed(60, new int[] { 0, 30, 0, -45, 0 })}");


//int[] numbers = { 4, 6, 9 };


//foreach (var number in numbers)
//{
//    Console.WriteLine(number);
//}

//for (int i = 0; i < numbers.Length; i++)
//{
//    Console.WriteLine($"Index:{i}, Value: {numbers[i]}");
//};


//int[] numbers = { 1, 2, 3, 4, 5 };

//for (int left = 0, right = numbers.Length - 1; left < right; left++, right--)
//{
//    Console.WriteLine($"Left: {numbers[left]}, Right: {numbers[right]}");
//}


//int[] numbers = { 5, 10, 15, 20, 25 };

//for (int i = numbers.Length - 1; i >= 0; i--)
//{
//    if (numbers[i] < 15)
//        break;

//    Console.WriteLine(numbers[i]);
//}



//List<string> list = new() { "Ana", "Jorge", "Harold" };

//Dictionary<string, int> dict = new();

//dict.Add("Jorge", 26);
//dict.Add("Harold", 45);

//Dictionary<string, int> dictio = new()
//{
//    { "Jorge", 26 },
//    { "Harold", 45 }
//};

//HashSet<int> hash = new HashSet<int>() { 4, 12, 45 };

//HashSet<int> hashset = new() {4, 12, 45 };



//int[,] matrix =
//{
//    { 1, 2, 3 },
//    { 4, 5, 6 }
//};

//for (int i = 0; i < matrix.GetLength(0); i++) // rows
//{
//    for (int j = 0; j < matrix.GetLength(1); j++) // columns
//    {
//        Console.WriteLine($"[{i},{j}] = {matrix[i, j]}");
//    }
//}





int[] numbers = { 1, 2, 3, 2, 4, 1 };

//for (int i = 0; i < numbers.Length; i++)
//{
//    for (int j = i + 1; j < numbers.Length; j++)
//    {
//        if (numbers[i] == numbers[j])
//        {
//            bool alreadyPrinted = false;

//            // Check if this number appeared before index i
//            for (int k = 0; k < i; k++)
//            {
//                if (numbers[k] == numbers[i])
//                {
//                    alreadyPrinted = true;
//                    break;
//                }
//            }

//            if (!alreadyPrinted)
//            {
//                Console.WriteLine($"Duplicate: {numbers[i]}");
//            }

//            break; // avoid repeated matches for same i
//        }
//    }
//}

List<int> nonDuplicate = new List<int>();

for (int i = 0; i < numbers.Length; i++)
{
    for (int j = i + 1; j < numbers.Length; j++)
    {
        if (!(numbers[i] == numbers[j]))
        {
            nonDuplicate.Add(numbers[i]);
        }
    }
}
foreach (var item in nonDuplicate)
{
    Console.WriteLine(item);
}



for (int i = 0; i < numbers.Length; i++)
{
    bool alreadySeen = false;

    // Check if the number appeared before index i
    for (int j = 0; j < i; j++)
    {
        if (numbers[i] == numbers[j])
        {
            alreadySeen = true;
            break;
        }
    }

    if (!alreadySeen)
    {
        Console.WriteLine(numbers[i]);
    }
}






