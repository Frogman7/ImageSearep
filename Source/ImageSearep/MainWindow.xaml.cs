using System.Windows;

namespace ImageSearep
{
    using ImageSearep.Models;
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

            // TODO: Find a beter way of initializing image finders
            var imageFinders = new IImageFinder[] { new PngImageFinder(), new JpgImageFinder() };

            viewNavigator.PushView(new StartView(new StartViewmodel(imageFinders)));
        }
    }
}
