using System;
using Game.ScriptableObjects.MoneyFormats;
using UnityEngine;

namespace Game.Helpers
{
    [Serializable]
    public class MoneyHelper
    {
        [SerializeField]
        private MoneyFormatScriptableObject moneyFormatScriptableObject;
        
        public string GetFormattedMoney(float number, bool noLetter = false)
        {
            var suffixIndex = 0;

            while (number >= 1000 && suffixIndex < moneyFormatScriptableObject.Formats.Count - 1 && !noLetter)
            {
                suffixIndex += 1;
                number /= 1000;
            }

            var roundedNumber = $"{Rounder(number)}";
            return noLetter ? roundedNumber : $"{roundedNumber} {moneyFormatScriptableObject.Formats[suffixIndex]}";
        }

        public static float Rounder(double number, int nDigits = 2)
        {
            return (float)Math.Round(number, nDigits);
        }
    }
}