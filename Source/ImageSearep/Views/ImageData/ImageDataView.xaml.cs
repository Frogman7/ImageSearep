using System.Windows;
using System.Windows.Controls;

namespace ImageSearep.Views.ImageData
{
    /// <summary>
    /// Interaction logic for ImagesView.xaml
    /// </summary>
    public partial class ImageDataView : UserControl, IView
    {
        public ImageDataView(IImageDataViewmodel viewmodel)
        {
            this.InitializeComponent();

            this.Viewmodel = viewmodel;
        }

        public IViewmodel Viewmodel
        {
            get
            {
                return this.DataContext as IViewmodel; ;
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
