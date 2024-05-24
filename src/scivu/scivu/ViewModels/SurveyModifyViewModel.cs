using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using Model.Question;



namespace scivu.ViewModels;
public class SurveyModifyViewModel : ViewModelBase
{

    public SurveyModifyViewModel()
    {
    }

    public void ChangeView(string view)
    {
        throw new NotImplementedException();
    }

    public async void AddPicture(IModifyQuestion question)
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop ||
            desktop.MainWindow?.StorageProvider is not { } provider)
            throw new NullReferenceException("Missing StorageProvider instance.");

        var file = await provider.OpenFilePickerAsync(new FilePickerOpenOptions()
        {
            Title = "Open Text File",
            AllowMultiple = false,
            FileTypeFilter = [FilePickerFileTypes.ImageAll]
        });

        question.ModifyPicture = file?.Count >= 1 ? file[0].Path.ToString() : string.Empty;
    }
}
