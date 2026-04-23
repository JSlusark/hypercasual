using System;
using DefaultNamespace.ScriptableObjects;
using SaveSystem.Character;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private Image characterSprite;
    DatabaseModel _data;

    private void Awake()
    {
        _data = GameManager.Instance.Database;
    }

    private void Start()
    {
        ShowIdle(); // fallback?
    }

    public void ShowIdle()
    {
        CharacterModel character = _data.GetActiveCharacter();
            characterSprite.sprite = character.IdleSprite;
    }
}
