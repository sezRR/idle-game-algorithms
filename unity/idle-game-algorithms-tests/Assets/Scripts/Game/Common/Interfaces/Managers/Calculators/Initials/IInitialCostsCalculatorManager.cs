using System.Collections.Generic;

namespace Game.Common.Interfaces.Managers.Calculators.Initials
{
    public interface IInitialCostsCalculatorManager : IInitialCalculatorManager
    {
        public float CalculateInitialCost(float previousInitialCost = 0);

        public string CalculateInitialCostWithFormatOutput(float previousInitialCost = 0, bool noLetterForOutputMoney = false);
        public List<object> CalculateInitialCosts(int numberOfMachines, bool noLetterForOutputMoney = false);
    }
}