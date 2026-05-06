using UnityEngine;

public class ProfilePanelController : PanelController
{
    

    [SerializeField] private ProfileView _profileView;
    

    public override void Show()
    {
        base.Show();
        _profileView = PanelInstance.GetComponentInChildren<ProfileView>();
        
        UpdateViews();
    }

    private void UpdateViews()
    {
        
        Debug.Log($"studioIndex: {_activeCharacter.Data.studioIndex}, array length: {_activeCharacter.Config.studioBackground.Length}");
        Sprite bg = _activeCharacter.Config.studioBackground[_activeCharacter.Data.studioIndex];
        Sprite portrait = _activeCharacter.Config.idleSprite;
        var name = _activeCharacter.Config.name;
        var followers = _activeCharacter.Data.followers.ToString("F0");
        var style = _activeCharacter.Config.id.ToString();
            
        _profileView.Setup(name, style, followers, portrait, bg);
            
    }

}
