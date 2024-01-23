using Game.Common.Constants.ScriptableObjects;
using UnityEditor;
using ScriptableObject = UnityEngine.ScriptableObject;

namespace Game.ScriptableObjects.MoneyFormats
{
    public static class MoneyFormatScriptableObjectMenuAction
    {
        [MenuItem(ScriptableObjectMenuItemPaths.MoneyFormatScriptableObject)]
        public static void CreateMyAsset()
        {
            var asset = ScriptableObject.CreateInstance<MoneyFormatScriptableObject>();

            AssetDatabase.CreateAsset(asset, ScriptableObjectCreatePathConstants.MoneyFormatScriptableObject);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }   
    }
}