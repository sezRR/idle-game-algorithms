using Game.Common.Constants.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace Game.ScriptableObjects.UIs
{
    public static class UISettingsScriptableObjectMenuAction
    {
        [MenuItem(ScriptableObjectMenuItemPaths.UISettingsScriptableObject)]
        public static void CreateMyAsset()
        {
            var asset = ScriptableObject.CreateInstance<UISettingsScriptableObject>();

            AssetDatabase.CreateAsset(asset, ScriptableObjectCreatePathConstants.UISettingsScriptableObject);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }   
    }
}