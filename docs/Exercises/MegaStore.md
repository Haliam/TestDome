`C# | Arithmetic | Conditional Statements | Enum | Easy | 10 min | code | Public`

---

# Mega Store

A megastore offers three types of discounts, which are represented as `DiscountType` enum.  
Implement the `GetDiscountedPrice` method which should take the total weight of the shopping cart, the total price, and the discount type. It should return the final discounted price based on the discount schemes as shown in the promotional video below.

Standard / Weight Any / DiscountRate = 0.06
Seasonal / Weight Any / DiscountRate = 0.12
Weight   / Weight <= 10
            ? DiscountRate = 0.06  
            : DiscountRate = 0.18

For example, the following code:

```
Console.WriteLine(GetDiscountedPrice(12, 100, DiscountType.Weight));
```

should print:

```
82.0
```

## C# code

```csharp
using System;

public class MegaStore
{
    public enum DiscountType
    {
        Standard,
        Seasonal,
        Weight
    }

    public static double GetDiscountedPrice(double cartWeight,
                                            double totalPrice,
                                            DiscountType discountType)
    {
        return 0.0;
    }
}
```

## Test results

- Example case: Wrong answer  
- Standard Discount: Wrong answer  
- Seasonal Discount: Wrong answer  
- Weighted Discount: Wrong answer  

## Information

- Difficulty: Easy  
- Duration: 10 min  
- Type: CODE  
- Set: Public  

**Tags:** C#, Arithmetic, Conditional Statements, Enum, Video

---

