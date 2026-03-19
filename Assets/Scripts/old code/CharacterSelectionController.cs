using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class CharacterSelectionController : MonoBehaviour
{

    [Header("UI Components")]
    [SerializeField] private Image idleSprite;
    [SerializeField] private TextMeshProUGUI danceStyle;
    [SerializeField] private TextMeshProUGUI dancerName;
    [SerializeField] private TextMeshProUGUI dancerPrice;

    [Header("Script Data")]
    private int listLength;
    private int index;

    private void Start()
    {
        listLength = OldGameManager.Instance.CharacterList.Length;
        index = OldGameManager.Instance.Index;
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
        OldGameManager.Instance.LoadCharacter(index);
        SceneManager.LoadScene("LevelScene");
    }

    private void ShowCharacterUI()
    {
        CharacterData character = OldGameManager.Instance.CharacterList[index];
        idleSprite.sprite = character.idleSprite;
        
        // if(!character.isUnlocked)
        // {
        //     idleSprite.color = new Color(0f, 0f, 0f, 0.5f); // 50% alpha
        //     dancerPrice.text = $"{character.costToUnlock} $";
        //     dancerName.text = "???";
        //     danceStyle.text = "Unknown";
        //     
        // }
        // else
        // { 
        //     idleSprite.color = Color.white;
        //     dancerPrice.text = "Select";
        //     danceStyle.text = character.danceStyle;
        //     dancerName.text = character.dancerName;
        // }
        // Debug.Log($"Switched to Character: {character.danceStyle}  | Index: {index} | High Score: {character.highScore}");
    }
}
