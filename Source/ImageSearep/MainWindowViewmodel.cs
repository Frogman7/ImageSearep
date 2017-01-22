namespace ImageSearep
{
    using ImageSearep.Navigation;

    public class MainWindowViewmodel
    {
        public IViewManager ViewNavigator { get; }

        public MainWindowViewmodel(IViewManager viewNavigator)
        {
            this.ViewNavigator = viewNavigator;
        }
    }
}
