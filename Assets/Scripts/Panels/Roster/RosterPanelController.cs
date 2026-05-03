using DefaultNamespace.ScriptableObjects;
using Panels.Roster.Views;

public class RosterPanelController : PanelController
{
    private PreviewCard _previewCard;
    private NavigationButtons[] _buttons;
    private NavigationModel _navigationModel;
    private CharacterID _previewID;
    private SelectButtonText _selectButtonText;


    public override void Show()
    {
        base.Show();
        AssignFields();
        SubscribeToEvents(true);
        UpdateViews(ActiveCharacterID);
    }
    public override void Hide()
    {
        SubscribeToEvents(false);
        base.Hide();
    }
    private void AssignFields()
    {
        _previewID = ActiveCharacterID; 
        
        // Models
        _navigationModel = new NavigationModel(CharacterCatalogue.configList, ActiveCharacterID);
        
        //Views
        _previewCard = PanelInstance.GetComponentInChildren<PreviewCard>();
        _buttons = PanelInstance.GetComponentsInChildren<NavigationButtons>();
        _selectButtonText =  PanelInstance.GetComponentInChildren<SelectButtonText>();
    }


    private void SubscribeToEvents(bool subscribe)
    {
        foreach (var button in _buttons)
        {
            if (subscribe)
                button.OnNavigationButton += HandleNavigationNavigationButton;
            else
                button.OnNavigationButton -= HandleNavigationNavigationButton;
        }
    }
    
    private void
        HandleNavigationNavigationButton(NavigationButtons.Request request) // moved list navigation logic in its own model class
    {
        switch (request)
        {
            case NavigationButtons.Request.ShowPrevious:
                _previewID = _navigationModel.Previous();
                break;
            case NavigationButtons.Request.ShowNext:
                _previewID = _navigationModel.Next();
                break;
            case NavigationButtons.Request.SelectCharacter:
                CharacterCatalogue.SetActiveCharacter(_previewID);
                break;
        }

        UpdateViews(_previewID);
        
    }

    private void UpdateViews(CharacterID id)
    {
        CharacterModel character = CharacterCatalogue.GetCharacter(id); 
        _previewCard.Show(character, CharacterCatalogue.IsActive(id));
        _selectButtonText.UpdateText(character, CharacterCatalogue.IsActive(id));
    }
    
}