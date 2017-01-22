namespace ImageSearep.Models
{
    using System;
    using System.Drawing;
    using System.IO;

    public sealed class PngImageData : ImageDataBase
    {
        private const string PNGFileExtension = "png";

        public PngImageData(byte[] imageData)
            : base(imageData)
        {
            this.Image = this.BytesToImage(imageData);
        }

        public override Image Image { get; protected set; }

        public override bool ReplaceImage(byte[] newImageData)
        {
            if (newImageData.Length > this.ImageBinary.Length)
            {
                return false;
            }

            Array.Copy(newImageData, this.ImageBinary, newImageData.Length);

            this.Image = this.BytesToImage(this.ImageBinary);

            return true;
        }

        public override string FileExtension
        {
            get
            {
                return PNGFileExtension;
            }
        }

        private Image BytesToImage(byte[] imageBytes)
        {
            Image image = null;

            using (var ms = new MemoryStream(imageBytes))
            {
                image = Image.FromStream(ms);
            }

            return image;
        }
    }
}
