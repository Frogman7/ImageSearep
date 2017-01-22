namespace ImageSearep.Navigation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;

    using Models;

    using PropertyChanged;

    using Views;

    [ImplementPropertyChanged]
    public class ViewManager : IViewManager
    {
        private readonly Stack<IView> ViewStack; 

        public event PropertyChangedEventHandler PropertyChanged;

        public bool ShowDialog { get; protected set; }

        public string DialogMessage { get; protected set; }

        public ViewManager()
        {
            this.ViewStack = new Stack<IView>();

            this.CloseDialog = new DelegateCommand(() => this.ShowDialog = false);

            this.ShowDialog = false;
        }

        public FrameworkElement CurrentView { get; private set; }

        public void PushView(IView view)
        {
            view.Viewmodel.OnViewPushed += this.ViewmodelOnViewPushed;
            view.Viewmodel.OnViewFinished += this.ViewmodelOnViewFinished;
            view.Viewmodel.OnNotifyUser += this.ViewmodelOnNotifyUser;

            this.CurrentView = view.FrameworkElement;

            this.ViewStack.Push(view);
        }

        private void ViewmodelOnNotifyUser(object sender, NotifyUserEventArgs e)
        {
            this.DialogMessage = e.Message;
            this.ShowDialog = true;
        }

        private void ViewmodelOnViewFinished(object sender, EventArgs eventArgs)
        {
            this.PopView();
        }

        private void ViewmodelOnViewPushed(object sender, PushViewEventArgs pushViewEventArgs)
        {
            this.PushView(pushViewEventArgs.View);
        }

        public IView PopView()
        {
            var removedElement = this.ViewStack.Pop();

            removedElement.Viewmodel.OnViewPushed -= this.ViewmodelOnViewPushed;
            removedElement.Viewmodel.OnViewFinished -= this.ViewmodelOnViewFinished;

            this.CurrentView = this.ViewStack.Peek().FrameworkElement;

            return removedElement;
        }

        public ICommand CloseDialog { get; }
    }
}
