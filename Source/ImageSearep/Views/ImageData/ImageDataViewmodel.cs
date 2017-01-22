namespace ImageSearep.Views.ImageData
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Windows.Input;

    using Models;

    using Microsoft.Win32;

    public class ImageDataViewmodel : IImageDataViewmodel
    {
        public EmbeddedImageInfo EmbeddedImage { get; }

        public IImageData NewImage { get; private set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public ICommand CommitImageChangesCommand { get; }

        public ICommand LoadReplacementImageCommand { get; }

        public ICommand GoBackCommand { get; }

        public bool Modified { get; private set; }

        private readonly IFileHandle fileHandle;

        public ImageDataViewmodel(IFileHandle fileHandle, EmbeddedImageInfo embeddedImageInfo)
        {
            this.fileHandle = fileHandle;
            this.EmbeddedImage = embeddedImageInfo;

            this.LoadReplacementImageCommand = new DelegateCommand(this.LoadReplacementImage);
            this.CommitImageChangesCommand = new DelegateCommand(this.CommitChanges);
            this.GoBackCommand = new DelegateCommand(() =>
            {
                if (this.OnViewFinished != null)
                {
                    this.OnViewFinished(this, new EventArgs());
                }
            });
        }

        private void LoadReplacementImage()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = string.Format(
                "{0} files (*.{1}) | *.{1}", 
                this.EmbeddedImage.ImageData.FileExtension.ToUpper(), 
                this.EmbeddedImage.ImageData.FileExtension);

            if (openFileDialog.ShowDialog() == true)
            {
                var filePath = openFileDialog.FileName;
                byte[] binaryData;

                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                using (BinaryReader br = new BinaryReader(fs))
                {
                    binaryData = br.ReadBytes(Convert.ToInt32(fs.Length));
                }

                if (this.EmbeddedImage.ImageData.ImageBinary.LongLength >= binaryData.LongLength)
                {
                    this.NewImage = new PngImageData(binaryData);
                    this.Modified = true;
                }
                else
                {
                    if (this.OnNotifyUser != null)
                    {
                        this.OnNotifyUser(this, new NotifyUserEventArgs("Replacement image is larger than the original!"));
                    }
                }
            }
        }

        private void CommitChanges()
        {
            if (this.NewImage != null)
            {
                if (this.EmbeddedImage.ImageData.ImageBinary.LongLength >= this.NewImage.ImageBinary.LongLength)
                {
                    // Overrites the image bytes in the original file
                    this.fileHandle.WriteBytes(this.NewImage.ImageBinary, this.EmbeddedImage.ImageStartIndex);

                    // Stores changes for viewing in the software
                    this.EmbeddedImage.ImageData.ReplaceImage(this.NewImage.ImageBinary);
                }
            }

            if (this.OnViewFinished != null)
            {
                this.OnViewFinished(this, EventArgs.Empty);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<PushViewEventArgs> OnViewPushed;

        public event EventHandler<NotifyUserEventArgs> OnNotifyUser;

        public event EventHandler OnViewFinished;
    }
}
