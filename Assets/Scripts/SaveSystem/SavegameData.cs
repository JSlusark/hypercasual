using System;
using System.Collections.Generic;
using DefaultNamespace.ScriptableObjects;
using SaveSystem.Character;

/*
 * Called Plain old C# Objects(POCO) or Data Containers.
 * Just holds variables so that Unity can read them and write them to a text file.
 * 
 * SaveGameData keeps global save data for the whole game.
 */

[Serializable]
public class SavegameData
{
   // public PlayerData PlayerData = new PlayerData();
   public DatabaseData databaseData = new DatabaseData();
   /*
    *  public SettingsData settingsData
    * 
    */

}