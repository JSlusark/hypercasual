using UnityEngine;

public class ProfileController : PanelController
{
    

    [SerializeField] private DetailsView detailsView;
    

    protected override void OnAwake()
    {
    }

    private void Start()
    {
        UpdateViews();
    }

    protected override void SubscribeToEvents(bool isSubscribed)
    {
    }

    private void UpdateViews()
    {
        Sprite bg = _activeCharacter.Config.studioBackground[_activeCharacter.Data.studioIndex];
        Sprite portrait = _activeCharacter.Config.idleSprite;
        var name = _activeCharacter.Config.name;
        var followers = _activeCharacter.Data.followers.ToString("F0");
        var style = _activeCharacter.Config.id.ToString();
            
        detailsView.Setup(name, style, followers, portrait, bg);
        
    }

}
