using System;
using Avalonia.Controls;


namespace scivu.ViewModels;

public class AddPictureViewModel : ViewModelBase
{

    public AddPictureViewModel()
    {
    }

    public void ChangeView(string view)
    {
        throw new NotImplementedException();
    }

    public void ChoosePicture()
    {
        throw new NotImplementedException();
    }

    public async void UploadPicture()
    {
        var window = n();
        window.StorageProvider.GetStorageForDirectory("path/to/directory");
        var dialog = new OpenFileDialog
        {
            Directory = "/point/to/inital/folder/"
        };
    }
}
