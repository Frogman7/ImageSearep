namespace ImageSearep.Models
{
    using System.Drawing;

    using PropertyChanged;

    [ImplementPropertyChanged]
    public abstract class ImageDataBase : IImageData
    {
        public abstract Image Image { get; protected set; }

        public byte[] ImageBinary { get; }

        public abstract bool ReplaceImage(byte[] newImageData);

        public abstract string FileExtension { get; }

        protected ImageDataBase(byte[] imageData)
        {
            this.ImageBinary = imageData;
        }
    }
}
