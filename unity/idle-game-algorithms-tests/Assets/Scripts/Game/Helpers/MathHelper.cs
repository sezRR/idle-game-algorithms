using System;

namespace Game.Helpers
{
    public static class MathHelper
    {
        public static float Rounder(double number, int nDigits = 2)
        {
            return (float)Math.Round(number, nDigits);
        }
    }
}