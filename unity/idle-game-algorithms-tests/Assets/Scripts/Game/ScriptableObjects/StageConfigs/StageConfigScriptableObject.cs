using UnityEngine;

namespace Game.ScriptableObjects.StageConfigs
{
    public class StageConfigScriptableObject : ScriptableObject
    {
        [SerializeField] private string stageName = "Stage";
        [SerializeField] private Sprite icon = null;
        [SerializeField] private float initialMachineCost = 5;
        [SerializeField] private int quantityOfMachines = 1;
        [SerializeField] private float levelDifficulty = 1;

        public string StageName => stageName;
        public Sprite Icon => icon;
        public float InitialMachineCost => initialMachineCost;
        public int QuantityOfMachines => quantityOfMachines;
        public float LevelDifficulty => levelDifficulty;
    }
}
