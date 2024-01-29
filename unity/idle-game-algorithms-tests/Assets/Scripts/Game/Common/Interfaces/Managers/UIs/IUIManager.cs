using TMPro;
using UnityEngine;

namespace Game.Common.Interfaces.Managers.UIs
{
    public interface IUIManager : IGameManager
    {
        public void InitializeUIElements();
        // public void AddUIElement(); TODO:
        // public void RemoveUIElementFromScreen { get; set; }
        // public void RemoveUIElementFromParent { get; set; }
        // public GameObject AddUIElementAsAChild();
        public void UpdateTextElement(TextMeshPro textMeshPro, string newText);
    }
}
