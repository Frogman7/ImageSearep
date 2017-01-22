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

        public event EventHandler<PushViewEventArgs> OnViewPushed;

        public event EventHandler<NotifyUserEventArgs> OnNotifyUser;

        public event EventHandler OnViewFinished;

        public string ChosenFilePath { get; private set; }

        public ICommand ChooseFileCommand { get; }

        public ICommand FindImagesInFileCommand { get; }

        public bool Processing { get; private set; }

        public bool FileSelected { get; private set; }

        private void SetChoosenFileFromFileOpenDialog()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                this.ChosenFilePath = openFileDialog.FileName;
                this.FileSelected = true;
            }
        }

        private async Task FindSupportImagesInFile()
        {
            IFileHandle fileHandle = new FileHandle(this.ChosenFilePath);

            if (fileHandle.Exists)
            {
                if (fileHandle.Open())
                {
                    this.Processing = true;

                    var binaryData = fileHandle.GetBytes();

                    if (binaryData != null && binaryData.Length > 0)
                    {
                        var pngImageFinder = new PngImageFinder();

                        var imageDatas = await pngImageFinder.FindImages(binaryData);

                        this.Processing = false;

                        if (imageDatas.Any())
                        {
                            if (this.OnViewPushed != null)
                            {
                                this.OnViewPushed(this, new PushViewEventArgs(new ImagesView(new ImagesViewmodel(fileHandle, imageDatas))));
                            }
                        }
                        else
                        {
                            if (this.OnNotifyUser != null)
                            {
                                this.OnNotifyUser(this, new NotifyUserEventArgs("No image data found in file!"));
                            }
                        }
                    }
                }
            }
        }
    }
}
