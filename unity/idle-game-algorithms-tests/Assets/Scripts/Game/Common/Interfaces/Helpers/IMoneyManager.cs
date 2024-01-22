namespace Game.Common.Interfaces.Helpers
{
    public interface IMoneyManager : IHelper
    {
        public string GetFormattedMoney(float number, bool noLetter = false);
    }
}