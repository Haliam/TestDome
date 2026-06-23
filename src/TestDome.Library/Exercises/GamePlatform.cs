namespace TestDome.Library;

using System;

public static class GamePlatform
{
        public static double CalculateFinalSpeed(double initialSpeed, int[] inclinations)
        {
            if(!IsValid(initialSpeed, inclinations))
                return 0.0; 
            
            double finalSpeed = initialSpeed;

            foreach (int inclination in inclinations)
            {
                finalSpeed -= inclination;
            }

            return finalSpeed <= 0 
                ? 0.0 
                : finalSpeed;
        }

        public static bool IsValid(double initialSpeed, int[] inclinations)
        {
            if (inclinations == null || inclinations.Length == 0){
                return false;
            }
            if (initialSpeed < 0){
                return false;
            }

            // Ensure each inclination is strictly less than 90 degrees in magnitude
            foreach (var inclination in inclinations)
            {
                if (inclination >= 90 || inclination <= -90)
                    return false;
            }

            return true;
        }
}
