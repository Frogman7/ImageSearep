namespace ImageSearep.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class PngImageFinder : IImageFinder
    {
        private readonly byte[] ImageHeaderStart;

        private readonly byte[] ImageEOF;

        public PngImageFinder()
        {
            this.ImageHeaderStart = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, 0x00 };

            this.ImageEOF = new byte[] { 0x00, 0x00, 0x49, 0x45, 0x4E, 0x44, 0xAE, 0x42, 0x60, 0x82 };
        }

        public Task<IEnumerable<EmbeddedImageInfo>> FindImages(byte[] binaryData)
        {
            return Task.Run(
                () => this.FindPngImages(binaryData));
        }

        private IEnumerable<EmbeddedImageInfo> FindPngImages(byte[] binaryData)
        {
            IImageData imageData = null;

            IList<EmbeddedImageInfo> embeddedImageInfos = new List<EmbeddedImageInfo>();

            var matches = BinarySearch.Match(binaryData, this.ImageHeaderStart, this.ImageEOF);

            foreach (var match in matches)
            {
                imageData = new PngImageData(match.BinaryData);

                embeddedImageInfos.Add(new EmbeddedImageInfo(imageData, match.StartIndex));
            }

            return embeddedImageInfos;
        }
    }
}
