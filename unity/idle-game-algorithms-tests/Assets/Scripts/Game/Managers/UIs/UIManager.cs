using Core.Helpers;
using Game.Common.Interfaces.Managers.UIs;
using Game.ScriptableObjects.UIs;
using UnityEngine;
using Zenject;

namespace Game.Managers.UIs
{
    public class UIManager : MonoBehaviour, IUIManager
    {
        [Inject]
        private UISettingsPrefabFactory _uiSettingsPrefabFactory;
        
        [Inject]
        private UISettingsScriptableObject _uiSettingsScriptableObject;
        
        public void Test()
        {
            _uiSettingsPrefabFactory.Create(_uiSettingsScriptableObject);
        }
        
        public class UISettingsPrefabFactory : PlaceholderFactory<UISettingsScriptableObject, GameObject>
        {
            public override GameObject Create(UISettingsScriptableObject param)
            {
                var uiGameObject = new GameObject("UI");
                var canvasGameObject = Instantiate(param.Canvas, uiGameObject.transform);

                var machinesGameObject = canvasGameObject.transform.Find("Machines").gameObject;

                var countOfMachinesStatusUIComponentGameObject = Instantiate(param.CountOfMachinesStatusUIComponentPrefab, canvasGameObject.transform);
                
                var machineUIComponentGameObject = Instantiate(param.MachineUIComponentPrefab, machinesGameObject.transform);
                machineUIComponentGameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(120, -66);

                return countOfMachinesStatusUIComponentGameObject;
            }
        }
    }
}
