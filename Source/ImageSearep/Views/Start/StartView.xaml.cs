using System.Windows.Controls;

namespace ImageSearep.Views
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for StartView.xaml
    /// </summary>
    public partial class StartView : UserControl, IView
    {
        public StartView(IStartViewmodel viewmodel)
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
