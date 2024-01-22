using System;
using UnityEditor;
using Object = UnityEngine.Object;

namespace Core.Helpers
{
    [Serializable]
    public class AssetsHelper
    {
        public static T[] FindAssetsByType<T>() where T : Object
        {
            var guids = FindAssets(typeof(T));
            var assets = new T[guids.Length];

            for (int i = 0; i < guids.Length; i++)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                assets[i] = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            }

            return assets;
        }

        public static T FindAssetByType<T>() where T : Object
        {
            var guid = FindAssets(typeof(T))[0];
            var assetPath = AssetDatabase.GUIDToAssetPath(guid);
            
            return AssetDatabase.LoadAssetAtPath<T>(assetPath);
        }

        private static string[] FindAssets(Type type)
        {
            return AssetDatabase.FindAssets($"t:{type}");
        }
    }
}