using System;
using DefaultNamespace.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;


public class PlayController : PanelController
{
    [SerializeField] private CharacterView _characterView;
    [SerializeField] private BackgroundView _backgroundView;

    protected override void OnAwake()
    {
        MenuBarManager.Instance.Show();
    }

    private void Start()
    {
        _characterView.SetSprite(_characterCatalogue.activeCharacter.Config.idleSprite);
        _backgroundView.Show(_characterCatalogue.activeCharacter.Config
                                                .reelBackground
                                                    [0]); // 0 by default, meant to change in Dance Summary as rounds progress
    }

    protected override void SubscribeToEvents(bool isSubscribed)
    {
    }
}