using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Managers.Calculators
{
    [Serializable]
    public class InitialCostsCalculator
    {
        [SerializeField] 
        private MoneyManager moneyManager;
        
        [SerializeField] 
        private float defaultInitialCost = 5;
        
        [SerializeField] 
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
                
                initialCosts.Add(moneyManager.GetFormattedMoney(previousInitialCost, noLetterForOutputMoney));
            }

            return initialCosts;
        }
    }
}