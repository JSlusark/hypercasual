using DefaultNamespace;
using DefaultNamespace.ScriptableObjects;
using UnityEngine;

public abstract class PanelController : MonoBehaviour
{
    protected GameObject PanelInstance;
    
    [Header("Panel Info")]
    public PanelID panelID;
    public bool showsMenuBar;
    [SerializeField] protected GameObject panelPrefab;

    [Header("PanelEmitterButtons - needed by PanelManager")]
    public PanelEmitterButton[] PanelEmitterButtons; /*{ get; private set; }*/ 
    
    
    [Header("Catalogue instance")]
    protected CharacterCatalogue _characterCatalogue;
    protected Character _activeCharacter;
    protected CharacterID _activeCharacterID;
    
    
    [Header("DanceSession from last round")]
    [SerializeField] protected DanceSession danceSessionDanceSession;


    public virtual void Show() 
    {
        if (PanelInstance != null)
        {
            Debug.LogWarning($"[PanelController] {panelPrefab.name} is already instantiated - did you forget to call Hide()?");
            return;
        }

        Debug.Log($"[PanelController] Show: {panelPrefab.name}");
        PanelInstance = Instantiate(panelPrefab);
        PanelEmitterButtons = PanelInstance.GetComponentsInChildren<PanelEmitterButton>(true); // includes inactive children
     
        _characterCatalogue = CharacterCatalogue.Instance;
        _activeCharacter = _characterCatalogue.activeCharacter;
        _activeCharacterID = _characterCatalogue.activeCharacter.Data.id;

        // Debug.Log($"[PanelController] Found {PanelEmitterButtons.Length} PanelEmitterButtons in {panelPrefab.name}");
    }

    public virtual void Hide()
    {
        if (PanelInstance == null)
        {
            // Debug.LogWarning($"[PanelController] {panelPrefab.name} is not currently shown.");
            return;
        }
        
        // Debug.Log($"[PanelController] Hide: {panelPrefab.name}");
        DestroyImmediate(PanelInstance);
        PanelInstance = null;
    }
    
}