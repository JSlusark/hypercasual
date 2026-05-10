using System;
using DefaultNamespace;
using DefaultNamespace.ScriptableObjects;
using UnityEngine;

public abstract class PanelController : MonoBehaviour
{
    [Header("Panel Info")]
    public PanelID panelID;
    [Header("PanelEmitterButtons - needed by PanelManager")]
    public PanelEmitterButton[] PanelEmitterButtons;// { get; private set; }
    
    [Header("Catalogue instance")]
    protected CharacterCatalogue _characterCatalogue;
    protected Character _activeCharacter;
    protected CharacterID _activeCharacterID;

    
    private void Awake()
    {
        Debug.LogWarning($"[{name}] is Awake");
        _characterCatalogue = CharacterCatalogue.Instance;
        _activeCharacter = _characterCatalogue.activeCharacter;
        _activeCharacterID = _characterCatalogue.activeCharacter.Data.id;

        OnAwake();
        SubscribeToEvents(true);
    }


    private void OnDestroy()
    {
        SubscribeToEvents(false);
        Debug.LogWarning($"[{name}] is destroyed");
    }
    

    protected abstract void OnAwake();
    protected abstract void SubscribeToEvents(bool isSubscribed);

}