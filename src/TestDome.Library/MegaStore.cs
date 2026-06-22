
namespace TestDome.Library;


public static class MegaStore
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
        return discountType switch
        {
            DiscountType.Standard => totalPrice * (1 - 0.06),
            DiscountType.Seasonal => totalPrice * (1 - 0.12),
            DiscountType.Weight => cartWeight <= 10 
                                    ? totalPrice * (1 - 0.06)
                                    : totalPrice * (1 - 0.18),
            _ => 0.0
        };
    }

    public static double GetDiscountedPriceII(double cartWeight, 
                                        double totalPrice, 
                                        DiscountType discountType)
    {       
        double discountRate = 0.0;

        switch (discountType)
        {
            case DiscountType.Standard:
                discountRate = 0.06; 
                break;

            case DiscountType.Seasonal:
                discountRate = 0.12; 
                break;

            case DiscountType.Weight:
                discountRate = cartWeight <= 10 ? 0.06 : 0.18;
                break;

            default:
                return totalPrice; // sin descuento
        }

        return totalPrice * (1 - discountRate);
    }

    public static void Main(string[] args)
    {
        Console.WriteLine(GetDiscountedPrice(12, 100, DiscountType.Weight));
        Console.WriteLine(GetDiscountedPriceII(12, 100, DiscountType.Weight));

    }
}

