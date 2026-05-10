using DefaultNamespace.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(fileName = "Wallet", menuName = "ScriptableObject/WalletConfig")]
public class WalletConfig : ScriptableObject
{
    public int maxCoins = 9999999;
}