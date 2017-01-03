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

        private IFileHandle fileHandle;

        public ImageDataViewmodel(IFileHandle fileHandle, EmbeddedImageInfo embeddedImageInfo)
        {
            this.fileHandle = fileHandle;
            this.EmbeddedImage = embeddedImageInfo;

            this.LoadReplacementImageCommand = new DelegateCommand(this.LoadReplacementImage);
            this.CommitImageChangesCommand = new DelegateCommand(this.CommitChanges);
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
                }
            }
        }

        private void CommitChanges()
        {
            if (this.NewImage != null)
            {
                if (this.EmbeddedImage.ImageData.ImageBinary.LongLength >= this.NewImage.ImageBinary.LongLength)
                {
                    this.fileHandle.WriteBytes(this.NewImage.ImageBinary, this.EmbeddedImage.ImageStartIndex);
                }
            }

            if (this.ViewFinished != null)
            {
                this.ViewFinished(this, EventArgs.Empty);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<PushViewEventArgs> ViewPushed;

        public event EventHandler ViewFinished;
    }
}
