using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/*
    Controllers(logic) mediates between Model(data) and View(UI).
    Orchestrates logic: Reacts to input, events, scene lifecycle.

    [SerializeField] : makes private fields visible and editable in the Unity Inspector.
 */

public class SelectionController : MonoBehaviour
{


    [Header("Character Definitions")]
    // [SerializeField] private SelectionState.characterList[] characterList;
    // [SerializeField] private DataLayer.characterList[] characterList;


    [Header("UI")]
    [SerializeField] private SpriteRenderer characterSprite;
    [SerializeField] private TextMeshProUGUI danceStyleName;



    private int currentIndex = 0;

    private void Start()
    {
        ShowCharacter(0);
    }

    public void NextCharacter()
    {
        // currentIndex = (currentIndex + 1) % characterList.Length;
        currentIndex = (currentIndex + 1) % DataLayer.Instance.characterList.Length;
        ShowCharacter(currentIndex);
    }

    public void PreviousCharacter()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = DataLayer.Instance.characterList.Length - 1;
        // currentIndex = characterList.Length - 1;

        ShowCharacter(currentIndex);
    }


    public void PlayGame()
    {
        // SelectionState.Instance.selectionData.characterIndex = currentIndex;
        // SelectionState.Instance.selectionData.characterIndex = currentIndex;
        SceneManager.LoadScene("LevelScene");
    }

    private void ShowCharacter(int index)
    {
        DataLayer.Instance.selectedCharacter = DataLayer.Instance.characterList[index];
        characterSprite.sprite = DataLayer.Instance.selectedCharacter.characterSprite;
        danceStyleName.text = DataLayer.Instance.selectedCharacter.characterDanceStyle;
        // characterSprite.sprite = characterList[index].characterSprite;
        // danceStyleName.text = characterList[index].characterDanceStyle;
    }

    // /*
    // Layers:
    // - Persistent game state - CharacterData.
    //     Keeps track of the global game state for characters:Selected character/skin, Experience points, Unlock status, has to be carried across scenes. So it has to be a singleton.
    // - Selection UI state: MonoBehaviours, ui widgets, buttons, etc. Changes during runtime as the player interacts with the selection screen. So it has to be normal instance variables.
    // - Character definition data: characterList.
    //     Does not change during runtime, just holds the data for all characters to read from. So it can be static, why? Because it's shared across all instances and doesn't need to be duplicated.
    //     But it does not need to be shared across scenes because it is used only in the selection scene so it can be a normal instance variable. However this data contains info on specific
    //     characters that are used in the game scene and every character has also their experience points and unlock status tracked globally. So this data can be used to initialize the global game state for characters when the game starts.
    //  */

    // private static readonly int characterTotal = 3;
    // private int selectedSkin = 0;

    // /*
    // Persistent game state stores IDs, indices, or keys.
    // Assets are resolved at runtime from definition data.
    // */
    // [System.Serializable]
    // public struct CharacterData // Persistent game state - singleton - depends on characterList - must not store scene objects or prefabs directly (breaks save/oad, breaks scene transitions, serialization and couples object to statee)
    // {
    //     public int characterIndex; // index of the selected character
    //     // however this also will have all exp points and unlock status for each character played not only the selected ones

    // }

    // [System.Serializable]
    // public struct characterList // Character definition data - static - does not depend on anything
    // {
    //     public string[] danceStyles;
    //     public Sprite[] characterSprites;
    //     public GameObject characterSelected; // prefab of the player skin
    // }

    // // UI depends on both CharacterData and characterList
    // [System.Serializable]
    // public struct CharacterSelectionUI
    // {
    //     public characterSprite characterRender; // sprite renderer of the player skin
    //     [SerializeField] public TextMeshProUGUI nameRander;
    // }


    // // public struct SelectionView
    // // {
    // //     public GameObject playerSkin; // prefab of the player skin
    // //     [SerializeField] public TextMeshProUGUI skinName;
    // // }


    // // public characterSprite sr; // sprite renderer of the player skin

    // // // // Start is called once before the first execution of Update after the MonoBehaviour is created
    // // void Start()
    // // {
    // //     skinName.text = spriteNames[selectedSkin];

    // // }

    // // // // Update is called once per frame
    // // // void Update()
    // // // {

    // // // }

    // // public void SwipeLeft()
    // // {
    // //     if (selectedSkin == 0)
    // //         selectedSkin = totalSkins - 1;
    // //     else
    // //         selectedSkin--;
    // //     sr.sprite = spriteOptions[selectedSkin];
    // //     skinName.text = spriteNames[selectedSkin];

    // //     Debug.Log($"Swipe Left - Chara[{selectedSkin}]:{sr.sprite.name}");
    // // }

    // // public void SwipeRight()
    // // {
    // //     if (selectedSkin == totalSkins - 1)
    // //         selectedSkin = 0;
    // //     else
    // //         selectedSkin++;
    // //     sr.sprite = spriteOptions[selectedSkin];
    // //     skinName.text = spriteNames[selectedSkin];
    // //     Debug.Log($"Swipe Right - Chara[{selectedSkin}]:{sr.sprite.name}");
    // // }


    // // public void PlayGame()
    // // {
    // //     PrefabUtility.SaveAsPrefabAsset(playerSkin, "Assets/Prefabs/SelectedChar.prefab"); // PlayerPrefs should be used here instead
    // //     Debug.Log($"Selected skin prefab saved. Loading LevelScene with Chara[{selectedSkin}]:{sr.sprite.name}");
    // //     SceneManager.LoadScene("LevelScene");
    // // }

}
