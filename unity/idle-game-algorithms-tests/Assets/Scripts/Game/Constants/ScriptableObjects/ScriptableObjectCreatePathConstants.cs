namespace Game.Constants.ScriptableObjects
{
    public static class ScriptableObjectCreatePathConstants
    {
        public const string StageConfigScriptableObject = ScriptableObjectBaseConstants.BaseCreatePath 
                                                          + ScriptableObjectDefaultFolderNameConstants.StageConfigScriptableObject
                                                          + ScriptableObjectDefaultFileNameConstants.StageConfigScriptableObject 
                                                          + ScriptableObjectBaseConstants.BaseScriptableObjectFileExtension;
        
        public const string MoneyFormatScriptableObject = ScriptableObjectBaseConstants.BaseCreatePath 
                                                          + ScriptableObjectDefaultFolderNameConstants.MoneyFormatScriptableObject 
                                                          + ScriptableObjectDefaultFileNameConstants.MoneyFormatScriptableObject 
                                                          + ScriptableObjectBaseConstants.BaseScriptableObjectFileExtension;
    }
}