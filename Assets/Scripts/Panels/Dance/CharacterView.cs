using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private Image characterSprite;
    // CatalogueModel _data;
    // private CharacterModel character;

    [Header("On dance move animation")] [SerializeField]
    private float timePassed = 0f;
    [SerializeField] private float timeAvailable = 0.2f;
    [SerializeField] private float wiggleAmount = 15f;
    
    
    // added them momentarily here - after will put in audio manager


    // private void Awake()
    // {
    //     Debug.Log("CharacterView.Awake");
    //     // _data = GameManager.Instance.Catalogue;
    //     // character = _data.GetActiveCharacter();
    //     // Debug.Log("Active character config: " + character.Name + " | OnFailSprite: " + character.OnFailSprite);
    // }

    // private void Start()
    // {
    //     SetSprite();
    // }

    public void SetSprite(Sprite idle)
    {
        characterSprite.sprite = idle;
    }

    public void ShowDanceMove(Sprite moveSprite, Sprite idleSprite)
    {
        StartCoroutine(MoveAnimation(moveSprite, idleSprite));
    }
    

    private IEnumerator MoveAnimation(Sprite moveSprite, Sprite idleSprite)
    {
        characterSprite.sprite = moveSprite;

        Vector3 startPosition = characterSprite.transform.localPosition;
        timePassed = 0f;
        timeAvailable = 0.2f;
        wiggleAmount = 15f; // degrees
        
        while (timePassed < timeAvailable)
        {
            timePassed += Time.deltaTime;
            float progress = timePassed / timeAvailable;
            float decay = 1f - progress; // starts strong, fades out
            float offset = Mathf.Sin(progress * Mathf.PI * 6) * wiggleAmount * decay;
            characterSprite.transform.localPosition = startPosition + new Vector3(offset, 0, 0);
            yield return null;
        }

        characterSprite.transform.localPosition = startPosition;
        characterSprite.sprite = idleSprite;
    }
}