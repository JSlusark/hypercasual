using System;
using DefaultNamespace.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;


public class PlayController : PanelController
{
    [SerializeField] private DetailsView characterDataView;


    protected override void OnAwake()
    {
        MenuBarManager.Instance.Show();
    }

    private void Start()
    {
        characterDataView.Setup(_activeCharacter.Config.name, _activeCharacter.Config.id.ToString(),
                                _activeCharacter.Data.followers.ToString("F0"), _activeCharacter.Config.idleSprite,
                                _activeCharacter.Config.reelBackground[0]);

    }

    protected override void SubscribeToEvents(bool isSubscribed)
    {
    }
}