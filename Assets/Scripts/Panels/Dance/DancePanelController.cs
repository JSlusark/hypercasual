using UnityEngine;

public class DancePanelController : PanelController {
  
    [SerializeField] private CharacterView characterSprite;
    
    
    
    public override void Show() 
    {
        base.Show();
        characterSprite = PanelInstance.GetComponentInChildren<CharacterView>();
        characterSprite.ShowIdle(GameManager.Instance.SelectedCharacter.idleSprite);
     }

    
}
