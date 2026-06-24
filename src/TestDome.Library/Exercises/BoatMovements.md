`C# | 2D Array | Graphs | ? | ? min | code | Public`


# Boat Movements

A turn-based strategy game has a grid with water and land. The grid contains a true value where it's water and false where it's land.

The player controls a boat unit with a particular movement pattern. It can only move to fixed destinations from its current position as shown in the video below.

The boat can only move in a direct path through water to the possible destinations, so a destination will become unreachable if there is land in the way.

Implement the **CanTravelTo** function, that checks whether a destination is reachable by the boat. It should return true for destinations that are reachable according to the pattern above, and false for unreachable or out of bounds destinations which are outside the grid.

For example, consider the following code:

```csharp
bool[,] gameMatrix =
{
    {false, true,  true,  false, false, false},
    {true,  true,  true,  true,  false, false},
    {true,  true,  true,  true,  true,  true },
    {false, true,  true,  false, true,  true },
    {false, true,  true,  true,  false, true },
    {false, false, false, false, false, false},
};

Console.WriteLine(CanTravelTo(gameMatrix, 3, 2, 2, 2)); // true, Valid move
Console.WriteLine(CanTravelTo(gameMatrix, 3, 2, 3, 4)); // false, Can't travel through land
Console.WriteLine(CanTravelTo(gameMatrix, 3, 2, 6, 2)); // false, Out of bounds
```

The following image shows valid and invalid destinations when the boat is in the position (3, 2):

***

## Código inicial

```csharp
using System;

public class BoatMovements
{
    public static bool CanTravelTo(bool[,] gameMatrix, int fromRow, int fromColumn, int toRow, int toColumn)
    {
        throw new NotImplementedException("Waiting to be implemented");
    }

    public static void Main()
    {
        bool[,] gameMatrix =
        {
            {false, true,  true,  false, false, false},
            {true,  true,  true,  true,  false, false},
            {true,  true,  true,  true,  true,  true },
            {false, true,  true,  false, true,  true },
            {false, true,  true,  true,  false, true },
            {false, false, false, false, false, false},
        };

        Console.WriteLine(CanTravelTo(gameMatrix, 3, 2, 2, 2)); // true, Valid move
        Console.WriteLine(CanTravelTo(gameMatrix, 3, 2, 3, 4)); // false, Can't travel through land
        Console.WriteLine(CanTravelTo(gameMatrix, 3, 2, 6, 2)); // false, Out of bounds
    }
}


```

    /* 

    PseudoCode
        if out of limits → false

        if  horizontal movement
            check path

        else if vertical movement
            check path

        else
            false
    */

