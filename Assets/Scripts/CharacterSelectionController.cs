using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterSelectionController : MonoBehaviour
{

    [Header("UI Components")]
    [SerializeField] private SpriteRenderer baseSprite;
    [SerializeField] private TextMeshProUGUI danceStyle;

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
        danceStyle.text = character.danceStyle;
        Debug.Log($"Switched to Character: {character.danceStyle}  | Index: {index} | High Score: {character.highScore}");
    }
}
