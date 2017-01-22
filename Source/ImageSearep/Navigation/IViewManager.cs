namespace ImageSearep.Navigation
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;

    using Views;

    public interface IViewManager : INotifyPropertyChanged
    {
        bool ShowDialog { get; }

        string DialogMessage { get; }

        FrameworkElement CurrentView { get; }

        void PushView(IView view);

        IView PopView();

        ICommand CloseDialog { get; }
    }
}
