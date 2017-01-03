namespace ImageSearep.Views
{
    using System.Windows;

    public interface IView
    {
        IViewmodel Viewmodel { get; }

        FrameworkElement FrameworkElement { get; }
    }
}
