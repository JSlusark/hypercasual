using System;
using DefaultNamespace.ScriptableObjects;
using SaveSystem.Character;
using UnityEngine;
using UnityEngine.UI;


public class DancePanelController : PanelController
{
    [SerializeField] private CharacterView characterSprite;
    
    private DatabaseModel _data;
    private CharacterID _characterID;
    
    public override void Show()
    {
        base.Show();

        // _data = GameManager.Instance.Database;
        // _characterID = _data.Data.activeCharacterId;
        
        characterSprite = PanelInstance.GetComponentInChildren<CharacterView>();
        
        // characterSprite.ShowIdle(_data.GetCharacter(_characterID));
        
    }


    public override void Hide()
    {
        base.Hide();
    }
}