namespace ImageSearep.Views.Images
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for ImagesView.xaml
    /// </summary>
    public partial class ImagesView : UserControl, IView
    {
        public ImagesView(IImagesViewmodel viewmodel)
        {
            this.InitializeComponent();

            this.Viewmodel = viewmodel;
        }

        public IViewmodel Viewmodel
        {
            get
            {
                return this.DataContext as IViewmodel;
            }

            private set
            {
                this.DataContext = value;
            }
        }

        public FrameworkElement FrameworkElement
        {
            get
            {
                return this;
            }
        }
    }
}
