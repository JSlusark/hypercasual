using DefaultNamespace.ScriptableObjects;
using Panels.Roster.Views;
using UnityEngine;

public class RosterController : PanelController
{
    [SerializeField] private PreviewCard previewCard;
    [SerializeField] private NavigationButtons[] navigationButtons;
    [SerializeField] private SelectButtonText _selectButtonText;

    private NavigationModel _navigationModel;
    private CharacterID _previewID;


    protected override void OnAwake()
    {
        _previewID = _activeCharacterID;
        _navigationModel = new NavigationModel(ConfigManager.Instance.CharacterCatalogue, _activeCharacterID);
    }

    private void Start()
    {
        UpdateViews(_activeCharacterID);
    }
    
    
    protected override void SubscribeToEvents(bool isSubscribed)
    {
        foreach (var button in navigationButtons)
        {
            if (isSubscribed)
                button.OnNavigationButton += HandleNavigationNavigationButton;
            else
                button.OnNavigationButton -= HandleNavigationNavigationButton;
        }
    }

    private void
        HandleNavigationNavigationButton(
            NavigationButtons.Request request) // moved list navigation logic in its own model class
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
                _characterCatalogue.SetActiveCharacter(_previewID);
                break;
        }

        UpdateViews(_previewID);
    }

    private void UpdateViews(CharacterID id)
    {
        Character pointedCharacter = _characterCatalogue.GetCharacter(id);
        previewCard.Show(pointedCharacter, _characterCatalogue.IsActive(id));
        // _selectButtonText.UpdateText(pointedCharacter);
    }
}