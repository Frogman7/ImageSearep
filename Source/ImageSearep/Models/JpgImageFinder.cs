using ImageSearep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSearep
{
    public class JpgImageFinder : IImageFinder
    {
        private readonly byte[] ImageHeaderStart;

        private readonly byte[] ImageEOF;

        public JpgImageFinder()
        {
            this.ImageHeaderStart = new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 };

            this.ImageEOF = new byte[] { 0xFF, 0xD9 };
        }

        public Task<IEnumerable<EmbeddedImageInfo>> FindImages(byte[] binaryData)
        {
            return Task.Run(
                () => this.FindJpgImages(binaryData));
        }

        private IEnumerable<EmbeddedImageInfo> FindJpgImages(byte[] binaryData)
        {
            IImageData imageData = null;

            IList<EmbeddedImageInfo> embeddedImageInfos = new List<EmbeddedImageInfo>();

            var matches = BinarySearch.Match(binaryData, this.ImageHeaderStart, this.ImageEOF);

            foreach (var match in matches)
            {
                imageData = new JpgImageData(match.BinaryData);

                embeddedImageInfos.Add(new EmbeddedImageInfo(imageData, match.StartIndex));
            }

            return embeddedImageInfos;
        }
    }
}