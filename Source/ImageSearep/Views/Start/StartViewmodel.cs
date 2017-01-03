namespace ImageSearep.Views
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Images;

    using Models;

    using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

    public class StartViewmodel : IStartViewmodel
    {
        public const string DefaultChoosenFileTextBoxText = "Please choose a file to continue!";

        public event PropertyChangedEventHandler PropertyChanged;

        public StartViewmodel()
        {
            this.ChooseFileCommand = new DelegateCommand(this.SetChoosenFileFromFileOpenDialog);
            this.FindImagesInFileCommand = new DelegateCommand(async () => await this.FindSupportImagesInFile());

            this.ChosenFilePath = DefaultChoosenFileTextBoxText;
        }

        public event EventHandler<PushViewEventArgs> ViewPushed;

        public event EventHandler ViewFinished;

        public string ChosenFilePath { get; private set; }

        public ICommand ChooseFileCommand { get; }

        public ICommand FindImagesInFileCommand { get; }

        private void SetChoosenFileFromFileOpenDialog()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                this.ChosenFilePath = openFileDialog.FileName;
            }
        }

        private async Task FindSupportImagesInFile()
        {
            IFileHandle fileHandle = new FileHandle(this.ChosenFilePath);

            if (fileHandle.Exists)
            {
                if (fileHandle.Open())
                {
                    var binaryData = fileHandle.GetBytes();

                    if (binaryData != null && binaryData.Length > 0)
                    {
                        var pngImageFinder = new PngImageFinder();

                        var imageDatas = await pngImageFinder.FindImages(binaryData);

                        if (imageDatas.Any())
                        {
                            if (this.ViewPushed != null)
                            {
                                this.ViewPushed(this, new PushViewEventArgs(new ImagesView(new ImagesViewmodel(fileHandle, imageDatas))));
                            }
                        }
                    }
                }
            }
        }
    }
}
