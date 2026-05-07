using System;
using UnityEngine;
using UnityEngine.Serialization;

/*
 * Adding this momentarily as i want the controller to not duplicate
 * Might perhaps want to create a base class for prefabs that need to be
 *
 */

public class WalletController : PrefabController<WalletController>
{
    [SerializeField] private CoinsView coinsView;
    private Wallet wallet;

    protected override void OnAwake()
    {
        wallet = Wallet.Instance;
        AddEvents();
    }

    private void Start()
    {
        coinsView.Show(wallet.Data.coins);
    }

    protected override void AddEvents()
    {
        RegisterEvent(nameof(coinsView.OnCoinIncrease),
                                 () => coinsView.OnCoinIncrease += HandleCoinIncrease,
                             () => coinsView.OnCoinIncrease -= HandleCoinIncrease
                            );
        RegisterEvent(nameof(wallet.OnCoinsUpdate),
                                 () => wallet.OnCoinsUpdate += HandleWalletUpdate,
                             () => wallet.OnCoinsUpdate -= HandleWalletUpdate
                            );
    }

    private void HandleCoinIncrease(int coin)
    {
        Debug.Log(coin);
        wallet.IncreaseCoins(coin);
    }
    
    private void HandleWalletUpdate(int coin)
    {
        coinsView.Show(wallet.Data.coins);
    }
}