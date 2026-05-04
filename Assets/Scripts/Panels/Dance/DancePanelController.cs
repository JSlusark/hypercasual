using System;
using DefaultNamespace.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;


public class DancePanelController : PanelController
{
    [SerializeField] private CharacterView _characterView;
    [SerializeField] private WalletController _walletController;

    
    public override void Show()
    {
        base.Show();
        _characterView = PanelInstance.GetComponentInChildren<CharacterView>();
        _characterView.SetSprite(_characterCatalogue.activeCharacter.Config.idleSprite);

        _walletController = PanelInstance.GetComponentInChildren<WalletController>();
    }


    public override void Hide()
    {
        base.Hide();
        
    }
}