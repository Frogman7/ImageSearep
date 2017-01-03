namespace ImageSearep.Views.ImageData
{
    using System.Windows.Input;

    using Models;

    public interface IImageDataViewmodel : IViewmodel
    {
        IImageData NewImage { get; }

        EmbeddedImageInfo EmbeddedImage { get; }

        int Height { get; set; }

        int Width { get; set; }

        ICommand CommitImageChangesCommand { get; }

        ICommand LoadReplacementImageCommand { get; }
    }
}
