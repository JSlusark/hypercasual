using System;
using DefaultNamespace.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;


public class DancePanelController : PanelController
{
    [SerializeField] private CharacterView _characterView;
    
    
    public override void Show()
    {
        base.Show();
        _characterView = PanelInstance.GetComponentInChildren<CharacterView>();
        _characterView.SetSprite(character.IdleSprite);
    }


    public override void Hide()
    {
        base.Hide();
    }
}