using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSearep.Models
{
    public class BinaryMatch
    {
        public long StartIndex { get; }

        public byte[] BinaryData { get; }

        public BinaryMatch(byte[] binaryData, long startIndex)
        {
            this.BinaryData = binaryData;
            this.StartIndex = startIndex;
        }
    }
}
