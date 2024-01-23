using Game.Common.Constants.ScriptableObjects;
using UnityEditor;
using ScriptableObject = UnityEngine.ScriptableObject;

namespace Game.ScriptableObjects.StageConfigs
{
    public static class StageConfigScriptableObjectMenuAction 
    {
        [MenuItem(ScriptableObjectMenuItemPaths.StageConfigScriptableObject)]
        public static void CreateMyAsset()
        {
            var asset = ScriptableObject.CreateInstance<StageConfigScriptableObject>();

            AssetDatabase.CreateAsset(asset, ScriptableObjectCreatePathConstants.StageConfigScriptableObject);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }   
    }
}