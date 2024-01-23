using Core.Helpers;
using Game.Common.Interfaces.Helpers;
using Game.Common.Interfaces.Managers;
using Game.Common.Interfaces.Managers.Calculators.Initials;
using Game.Managers.Calculators;
using Game.ScriptableObjects.StageConfigs;
using UnityEngine;
using Zenject;

namespace Game.Managers.Generators
{
    public class Stage : MonoBehaviour
    {
        [Inject]
        private IMoneyManager _moneyManager;

        [Inject] 
        private IInitialCostsCalculatorManager _initialCostsCalculator;
        
        void Start()
        {
            StageConfigScriptableObject[] stageConfigs = AssetsHelper.FindAssetsByType<StageConfigScriptableObject>();

            foreach (var stageConfig in stageConfigs)
            {
                Debug.Log($"Found StageConfig: {stageConfig.StageName}");

                foreach (var cost in _initialCostsCalculator.CalculateInitialCosts(stageConfig.QuantityOfMachines))
                {
                    Debug.Log(cost);
                }
            }

            Debug.Log(_moneyManager.GetFormattedMoney(100000000000000000));
        }
    }
}
