using Core.Helpers;
using Game.Helpers;
using Game.Managers;
using Game.Managers.Calculators;
using Game.ScriptableObjects.StageConfigs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Generators
{
    public class Stage : MonoBehaviour
    {
        [FormerlySerializedAs("moneyHelper")] [SerializeField] 
        private MoneyManager moneyManager;

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

            Debug.Log(moneyManager.GetFormattedMoney(100000000000000000));
            
        }
    }
}
