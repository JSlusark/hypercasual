using System;
using UnityEngine;

public class Wallet : Singleton<Wallet>
{
    
    public event Action<int> OnCoinsUpdate;
    
    
    public WalletData Data { get; private set; }
    public WalletConfig Config { get; private set; }


    protected override void Initialize()
    {
        Data = SaveSystem.Instance.SaveData.wallet;
        Config = ConfigManager.Instance.wallet;
    }

    public void AddCoins(int amount)
    {
        // Debug.Log($"[WALLET MODEL] Adding {amount} coins to wallet");
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
        // Debug.Log($"[WALLET MODEL] Coins in Wallet updated {Data.coins}");   
        OnCoinsUpdate?.Invoke(Data.coins);
    }


    public bool RemoveCoins(int amount)
    {
        if (Data.coins < amount) return false; // avoiding negative values
        Data.coins -= amount;
        OnCoinsUpdate?.Invoke(Data.coins);
        return true;
    }
}