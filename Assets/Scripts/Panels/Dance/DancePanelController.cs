using System;
using SaveSystem.Character;
using UnityEngine;
using UnityEngine.UI;


public class DancePanelController : PanelController
{
    [SerializeField] private CharacterView characterSprite;
    
    public override void Show()
    {
        base.Show();

        characterSprite = PanelInstance.GetComponentInChildren<CharacterView>();
        
        // CAN BE TAKEN FROM CHARACTERLISTMODEL.getactiveCharacter no?ß
        // CharacterModel activeDancer = GameManager.Instance.GetActiveCharacter();

        // if (activeDancer != null)
        // {
        //     characterSprite.ShowIdle(activeDancer.IdleSprite);
        // }
    }


    public override void Hide()
    {
        base.Hide();
    }
}