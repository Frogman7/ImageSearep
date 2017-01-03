namespace ImageSearep.Models
{
    using System;
    using System.Collections.Generic;

    public class BinarySearch
    {
        public static IEnumerable<BinaryMatch> Match(byte[] source, byte[] beginningSequence, byte[] endSequence)
        {
            long index = 0;
            long startIndex = 0;
            long endIndex = 0;

            IList<BinaryMatch> matches = new List<BinaryMatch>();

            while (index < source.Length)
            {
                startIndex = FindSequence(source, beginningSequence, index);

                if (startIndex >= 0)
                {
                    endIndex = FindSequence(source, endSequence, startIndex + beginningSequence.Length);

                    if (endIndex >= 0)
                    {
                        var length = (endIndex - startIndex) + endSequence.Length;
                        var buffer = new byte[length];

                        Array.Copy(source, startIndex, buffer, 0, length);

                        matches.Add(new BinaryMatch(buffer, startIndex));

                        index = endIndex + endSequence.Length;
                    }
                    else
                    {
                        index = source.Length;
                    }
                }
                else
                {
                    index = source.Length;
                }
            }

            return matches;
        }

        private static long FindSequence(byte[] bytes, byte[] sequence, long startIndex = 0)
        {
            long currentIndex = startIndex;
            long sequenceIndex = 0;
            bool found = false;

            while (!found && currentIndex < bytes.Length)
            {
                if (bytes[currentIndex] == sequence[sequenceIndex])
                {
                    if (sequenceIndex == (sequence.Length - 1))
                    {
                        found = true;
                    }
                    else
                    {
                        sequenceIndex++;
                    }
                }
                else
                {
                    currentIndex -= sequenceIndex;
                    sequenceIndex = 0;
                }

                currentIndex++;
            }

            return found ? (currentIndex - sequence.Length) : -1;
        }
    }
}