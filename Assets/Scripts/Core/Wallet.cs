using System;
using UnityEngine;

public class Wallet : Singleton<Wallet>
{
    public WalletData Data { get; private set; }
    public WalletConfig Config { get; private set; }


    protected override void Initialize()
    {
        Data = SaveSystem.Instance.SaveData.wallet;
        Config = ConfigManager.Instance.wallet;
    }

    public void IncreaseCoins(int amount)
    {
        Debug.Log($"COINS: Data.coins{Data.coins}");
        if (Data.coins + amount == Config.maxCoins)
        {
            Data.maxCoinsReached = true;
            Data.coins = Config.maxCoins;
        }
        else
        {
            Data.maxCoinsReached = false;
            Data.coins += amount;
        }
    }


    public bool DecreaseCoins(int amount)
    {
        if (Data.coins < amount) return false; // avoiding negative values
        Data.coins -= amount;
        return true;
    }
}