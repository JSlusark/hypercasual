using System;
using UnityEngine;
using UnityEngine.UI;


public class DancePanelController : PanelController
{
    [SerializeField] private CharacterView characterSprite;
    private Button _danceSessionButton;
    [SerializeField] private GameObject danceSessionController;


    private void Awake()
    {
    }

    public override void Show()
    {
        hasSubPanel = true;
        base.Show();
        _danceSessionButton = PanelInstance.GetComponentInChildren<Button>();
        _danceSessionButton.onClick.AddListener(() => TriggerPanelLayer(danceSessionController, false));
        
        characterSprite = PanelInstance.GetComponentInChildren<CharacterView>();
        characterSprite.ShowIdle(GameManager.Instance.SelectedCharacter.idleSprite);
    }

    public override void Hide()
    {
        base.Hide();
        hasSubPanel = false;
    }
}