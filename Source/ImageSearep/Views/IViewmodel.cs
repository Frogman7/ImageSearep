namespace ImageSearep.Views
{
    using System;
    using System.ComponentModel;

    public interface IViewmodel : INotifyPropertyChanged
    {
        event EventHandler<PushViewEventArgs> ViewPushed;

        event EventHandler ViewFinished;
    }
}
