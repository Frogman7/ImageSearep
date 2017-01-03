namespace ImageSearep.Models
{
    using System;
    using System.Drawing;

    using PropertyChanged;

    [ImplementPropertyChanged]
    public abstract class ImageDataBase : IImageData
    {
        public abstract Image Image { get; protected set; }

        public byte[] ImageBinary { get; }

        public abstract bool ReplaceImage(string newImageData);

        public abstract string FileExtension { get; }

        protected ImageDataBase(byte[] imageData)
        {
            this.ImageBinary = imageData;
        }
    }
}
