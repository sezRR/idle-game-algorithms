using Game.ScriptableObjects.StageConfigs;

namespace Game.Common.Interfaces.Managers.UIs
{
    public interface IUIManager : IGameManager
    {
        public void InitializeUIElements(StageConfigScriptableObject stageConfigScriptableObject);
        // public void AddUIElement(); TODO:
        // public void RemoveUIElementFromScreen { get; set; }
        // public void RemoveUIElementFromParent { get; set; }
        // public GameObject AddUIElementAsAChild();
    }
}
