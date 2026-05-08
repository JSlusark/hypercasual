using System;
using DefaultNamespace.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;


public class DancePanelController : PanelController
{
    [SerializeField] private CharacterView _characterView;
    [SerializeField] private WalletController _walletController;
    [SerializeField] private BackgroundView _backgroundView;
    
    public override void Show()
    {
        MenuBarManager.Instance.Show();
        base.Show();
        _walletController = PanelInstance.GetComponentInChildren<WalletController>();
       
        _characterView = PanelInstance.GetComponentInChildren<CharacterView>();
        _backgroundView = PanelInstance.GetComponentInChildren<BackgroundView>();
        
        _characterView.SetSprite(_characterCatalogue.activeCharacter.Config.idleSprite);
        _backgroundView.Show(_characterCatalogue.activeCharacter.Config.reelBackground[0]); // 0 by default, meant to change in Dance Summary as rounds progress
    }


    public override void Hide()
    {
        base.Hide();
        
    }
}