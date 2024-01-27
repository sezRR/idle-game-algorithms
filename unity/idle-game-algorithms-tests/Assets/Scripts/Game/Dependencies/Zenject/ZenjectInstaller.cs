using Core.Helpers;
using Game.Common.Interfaces.Managers;
using Game.Common.Interfaces.Managers.Calculators.Initials;
using Game.Common.Interfaces.Managers.UIs;
using Game.Managers;
using Game.Managers.Calculators;
using Game.Managers.UIs;
using Game.ScriptableObjects.MoneyFormats;
using Game.ScriptableObjects.UIs;
using UnityEngine;
using Zenject;

namespace Game.Dependencies.Zenject
{
    public class ZenjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MoneyFormatScriptableObject>()
                .FromScriptableObject(AssetsHelper.FindAssetByType<MoneyFormatScriptableObject>()).AsSingle();
            Container.Bind<IMoneyManager>().To<MoneyManager>().AsSingle();
            
            Container.Bind<IInitialCostsCalculatorManager>().To<InitialCostsCalculator>().AsSingle();

            Container.Bind<UISettingsScriptableObject>()
                .FromScriptableObject(AssetsHelper.FindAssetByType<UISettingsScriptableObject>()).AsSingle();
            Container.Bind<IUIManager>().To<UIManager>().FromNewComponentOnNewGameObject().AsSingle();
            Container.BindFactory<UISettingsScriptableObject, GameObject, UIManager.UISettingsPrefabFactory>().AsSingle();
        }
    }
}