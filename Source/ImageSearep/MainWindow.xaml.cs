using System.Windows;

namespace ImageSearep
{
    using ImageSearep.Navigation;
    using ImageSearep.Views;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            var viewNavigator = new ViewManager();
            this.DataContext = new MainWindowViewmodel(viewNavigator);

            viewNavigator.PushView(new StartView(new StartViewmodel()));
        }
    }
}
