namespace TestDome.Library
{


    public class BoatMovements
    {
        public static bool CanTravelTo(bool[,] gameMatrix, int fromRow, int fromColumn, int toRow, int toColumn)
        {
            // 1. Bounds check
            if (!IsValid(gameMatrix, fromRow, fromColumn, toRow, toColumn))
                return false;


            // 2. Only horizontal or vertical movement allowed
            if (fromRow == toRow)
            {
                int start = Math.Min(fromColumn, toColumn);
                int end = Math.Max(fromColumn, toColumn);

                for (int col = start; col <= end; col++)
                {
                    if (!gameMatrix[fromRow, col])
                        return false;
                }
                return true;
            }
            else if (fromColumn == toColumn)
            {
                int start = Math.Min(fromRow, toRow);
                int end = Math.Max(fromRow, toRow);

                for (int row = start; row <= end; row++)
                {
                    if (!gameMatrix[row, fromColumn])
                        return false;
                }
                return true;
            }

            // 3. No
            return false;
        }

        public static void Main()
        {
            bool[,] gameMatrix =
            {
            {false, true,  true,  false, false, false},
            {true,  true,  true,  false, false, false},
            {true,  true,  true,  true,  true,  true},
            {false, true,  true,  false, true,  true},
            {false, true,  true,  true,  false, true},
            {false, false, false, false, false, false},
        };

            Console.WriteLine(CanTravelTo(gameMatrix, 3, 2, 2, 2)); // true, Valid move
            Console.WriteLine(CanTravelTo(gameMatrix, 3, 2, 3, 4)); // false, Can't travel through land
            Console.WriteLine(CanTravelTo(gameMatrix, 3, 2, 6, 2)); // false, Out of bounds
        }

        private static bool IsValid(bool[,] gameMatrix, int fromRow, int fromColumn, int toRow, int toColumn)
        {
            int rows = gameMatrix.GetLength(0);
            int columnns = gameMatrix.GetLength(1);

            if (fromRow < 0
                || fromRow >= rows
                || fromColumn < 0
                || fromColumn >= columnns
                || toRow < 0
                || toRow >= rows
                || toColumn < 0
                || toColumn >= columnns)
            {
                return false;
            }

            return true;
        }
    }
}
