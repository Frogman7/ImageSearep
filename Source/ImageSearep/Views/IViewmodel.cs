namespace ImageSearep.Views
{
    using System;
    using System.ComponentModel;

    public interface IViewmodel : INotifyPropertyChanged
    {
        event EventHandler<PushViewEventArgs> OnViewPushed;

        event EventHandler<NotifyUserEventArgs> OnNotifyUser; 

        event EventHandler OnViewFinished;
    }
}
