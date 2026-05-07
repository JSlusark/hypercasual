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
    private AudioSource audiosource;

    protected override void OnAwake()
    {
        AddEvents();
        Debug.Log($"[{GetType().Name}] Initializing {nameof(WalletController)}");
    }

    private void Start()
    {
        coinsView.Show(Wallet.Instance.Data.coins);
    }

    protected override void Enable() // <-------------------------- quick workaround - i will change this!!!
    {
        coinsView.Show(Wallet.Instance.Data.coins);
        
    }


    protected override void AddEvents()
    {
        RegisterEvent(nameof(coinsView.OnCoinsTopUp),
                                 () => coinsView.OnCoinsTopUp += HandleCoinsTopUp,
                             () => coinsView.OnCoinsTopUp -= HandleCoinsTopUp
                            );
        RegisterEvent(nameof(Wallet.Instance.OnCoinsUpdate),
                                 () => Wallet.Instance.OnCoinsUpdate += HandleCoinsView,
                             () => Wallet.Instance.OnCoinsUpdate -= HandleCoinsView
                            );
    }

    private void HandleCoinsTopUp(int coin)
    {
        Debug.Log(coin);
        Wallet.Instance.AddCoins(coin);
    }
    
    private void HandleCoinsView(int coin)
    {
        Debug.Log($"CoinsView updated with {coin}");
        coinsView.Show(Wallet.Instance.Data.coins);
    }
}