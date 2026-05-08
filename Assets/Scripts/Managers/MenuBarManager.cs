using MenuBars;
using UnityEngine;

public class MenuBarManager : Manager<MenuBarManager>
{
    [SerializeField] private RectTransform menuCanvas;
    [SerializeField] private MenuBarController topMenuPrefab;
    [SerializeField] private MenuBarController lowMenuPrefab;
    
    private MenuBarController _topMenu;
    private MenuBarController _lowMenu;

    protected override void OnAwake()
    {
        _topMenu = Instantiate(topMenuPrefab, menuCanvas);
        _lowMenu = Instantiate(lowMenuPrefab, menuCanvas);
        
        _topMenu.Init();
        _lowMenu.Init();

    }

    public void Show()
    {
        _topMenu.gameObject.SetActive(true);
        _lowMenu.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _topMenu.gameObject.SetActive(false);
        _lowMenu.gameObject.SetActive(false);
    }

    public PanelEmitterButton[] GetMenuButtons()
    {
        return _lowMenu.GetComponentsInChildren<PanelEmitterButton>();
    }
    
}
