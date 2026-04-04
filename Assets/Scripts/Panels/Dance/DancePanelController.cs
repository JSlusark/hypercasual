using System;
using UnityEngine;
using UnityEngine.UI;


public class DancePanelController : PanelController
{
    [SerializeField] private CharacterView characterSprite;
    
    private void Awake()
    {
    }

    public override void Show()
    {
        base.Show();

        characterSprite = PanelInstance.GetComponentInChildren<CharacterView>();
        characterSprite.ShowIdle(GameManager.Instance.SelectedCharacter.idleSprite);
    }


    public override void Hide()
    {
        base.Hide();
    }
}