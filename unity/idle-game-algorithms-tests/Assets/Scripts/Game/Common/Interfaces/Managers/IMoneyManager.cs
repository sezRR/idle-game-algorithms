using Game.Common.Interfaces.Helpers;

namespace Game.Common.Interfaces.Managers
{
    public interface IMoneyManager : IHelper // TODO: ?
    {
        public string GetFormattedMoney(float number, bool noLetter = false);
    }
}