using UnityEngine;
using DefaultNamespace.ScriptableObjects;

public class CharacterModel
{
    public CharacterConfig Config { get; private set; }
    public CharacterData Data { get; private set; }

    public CharacterModel(CharacterConfig config, CharacterData data)
    {
        Config = config;
        Data = data;
    }

    public void Unlock()
    {
        if (!Data.isUnlocked)
            Data.isUnlocked = true; // before unlocking should check with wallet
            // Debug.Log($"{Config.dancerName} has been unlocked!");
    }
}