namespace ImageSearep.Navigation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;

    using ImageSearep.Views;

    using PropertyChanged;

    [ImplementPropertyChanged]
    public class ViewNavigator : IViewNavigator
    {
        private readonly Stack<IView> ViewStack; 

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewNavigator()
        {
            this.ViewStack = new Stack<IView>();
        }

        public FrameworkElement CurrentView { get; private set; }

        public void PushView(IView view)
        {
            view.Viewmodel.ViewPushed += this.ViewmodelOnViewPushed;
            view.Viewmodel.ViewFinished += this.ViewmodelOnViewFinished;

            this.CurrentView = view.FrameworkElement;

            this.ViewStack.Push(view);
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

            removedElement.Viewmodel.ViewPushed -= this.ViewmodelOnViewPushed;
            removedElement.Viewmodel.ViewFinished -= this.ViewmodelOnViewFinished;

            this.CurrentView = this.ViewStack.Peek().FrameworkElement;

            return removedElement;
        }
    }
}
