using System.Collections.Generic;
using Game.Common.Interfaces.Managers;
using Game.Common.Interfaces.Managers.Calculators.Initials;
using Zenject;

namespace Game.Managers.Calculators
{
    public class InitialCostsCalculator : IInitialCostsCalculatorManager
    {
        private readonly IMoneyManager _moneyManager;
        
        // TODO: Scriptable Object
        public float DefaultInitialCost { get; set; } = 5;
        public float DefaultInitialCostMultiplier { get; set; } = 24;

        [Inject]
        public InitialCostsCalculator(IMoneyManager moneyManager)
        {
            _moneyManager = moneyManager;
        }
        
        public float CalculateInitialCost(float previousInitialCost = 0)
        {
            return previousInitialCost == 0
                ? DefaultInitialCost
                : previousInitialCost * DefaultInitialCostMultiplier;
        }
        
        public string CalculateInitialCostWithFormatOutput(float previousInitialCost = 0, bool noLetterForOutputMoney = false)
        {
            return previousInitialCost == 0
                ? _moneyManager.GetFormattedMoney(DefaultInitialCost, noLetterForOutputMoney)
                : _moneyManager.GetFormattedMoney(previousInitialCost * DefaultInitialCostMultiplier, noLetterForOutputMoney);
        }

        public string GetInitialCostWithFormatOutput(float initialCost)
        {
            return _moneyManager.GetFormattedMoney(initialCost);
        }

        public List<object> CalculateInitialCosts(int numberOfMachines, bool noLetterForOutputMoney = false)
        {
            List<object> initialCosts = new();
            float previousInitialCost = 0;
            
            for (int i = 0; i < numberOfMachines; i++)
            {
                previousInitialCost = CalculateInitialCost(previousInitialCost);
                
                initialCosts.Add(_moneyManager.GetFormattedMoney(previousInitialCost, noLetterForOutputMoney));
            }

            return initialCosts;
        }
    }
}