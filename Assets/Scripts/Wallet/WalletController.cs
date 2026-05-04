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

    protected override void Awake()
    {
        wallet = Wallet.Instance;
        AddEvents(); // has to be called before show, as we subscribe inside base class
        base.Awake();

        coinsView.Show(Wallet.Instance.Data.coins);
    }

    private void OnEnable()
    {
        SubscribeToEvents(true);
    }

    private void OnDisable()
    {
        SubscribeToEvents(false);
    }

    protected override void AddEvents()
    {
        RegisterEvent(
                      () => coinsView.OnCoinIncrease += HandleCoinIncrease,
                      () => coinsView.OnCoinIncrease -= HandleCoinIncrease
                     );
    }

    private void HandleCoinIncrease(int coin)
    {
        Debug.Log(coin);
        wallet.IncreaseCoins(coin);
        coinsView.Show(Wallet.Instance.Data.coins);

    }
}