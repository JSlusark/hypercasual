using DefaultNamespace.ScriptableObjects;
using Panels.Roster.Views;

public class RosterPanelController : PanelController
{
    private PreviewCard _previewCard;
    private NavigationButtons[] _buttons;
    private NavigationModel _navigationModelModel;
    private DatabaseModel _data;
    private CharacterID _previewID;
    private SelectButtonText _selectButtonText;


    public override void Show()
    {
        base.Show();
        AssignFields();
        SubscribeToEvents(true);
        UpdateViews(_data.Data.activeCharacterId);
    }
    public override void Hide()
    {
        SubscribeToEvents(false);
        base.Hide();
    }
    private void AssignFields()
    {
        // Models
        _data = GameManager.Instance.Database;
        _navigationModelModel = new NavigationModel(_data);
        
        //Views
        _previewCard = PanelInstance.GetComponentInChildren<PreviewCard>();
        _buttons = PanelInstance.GetComponentsInChildren<NavigationButtons>();
        _selectButtonText =  PanelInstance.GetComponentInChildren<SelectButtonText>();
    }

    private void UpdateViews(CharacterID characterID)
    {
        // Start component change
        _previewCard.ShowCharacter(_data.GetCharacter(characterID));
        _selectButtonText.UpdateText(_data.GetCharacter(characterID));
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
                _previewID = _navigationModelModel.Previous();
                break;
            case NavigationButtons.Request.ShowNext:
                _previewID = _navigationModelModel.Next();
                break;
            case NavigationButtons.Request.SelectCharacter:
                _data.SetActiveCharacter(_previewID);
                break;
        }

        UpdateViews(_previewID);
        // _previewCard.ShowCharacter(_data.GetCharacter(_previewID));
        // _selectButtonText.UpdateText(_data.GetCharacter(_previewID));
        
    }
}