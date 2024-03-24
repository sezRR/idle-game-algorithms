using Core.Helpers;
using Game.Common.Constants.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace Game.ScriptableObjects.UIs
{
    public class UISettingsScriptableObject : ScriptableObject
    {
        [SerializeField] private GameObject countOfMachinesStatusUIComponentPrefab;
        [SerializeField] private GameObject machineUIComponentPrefab;
        [SerializeField] private GameObject canvas;

        public GameObject CountOfMachinesStatusUIComponentPrefab => countOfMachinesStatusUIComponentPrefab;
        public GameObject MachineUIComponentPrefab => machineUIComponentPrefab;
        public GameObject Canvas => canvas;
        
        [MenuItem(ScriptableObjectMenuItemPaths.UISettingsScriptableObject)]
        public static void CreateMyAsset()
        {
            MenuItemHelper.Create<UISettingsScriptableObject>(ScriptableObjectCreatePathConstants.UISettingsScriptableObject);
        }   
    }
}