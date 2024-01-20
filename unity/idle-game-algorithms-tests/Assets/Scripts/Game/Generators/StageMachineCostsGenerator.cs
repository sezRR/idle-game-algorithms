using Core.Helpers;
using Game.Helpers;
using Game.Helpers.Calculators;
using Game.ScriptableObjects.StageConfigs;
using UnityEngine;

namespace Game.Generators
{
    public class Stage : MonoBehaviour
    {
        [SerializeField] 
        private MoneyHelper moneyHelper;

        [SerializeField] 
        private InitialCostsCalculator initialCostsCalculator;
        
        void Start()
        {
            StageConfigScriptableObject[] stageConfigs = AssetsHelper.FindAssetsByType<StageConfigScriptableObject>();

            foreach (var stageConfig in stageConfigs)
            {
                Debug.Log($"Found StageConfig: {stageConfig.StageName}");
                Debug.Log(initialCostsCalculator.CalculateInitialCosts(stageConfig.QuantityOfMachines));
            }

            Debug.Log(moneyHelper.GetFormattedMoney(100000000000000000));
            
        }
    }
}
