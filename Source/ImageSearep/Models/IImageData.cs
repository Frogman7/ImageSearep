namespace ImageSearep.Models
{
    using System.Drawing;

    public interface IImageData
    {
        Image Image { get; }

        byte[] ImageBinary { get; }

        bool ReplaceImage(byte[] newImageData);

        string FileExtension { get; }
    }
}
