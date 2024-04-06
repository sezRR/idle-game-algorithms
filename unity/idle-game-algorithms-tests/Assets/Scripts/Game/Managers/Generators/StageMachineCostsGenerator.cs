using Core.Helpers;
using Game.Common.Interfaces.Managers;
using Game.Common.Interfaces.Managers.Calculators.Initials;
using Game.Common.Interfaces.Managers.UIs;
using Game.ScriptableObjects.StageConfigs;
using UnityEngine;
using Zenject;

namespace Game.Managers.Generators
{
    public class StageMachineCostsGenerator : MonoBehaviour
    {
        private IMoneyManager _moneyManager;
        private IInitialCostsCalculatorManager _initialCostsCalculator;
        private IUIManager _uiManager;

        [Inject]
        public void Construct(IMoneyManager moneyManager, IInitialCostsCalculatorManager initialCostsCalculator, IUIManager uiManager)
        {
            _moneyManager = moneyManager;
            _initialCostsCalculator = initialCostsCalculator;
            _uiManager = uiManager;
        }

        void Start()
        {
            var stageConfigs = AssetsHelper.FindAssetsByType<StageConfigScriptableObject>();

            foreach (var stageConfig in stageConfigs)
            {
                Debug.Log($"Found StageConfig: {stageConfig.StageName}");

                foreach (var cost in _initialCostsCalculator.CalculateInitialCosts(stageConfig.QuantityOfMachines))
                {
                    Debug.Log(cost);
                }
            }

            Debug.Log(_moneyManager.GetFormattedMoney(100000000000000000));

            _uiManager.InitializeUIElements(stageConfigs[0]);
        }
    }
}
