using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*Formatting of 1054.32179:
N:                     1,054.32
N0:                    1,054
N1:                    1,054.3
N2:                    1,054.32
N3:                    1,054.322 */

public class CoinsView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private Button coinsTopUpButton;

    public event Action<int> OnCoinsTopUp;

    public void Show(float amount)
    {
        var text = "$" + amount.ToString("N0");
        coinsText.text = text;
    }

    public void onAddCoins()
    {
        OnCoinsTopUp?.Invoke(100);
    }
}