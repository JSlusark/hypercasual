using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/*
    Controllers(logic) mediates between Model(data) and View(UI).
    Orchestrates logic: Reacts to input, events, scene lifecycle.
    [SerializeField] : makes private fields visible and editable in the Unity Inspector.
 */

public class CharacterSelectionController : MonoBehaviour
{

    [Header("UI Components")]
    [SerializeField] private SpriteRenderer baseSprite;
    [SerializeField] private TextMeshProUGUI danceStyleName;

    // [Header("Script Data")]
    private int listLength;
    private int selection;

    private CharacterData activeCharacter;


    private void Start()
    {
        selection = GameManager.Instance.GetCharacterIndex;
        listLength = GameManager.Instance.characterList.Length;
        ShowCharacter();
    }

    public void NextCharacter()
    {

        selection = (selection + 1) % listLength;
        ShowCharacter();
    }

    public void PreviousCharacter()
    {
        selection--;
        if (selection < 0)
            selection = listLength - 1;
        // selection = characterList.Length - 1;

        ShowCharacter();
    }


    public void PlayGame()
    {
        GameManager.Instance.SaveActiveCharacter(selection);
        SceneManager.LoadScene("LevelScene");
    }

    private void ShowCharacter()
    {
        activeCharacter = GameManager.Instance.characterList[selection];
        baseSprite.sprite = activeCharacter.baseSprite;
        danceStyleName.text = activeCharacter.danceStyleName;
    }


}
