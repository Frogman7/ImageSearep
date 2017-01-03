namespace ImageSearep.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IImageFinder
    {
        Task<IEnumerable<EmbeddedImageInfo>> FindImages(byte[] binaryData);
    }
}