using System.Collections.Generic;
using Game.Common.Interfaces.Managers;
using Game.Common.Interfaces.Managers.Calculators.Initials;
using Zenject;

namespace Game.Managers.Calculators
{
    public class InitialCostsCalculator : IInitialCostsCalculatorManager
    {
        private IMoneyManager _moneyManager;
        
        // TODO: Scriptable Object
        private readonly float _defaultInitialCost = 5;
        private readonly float _initialCostMultiplier = 24;

        [Inject]
        public InitialCostsCalculator(IMoneyManager moneyManager)
        {
            _moneyManager = moneyManager;
        }
        
        public float CalculateInitialCost(float previousInitialCost = 0)
        {
            return previousInitialCost == 0
                ? _defaultInitialCost
                : previousInitialCost * _initialCostMultiplier;
        }
        
        public string CalculateInitialCostWithFormatOutput(float previousInitialCost = 0, bool noLetterForOutputMoney = false)
        {
            return previousInitialCost == 0
                ? _moneyManager.GetFormattedMoney(_defaultInitialCost, noLetterForOutputMoney)
                : _moneyManager.GetFormattedMoney(previousInitialCost * _initialCostMultiplier, noLetterForOutputMoney);
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