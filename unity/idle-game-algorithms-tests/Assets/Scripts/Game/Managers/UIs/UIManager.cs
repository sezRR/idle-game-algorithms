using Core.Helpers;
using Game.Common.Interfaces.Managers.Calculators.Initials;
using Game.Common.Interfaces.Managers.UIs;
using Game.Managers.UIs.Data;
using Game.ScriptableObjects.StageConfigs;
using Game.ScriptableObjects.UIs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Managers.UIs
{
    public class UIManager : MonoBehaviour, IUIManager
    {
        private UISettingsPrefabFactory _uiSettingsPrefabFactory;
        private UISettingsScriptableObject _uiSettingsScriptableObject;
        private IInitialCostsCalculatorManager _initialCostsCalculator;
        private static MachinesData _machinesData;

        [Inject]
        public void Construct(UISettingsPrefabFactory uiSettingsPrefabFactory,
            UISettingsScriptableObject uiSettingsScriptableObject,
            IInitialCostsCalculatorManager initialCostsCalculator, MachinesData machinesData)
        {
            _uiSettingsPrefabFactory = uiSettingsPrefabFactory;
            _uiSettingsScriptableObject = uiSettingsScriptableObject;
            _initialCostsCalculator = initialCostsCalculator;
            _machinesData = machinesData;
        }

        public void InitializeUIElements(StageConfigScriptableObject stageConfigScriptableObject)
        {
            _uiSettingsPrefabFactory.StageConfigScriptableObject = stageConfigScriptableObject;
            
            var canvasGameObject = _uiSettingsPrefabFactory.Create(_uiSettingsScriptableObject);
            var machinesGameObject = canvasGameObject.transform.Find("Container").Find("Machines").gameObject;
            var countOfMachinesGameObject = canvasGameObject.transform.Find("CountOfMachinesStatusComponent").gameObject;

            AddMachinePrefabsToContainer(stageConfigScriptableObject.QuantityOfMachines, _uiSettingsScriptableObject.MachineUIComponentPrefab, machinesGameObject, stageConfigScriptableObject.InitialMachineCost);
            AddCountOfMachinesToUI(countOfMachinesGameObject, stageConfigScriptableObject.QuantityOfMachines);
        }

        private void AddCountOfMachinesToUI(GameObject countOfMachinesGameObject, int machineCount)
        {
            UIHelper.UpdateTextElement(
                UIHelper.GetComponentFromInnerGameObjectByName<TMP_Text>(countOfMachinesGameObject, "NumberOfMachines"),
                machineCount.ToString());
        }

        private float UpdateMachineGameObjectTexts(int indexer, TMP_Text[] texts, float previousPrice)
        {
            UIHelper.UpdateTextElement(texts[0], $"Machine {indexer}");

            UIHelper.UpdateTextElement(texts[1],
                $"${_initialCostsCalculator.GetInitialCostWithFormatOutput(previousPrice)}");
            var newPrice = _initialCostsCalculator.CalculateInitialCost(previousPrice);

            return newPrice;
        }

        private void AddMachinePrefabsToContainer(int count, GameObject machineUIComponentPrefab, GameObject attachToGameObject,
            float initialPrice)
        {
            var attachRectTransform = attachToGameObject.GetComponent<RectTransform>();
            var gridLayoutGroup = attachToGameObject.GetComponent<GridLayoutGroup>();
            var currentHeight = UIHelper.GetCurrentHeight(attachRectTransform, gridLayoutGroup);
            
            for (int i = 1; i <= count; i++)
            {
                var addedMachineGameObject = AddMachinePrefabToContainer(machineUIComponentPrefab, attachToGameObject,
                    gridLayoutGroup, ref currentHeight, initialPrice);

                var texts = addedMachineGameObject.GetComponentsInChildren<TMP_Text>();
                initialPrice = UpdateMachineGameObjectTexts(i, texts, initialPrice);
            }

            var addedRectTransform = machineUIComponentPrefab.GetComponent<RectTransform>();
            addedRectTransform.offsetMax = new Vector2(addedRectTransform.offsetMax.x, 0);
        }

        public static GameObject AddMachinePrefabToContainer(GameObject machineUIComponentPrefab, GameObject attachToGameObject,
            GridLayoutGroup gridLayoutGroup, ref float currentHeight, float machinePrice)
        {
            var machineUIComponentGameObject = Instantiate(machineUIComponentPrefab, attachToGameObject.transform);
            currentHeight += gridLayoutGroup.cellSize.y;

            var attachRectTransform = attachToGameObject.GetComponent<RectTransform>();
            attachRectTransform.sizeDelta = new Vector2(attachRectTransform.sizeDelta.x, currentHeight);

            _machinesData.Machines.Push((machineUIComponentGameObject, machinePrice));

            return machineUIComponentGameObject;
        }
    }
}