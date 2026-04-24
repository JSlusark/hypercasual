using System;

/*
 * Called Plain old C# Objects(POCO) or Data Containers.
 * Just holds variables so that Unity can read them and write them to a text file.
 *
 * SaveGameData keeps global save data for the whole game.
 */

[Serializable]
public class SaveData
{
    public DatabaseData databaseData = new DatabaseData();
    /*
     *  public SettingsData settingsData
     *  public WalletData Wallet
     *   public Wallet Wallet = new Wallet();
     *
     */
}