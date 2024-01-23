using System;
using System.Collections.Generic;
using Game.Common.Interfaces.Helpers;
using Game.Common.Interfaces.Managers;
using Game.Common.Interfaces.Managers.Calculators.Initials;
using UnityEngine;
using Zenject;

namespace Game.Managers.Calculators
{
    public class InitialCostsCalculator : IInitialCostsCalculatorManager
    {
        [Inject]
        private IMoneyManager _moneyManager;
        
        private float defaultInitialCost = 5;
        private float initialCostMultiplier = 24;
        
        public float CalculateInitialCost(float previousInitialCost = 0)
        {
            return previousInitialCost == 0
                ? defaultInitialCost
                : previousInitialCost * initialCostMultiplier;
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