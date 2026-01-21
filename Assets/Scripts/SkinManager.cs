using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SkinManager : MonoBehaviour
{
    public SpriteRenderer sr; // sprite renderer of the player skin

    private static readonly int totalSkins = 3;
    private int selectedSkin = 0;
    public Sprite[] skins = new Sprite[totalSkins];
    public string[] skinNames = new string[totalSkins];
    public GameObject playerSkin; // prefab of the player skin
    [SerializeField] public TextMeshProUGUI skinName;

    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        skinName.text = skinNames[selectedSkin];

    }

    // // Update is called once per frame
    // void Update()
    // {

    // }

    public void SwipeLeft()
    {
        if (selectedSkin == 0)
            selectedSkin = totalSkins - 1;
        else
            selectedSkin--;
        sr.sprite = skins[selectedSkin];
        skinName.text = skinNames[selectedSkin];

        Debug.Log($"Swipe Left - Chara[{selectedSkin}]:{sr.sprite.name}");
    }

    public void SwipeRight()
    {
        if (selectedSkin == totalSkins - 1)
            selectedSkin = 0;
        else
            selectedSkin++;
        sr.sprite = skins[selectedSkin];
        skinName.text = skinNames[selectedSkin];
        Debug.Log($"Swipe Right - Chara[{selectedSkin}]:{sr.sprite.name}");
    }


    public void PlayGame()
    {
        PrefabUtility.SaveAsPrefabAsset(playerSkin, "Assets/Prefabs/SelectedChar.prefab"); // PlayerPrefs should be used here instead
        Debug.Log($"Selected skin prefab saved. Loading LevelScene with Chara[{selectedSkin}]:{sr.sprite.name}");
        SceneManager.LoadScene("LevelScene");
    }

}
