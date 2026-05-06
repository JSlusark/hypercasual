using TMPro;
using UnityEngine;

public class ResultController : MonoBehaviour
{

    [SerializeField] private ResultView resultView;
    
    [SerializeField] Score scoreConfig; 
    
    void Start()
    {
        var rounds = scoreConfig.rounds.ToString();
        var followers = (scoreConfig.rounds * scoreConfig.points).ToString("F0");
        var tips = "N/A";
        
        resultView.Show(rounds, followers, tips);
       
        Character character = CharacterCatalogue.Instance.activeCharacter;
        character.UpdateExperience(scoreConfig.rounds, scoreConfig.points);
        
    }

}
