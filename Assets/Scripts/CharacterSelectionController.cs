using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class CharacterSelectionController : MonoBehaviour
{

    [Header("UI Components")]
    [SerializeField] private Image baseSprite;
    [SerializeField] private TextMeshProUGUI danceStyle;
    [SerializeField] private TextMeshProUGUI dancerName;
    [SerializeField] private TextMeshProUGUI dancerPrice;

    [Header("Script Data")]
    private int listLength;
    private int index;

    private void Start()
    {
        listLength = GameManager.Instance.CharacterList.Length;
        index = GameManager.Instance.Index;
        ShowCharacterUI();
    }

    public void NextCharacter()
    {
        index = (index + 1) % listLength;
        ShowCharacterUI();
    }

    public void PreviousCharacter()
    {
        index--;
        if (index < 0)
            index = listLength - 1;

        ShowCharacterUI();
    }


    public void PlayGame()
    {
        GameManager.Instance.LoadCharacter(index);
        SceneManager.LoadScene("LevelScene");
    }

    private void ShowCharacterUI()
    {
        CharacterData character = GameManager.Instance.CharacterList[index];
        baseSprite.sprite = character.baseSprite;
        
        if(!character.isUnlocked)
        {
            baseSprite.color = new Color(0f, 0f, 0f, 0.5f); // 50% alpha
            dancerPrice.text = $"{character.costToUnlock} $";
            dancerName.text = "???";
            danceStyle.text = "Unknown";
            
        }
        else
        { 
            baseSprite.color = Color.white;
            dancerPrice.text = "Select";
            danceStyle.text = character.danceStyle;
            dancerName.text = character.dancerName;
        }
        Debug.Log($"Switched to Character: {character.danceStyle}  | Index: {index} | High Score: {character.highScore}");
    }
}
