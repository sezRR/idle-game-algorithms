using Core.Helpers;
using Game.Common.Helpers;
using Game.Common.Interfaces.Helpers;
using Game.Common.Interfaces.Managers;
using Game.ScriptableObjects.MoneyFormats;
using Zenject;

namespace Game.Managers
{
    public class MoneyManager : IMoneyManager
    {
        private readonly MoneyFormatScriptableObject _moneyFormatScriptableObject;
        
        [Inject]
        public MoneyManager(MoneyFormatScriptableObject moneyFormatScriptableObject)
        {
            _moneyFormatScriptableObject = moneyFormatScriptableObject;
        }
        
        public string GetFormattedMoney(float number, bool noLetter = false)
        {
            var suffixIndex = 0;

            while (number >= 1000 && suffixIndex < _moneyFormatScriptableObject.Formats.Count - 1 && !noLetter)
            {
                suffixIndex += 1;
                number /= 1000;
            }

            var roundedNumber = $"{MathHelper.Rounder(number)}";
            return noLetter ? roundedNumber : $"{roundedNumber} {_moneyFormatScriptableObject.Formats[suffixIndex]}";
        }
    }
}