namespace ImageSearep.Views.ImageData
{
    using System.Windows.Input;

    using Models;

    public interface IImageDataViewmodel : IViewmodel
    {
        bool Modified { get; }

        IImageData NewImage { get; }

        EmbeddedImageInfo EmbeddedImage { get; }

        int Height { get; set; }

        int Width { get; set; }

        ICommand CommitImageChangesCommand { get; }

        ICommand LoadReplacementImageCommand { get; }

        ICommand GoBackCommand { get; }
    }
}
