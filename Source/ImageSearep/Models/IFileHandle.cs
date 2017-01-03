namespace ImageSearep.Models
{
    public interface IFileHandle
    {
        bool Exists { get; }

        bool Opened { get; }

        string PathToFile { get; }

        bool Open();

        bool Close();

        byte[] GetBytes();

        void WriteBytes(byte[] byes, long startIndex);
    }
}
