using System.Collections.Generic;

namespace Game.Common.Interfaces.Managers.Calculators.Initials
{
    public interface IInitialCostsCalculatorManager : IInitialCalculatorManager
    {
        public List<object> CalculateInitialCosts(int numberOfMachines, bool noLetterForOutputMoney = false);
    }
}