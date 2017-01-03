namespace ImageSearep
{
    using ImageSearep.Navigation;

    public class MainWindowViewmodel
    {
        public IViewNavigator ViewNavigator { get; }

        public MainWindowViewmodel(IViewNavigator viewNavigator)
        {
            this.ViewNavigator = viewNavigator;
        }
    }
}
