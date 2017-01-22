namespace ImageSearep.Views
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class HoverViewControl : ContentControl
    {
        public bool HoverEnabled
        {
            get
            {
                return (bool)this.GetValue(HoverEnabledProperty);
            }

            set
            {
                this.SetValue(HoverEnabledProperty, value);
            }
        }

        public static readonly DependencyProperty HoverEnabledProperty =
            DependencyProperty.Register("HoverEnabled", typeof(bool), typeof(HoverViewControl), new PropertyMetadata(false));

        public UIElement DefaultDataTemplate
        {
            get
            {
                return (UIElement)this.GetValue(DefaultDataTemplateProperty);
            }

            set
            {
                this.SetValue(DefaultDataTemplateProperty, value);
            }
        }

        public static readonly DependencyProperty DefaultDataTemplateProperty =
            DependencyProperty.Register("DefaultDataTemplate", typeof(UIElement), typeof(HoverViewControl), new PropertyMetadata(null));

        public UIElement HoverDataTemplate
        {
            get
            {
                return (UIElement)this.GetValue(HoverDataTemplateProperty);
            }

            set
            {
                this.SetValue(HoverDataTemplateProperty, value);
            }
        }

        public static readonly DependencyProperty HoverDataTemplateProperty =
            DependencyProperty.Register("HoverDataTemplate", typeof(UIElement), typeof(HoverViewControl), new PropertyMetadata(null));

        public HoverViewControl()
        {
            var defaultContentControl = new ContentControl();
            defaultContentControl.Content = this.DefaultDataTemplate;

            var hoverContentControl = new ContentControl() { Visibility = Visibility.Hidden };
            hoverContentControl.Content = this.HoverDataTemplate;

            var container = new Grid();

            container.Children.Add(defaultContentControl);
            container.Children.Add(hoverContentControl);

            var defaultContentBinding = new Binding();
            defaultContentBinding.Source = this;
            defaultContentBinding.Path = new PropertyPath(nameof(this.DefaultDataTemplate));
            BindingOperations.SetBinding(defaultContentControl, ContentControl.ContentProperty, defaultContentBinding);

            var hoverContentBinding = new Binding();
            hoverContentBinding.Source = this;
            hoverContentBinding.Path = new PropertyPath(nameof(this.HoverDataTemplate));
            BindingOperations.SetBinding(hoverContentControl, ContentControl.ContentProperty, hoverContentBinding);

            DependencyPropertyDescriptor.FromProperty(ContentControl.IsMouseOverProperty, typeof(HoverViewControl))
                .AddValueChanged(
                    this,
                    (sender, args) =>
                        {
                            if (this.HoverEnabled)
                            {
                                if (this.IsMouseOver)
                                {
                                    hoverContentControl.Visibility = Visibility.Visible;
                                    defaultContentControl.Visibility = Visibility.Hidden;
                                }
                                else
                                {
                                    hoverContentControl.Visibility = Visibility.Hidden;
                                    defaultContentControl.Visibility = Visibility.Visible;
                                }
                            }
                            else
                            {
                                hoverContentControl.Visibility = Visibility.Hidden;
                                defaultContentControl.Visibility = Visibility.Visible;
                            }
                        });

            this.Content = container;
        }
    }
}
