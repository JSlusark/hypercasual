namespace DefaultNamespace
{
    public interface IScreen
    {
        /*
         These are generic methods: they take a type parameter T, which must refer to a subclass of PanelController (ShopPanelController, DancePanelController, etc.).
         When you call ShowScreen<T>(), you specify which screen you want to show by providing the type of its controller.
         For example, ShowScreen<DancePanelController>() will show the play screen. The same applies to HideScreen<T>().
         It will show the screen associated with that controller.
         
        */
        void ShowScreen<T>() where T : PanelController; // Show can be used only from T that inherit from PanelController
        void HideScreen<T>() where T : PanelController;
    }
}