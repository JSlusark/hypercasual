using System;
using System.Collections.Generic;
using DefaultNamespace.ScriptableObjects;
using UnityEngine.Serialization;

/*
 * Called Plain old C# Objects(POCO) or Data Containers.
 * Just holds variables so that Unity can read them and write them to a text file.
 *
 * SaveGameData keeps global save data for the whole game.
 */

[Serializable]
public class SaveData
{
    public String PlayerName; // Can convert to playerData with Name, number of dancers, most successful dancer, etc..
    public CharacterID activeCharacterID;
    public List<CharacterData> characterCatalogue; 
    public WalletData wallet;
}