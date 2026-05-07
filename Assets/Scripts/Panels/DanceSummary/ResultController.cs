using TMPro;
using UnityEngine;

public class ResultController : MonoBehaviour
{

    [SerializeField] private ResultView resultView;
    
    [SerializeField] DanceSession danceSessionConfig; 
    
    void Start()
    {

        Debug.Log($"[Wallet model] Coins in wallet");
        Wallet.Instance.AddCoins(danceSessionConfig.coins);
        
        
        var rounds = danceSessionConfig.rounds.ToString();
        var followers = (danceSessionConfig.rounds * danceSessionConfig.points).ToString("F0");
        var tips = danceSessionConfig.coins.ToString();
        
        resultView.Show(rounds, followers, tips);
       
        Character character = CharacterCatalogue.Instance.activeCharacter;
        character.UpdateExperience(danceSessionConfig.rounds, danceSessionConfig.points);
        
    }

}
