using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSearep.Models
{
    public class EmbeddedImageInfo
    {
        public long ImageStartIndex { get; private set; }

        public IImageData ImageData { get; private set; }

        public EmbeddedImageInfo(IImageData imageData, long imageStartIndex)
        {
            this.ImageData = imageData;
            this.ImageStartIndex = imageStartIndex;
        }
    }
}
