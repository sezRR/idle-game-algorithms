using Game.Common.Interfaces.Managers;
using Game.Common.Interfaces.Managers.Calculators.Initials;
using Game.Managers;
using Game.Managers.Calculators;
using Zenject;

namespace Game.Dependencies.Zenject
{
    public class ZenjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IMoneyManager>().To<MoneyManager>().AsSingle();
            Container.Bind<IInitialCostsCalculatorManager>().To<InitialCostsCalculator>().AsSingle();
        }
    }
}