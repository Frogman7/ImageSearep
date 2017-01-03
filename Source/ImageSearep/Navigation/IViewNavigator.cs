namespace ImageSearep.Navigation
{
    using System.ComponentModel;
    using System.Windows;

    using ImageSearep.Views;

    public interface IViewNavigator : INotifyPropertyChanged
    {
        FrameworkElement CurrentView { get; }

        void PushView(IView view);

        IView PopView();
    }
}
