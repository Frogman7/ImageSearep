namespace ImageSearep.Views
{
    using System;

    public class PushViewEventArgs : EventArgs
    {
        public IView View { get; }

        public PushViewEventArgs(IView view)
        {
            this.View = view;
        }
    }
}
