namespace ImageSearep.Views.Images
{
    using System.Collections.Generic;
    using System.Windows.Input;

    using Models;

    public interface IImagesViewmodel : IViewmodel
    {
        IReadOnlyList<EmbeddedImageInfo> EmbeddedImageInfos { get; }

        EmbeddedImageInfo SelectedEmbeddedImage { get; set; }

        ICommand ViewSelectedImageDataInDetailCommand { get; }

        ICommand GoBackCommand { get; }
    }
}
