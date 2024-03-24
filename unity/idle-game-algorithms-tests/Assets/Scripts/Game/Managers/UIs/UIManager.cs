using Game.Common.Interfaces.Managers.Calculators.Initials;
using Game.Common.Interfaces.Managers.UIs;
using Game.ScriptableObjects.UIs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Managers.UIs
{
    public abstract class UIManager : MonoBehaviour, IUIManager
    {
        [Inject]
        private UISettingsPrefabFactory _uiSettingsPrefabFactory;
        
        [Inject]
        private UISettingsScriptableObject _uiSettingsScriptableObject;
        
        [Inject]
        private IInitialCostsCalculatorManager _initialCostsCalculator;
        
        // TODO: maybe core?
        public void InitializeUIElements(int quantityOfMachines, float initialPrice)
        {
            var canvasGameObject = _uiSettingsPrefabFactory.Create(_uiSettingsScriptableObject);
            var machinesGameObject = canvasGameObject.transform.Find("Container").Find("Machines").gameObject;
            
            AddMachinesToUI(quantityOfMachines, _uiSettingsScriptableObject.MachineUIComponentPrefab, machinesGameObject, initialPrice);
        }

        // TODO: MAKE STATIC AND MOVE TO CORE, NAMED UIHELPER, CORE
        public void UpdateTextElement(TMP_Text oldText, string newText)
        {
            oldText.text = newText;
        }

        private float UpdateMachineGameObjectTexts(int indexer, TMP_Text[] texts, float previousPrice)
        {
            UpdateTextElement(texts[0], $"Machine {indexer}");

            var newPrice = _initialCostsCalculator.CalculateInitialCost(previousPrice);
            UpdateTextElement(texts[1], $"${_initialCostsCalculator.CalculateInitialCostWithFormatOutput(newPrice)}");

            return newPrice;
        }
        
        // TODO: REFACTOR
        private void AddMachinesToUI(int count, GameObject machineUIComponentPrefab, GameObject attachToGameObject, float initialPrice)
        {
            var attachRectTransform = attachToGameObject.GetComponent<RectTransform>();
            var gridLayoutGroup = attachToGameObject.GetComponent<GridLayoutGroup>();

            float currentHeight = attachRectTransform.rect.height + gridLayoutGroup.padding.top + gridLayoutGroup.padding.bottom;
            for (int i = 1; i <= count; i++)
            {
                var addedMachineGameObject = AddMachineToUI(machineUIComponentPrefab, attachToGameObject, gridLayoutGroup, ref currentHeight);

                var texts = addedMachineGameObject.GetComponentsInChildren<TMP_Text>();
                initialPrice = UpdateMachineGameObjectTexts(i, texts, initialPrice);
            }
            
            var addedRectTransform = machineUIComponentPrefab.GetComponent<RectTransform>();
            addedRectTransform.offsetMax = new Vector2(addedRectTransform.offsetMax.x, 0);
        }

        // TODO: REFACTOR
        private GameObject AddMachineToUI(GameObject machineUIComponentPrefab, GameObject attachToGameObject, GridLayoutGroup gridLayoutGroup, ref float currentHeight)
        {
            var machineUIComponentGameObject = Instantiate(machineUIComponentPrefab, attachToGameObject.transform);
            currentHeight += gridLayoutGroup.cellSize.y;

            var attachRectTransform = attachToGameObject.GetComponent<RectTransform>();
            attachRectTransform.sizeDelta = new Vector2(attachRectTransform.sizeDelta.x, currentHeight);

            return machineUIComponentGameObject;
        }

        public abstract class UISettingsPrefabFactory : PlaceholderFactory<UISettingsScriptableObject, GameObject>
        {
            public override GameObject Create(UISettingsScriptableObject param)
            {
                var canvasGameObject = Instantiate(param.Canvas);
                var countOfMachinesStatusUIComponentGameObject = Instantiate(param.CountOfMachinesStatusUIComponentPrefab, canvasGameObject.transform);
                
                return canvasGameObject;
            }
        }
    }
}
