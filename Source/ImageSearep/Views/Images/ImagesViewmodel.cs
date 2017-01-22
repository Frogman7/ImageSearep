namespace ImageSearep.Views.Images
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Input;

    using ImageData;

    using Models;

    public class ImagesViewmodel : IImagesViewmodel
    {
        public ImagesViewmodel(IFileHandle fileHandle, IEnumerable<EmbeddedImageInfo> embeddedImageInfos)
        {
            this.fileHandle = fileHandle;
            this.EmbeddedImageInfos = new ReadOnlyCollection<EmbeddedImageInfo>(embeddedImageInfos.ToList());

            this.ViewSelectedImageDataInDetailCommand = new DelegateCommand(this.ViewSelectedImageDetail);
            this.GoBackCommand = new DelegateCommand(
                () =>
                    {
                        if (this.OnViewFinished != null)
                        {
                            this.fileHandle.Close();
                            this.OnViewFinished(this, new EventArgs());
                        }
                    });
        }

        public IReadOnlyList<EmbeddedImageInfo> EmbeddedImageInfos { get; }

        public EmbeddedImageInfo SelectedEmbeddedImage { get; set; }

        public ICommand ViewSelectedImageDataInDetailCommand { get; }

        public ICommand GoBackCommand { get; }

        public event EventHandler<PushViewEventArgs> OnViewPushed;

        public event EventHandler<NotifyUserEventArgs> OnNotifyUser;

        public event EventHandler OnViewFinished;

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IFileHandle fileHandle;

        private void ViewSelectedImageDetail()
        {
            if (SelectedEmbeddedImage != null)
            {
                if (this.OnViewPushed != null)
                {
                    this.OnViewPushed(this, new PushViewEventArgs(new ImageDataView(new ImageDataViewmodel(this.fileHandle, this.SelectedEmbeddedImage))));
                }
            }
        }
    }
}
