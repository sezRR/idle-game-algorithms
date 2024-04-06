using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Helpers
{
    public static class UIHelper
    {
        public static void UpdateTextElement(TMP_Text oldText, string newText)
        {
            oldText.text = newText;
        }

        public static float GetCurrentHeight(RectTransform attachRectTransform, GridLayoutGroup gridLayoutGroup) =>
            attachRectTransform.rect.height + gridLayoutGroup.padding.top + gridLayoutGroup.padding.bottom;

        public static T GetComponentFromInnerGameObjectByName<T>(GameObject gameObject, string componentName)
            where T : ICanvasElement =>
            gameObject.transform.Find(componentName).gameObject.GetComponent<T>();
    }
}