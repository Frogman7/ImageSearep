namespace ImageSearep.Models
{
    using System;
    using System.IO;

    public class FileHandle : IFileHandle
    {
        public bool Exists
        {
            get
            {
                if (this.Opened || File.Exists(this.PathToFile))
                {
                    return true;
                }

                return false;
            }
        }

        public bool Opened { get; private set; }

        public string PathToFile { get; }

        private FileStream fileStream;

        public bool Open()
        {
            if (this.Opened)
            {
                return false;
            }

            try
            {
                this.fileStream = new FileStream(this.PathToFile, FileMode.Open, FileAccess.ReadWrite);
            }
            catch (Exception)
            {
                return false;
            }

            this.Opened = true;

            return true;
        }

        public bool Close()
        {
            if (!this.Opened)
            {
                return false;
            }

            try
            {
                this.fileStream.Dispose();
                this.fileStream = null;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public byte[] GetBytes()
        {
            byte[] bytes;

            try
            {
                using (BinaryReader br = new BinaryReader(this.fileStream))
                {
                    bytes = br.ReadBytes(Convert.ToInt32(this.fileStream.Length));
                }

                // Reopen stream to maintain file lock
                this.fileStream = new FileStream(this.PathToFile, FileMode.Open, FileAccess.ReadWrite);
            }
            catch (Exception)
            {
                bytes = null;
            }
            
            return bytes;
        }

        public void WriteBytes(byte[] bytes, long startIndex)
        {
            try
            {
                using (BinaryWriter bw = new BinaryWriter(this.fileStream))
                {
                    bw.BaseStream.Seek(startIndex, SeekOrigin.Begin);

                    bw.Write(bytes);
                }

                // Reopen stream to maintain file lock
                this.fileStream = new FileStream(this.PathToFile, FileMode.Open, FileAccess.ReadWrite);
            }
            catch (Exception ex)
            {
            }
        }

        public FileHandle(string pathToFile)
        {
            this.PathToFile = pathToFile;
        }

        ~FileHandle()
        {
            this.Close();
        }
    }
}
