using Core.Helpers;
using Game.Common.Interfaces.Managers.Calculators.Initials;
using Game.Managers.UIs.Data;
using Game.ScriptableObjects.StageConfigs;
using Game.ScriptableObjects.UIs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Managers.UIs
{
    public class UISettingsPrefabFactory : PlaceholderFactory<UISettingsScriptableObject, GameObject>
    {
        private readonly UISettingsScriptableObject _uiSettingsScriptableObject;
        private readonly IInitialCostsCalculatorManager _initialCostsCalculator;
        private readonly MachinesData _machinesData;
        
        public StageConfigScriptableObject StageConfigScriptableObject { get; set; }

        [Inject]
        public UISettingsPrefabFactory(UISettingsScriptableObject uiSettingsScriptableObject,
            IInitialCostsCalculatorManager initialCostsCalculator, MachinesData machinesData)
        {
            _uiSettingsScriptableObject = uiSettingsScriptableObject;
            _initialCostsCalculator = initialCostsCalculator;
            _machinesData = machinesData;
        }

        private GameObject _canvasGameObject;
        private float _lastHeight;

        public override GameObject Create(UISettingsScriptableObject param)
        {
            _initialCostsCalculator.DefaultInitialCost = StageConfigScriptableObject.InitialMachineCost;
            // _initialCostsCalculator.DefaultInitialCostMultiplier = StageConfigScriptableObject.;

            _canvasGameObject = Object.Instantiate(param.Canvas);
            SubscribeToCountOfMachinesComponentButtons(_canvasGameObject);

            return _canvasGameObject;
        }

        private void SubscribeToCountOfMachinesComponentButtons(GameObject canvas)
        {
            var baseGameComponent =
                canvas.transform.Find("CountOfMachinesStatusComponent")
                    .transform; // TODO: make them tags and get by enum

            var decreaseButton = baseGameComponent.Find("Decrease").GetComponent<Button>();
            var increaseButton = baseGameComponent.Find("Increase").GetComponent<Button>();

            decreaseButton.onClick.AddListener(() =>
                DecreaseButtonAction(
                    UIHelper.GetComponentFromInnerGameObjectByName<TMP_Text>(baseGameComponent.gameObject,
                        "NumberOfMachines")));

            increaseButton.onClick.AddListener(() =>
                IncreaseButtonAction(
                    UIHelper.GetComponentFromInnerGameObjectByName<TMP_Text>(baseGameComponent.gameObject,
                        "NumberOfMachines")));
        }

        private void DecreaseButtonAction(TMP_Text textElement)
        {
            if (int.Parse(textElement.text) == 0)
                return;

            UIHelper.UpdateTextElement(textElement, EvaluateButtonAction(textElement, '-'));

            var attachToGameObject = _canvasGameObject.transform.Find("Container").Find("Machines").gameObject;
            var attachRectTransform = attachToGameObject.GetComponent<RectTransform>();
            var gridLayoutGroup = attachToGameObject.GetComponent<GridLayoutGroup>();
            Object.Destroy(_machinesData.Machines.Pop().machine);
            _lastHeight = UIHelper.GetCurrentHeight(attachRectTransform, gridLayoutGroup) -
                          gridLayoutGroup.padding.top -
                          gridLayoutGroup.cellSize.y;
            attachRectTransform.sizeDelta = new Vector2(attachRectTransform.sizeDelta.x, _lastHeight);
        }

        private void IncreaseButtonAction(TMP_Text textElement)
        {
            UIHelper.UpdateTextElement(textElement, EvaluateButtonAction(textElement, '+'));

            var attachToGameObject = _canvasGameObject.transform.Find("Container").Find("Machines").gameObject;
            var attachRectTransform = attachToGameObject.GetComponent<RectTransform>();
            var gridLayoutGroup = attachToGameObject.GetComponent<GridLayoutGroup>();
            _lastHeight = UIHelper.GetCurrentHeight(attachRectTransform, gridLayoutGroup) -
                          gridLayoutGroup.padding.top; // TODO: DUPLICATE

            float lastPrice = 0;
            if (_machinesData.Machines.Count != 0)
                lastPrice = _machinesData.Machines.Peek().initialCost;
                
            var addedMachineGameObject = UIManager.AddMachinePrefabToContainer(_uiSettingsScriptableObject.MachineUIComponentPrefab,
                attachToGameObject, gridLayoutGroup, ref _lastHeight,
                _initialCostsCalculator.CalculateInitialCost(lastPrice));

            var texts = addedMachineGameObject.GetComponentsInChildren<TMP_Text>();

            UIHelper.UpdateTextElement(texts[0], $"Machine {_machinesData.Machines.Count}");
            UIHelper.UpdateTextElement(texts[1], $"${_initialCostsCalculator.CalculateInitialCostWithFormatOutput(lastPrice)}");
        }

        private string EvaluateButtonAction(TMP_Text textElement, char operation)
        {
            var textToInt = int.Parse(textElement.text);
            return operation switch
            {
                '+' => (++textToInt).ToString(),
                '-' => (--textToInt).ToString(),
                _ => textToInt.ToString()
            };
        }
    }
}