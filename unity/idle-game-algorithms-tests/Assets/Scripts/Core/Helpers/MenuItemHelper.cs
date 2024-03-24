using UnityEditor;
using UnityEngine;

namespace Core.Helpers
{
    public static class MenuItemHelper
    {
        public static void Create<T>(string path) where T: ScriptableObject
        {
            var asset = ScriptableObject.CreateInstance<T>();

            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }
    }
}