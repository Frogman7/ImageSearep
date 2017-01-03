namespace ImageSearep.Views
{
    using System.Windows.Input;

    public interface IStartViewmodel : IViewmodel
    {
        string ChosenFilePath { get; }

        ICommand ChooseFileCommand { get; }

        ICommand FindImagesInFileCommand { get; }
    }
}
