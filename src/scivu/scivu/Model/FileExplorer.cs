namespace scivu.Model;

using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;

public static class FileExplorer
{
    private static bool FileExplorerAvailable(out IStorageProvider storageProvider)
    {
        storageProvider = default;
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop ||
            desktop.MainWindow?.StorageProvider is not { } provider)
            return false;
        storageProvider = provider;
        return true;
    }

    public static async Task<IStorageFile?> OpenSurveyAsync()
    {
        if (!FileExplorerAvailable(out var provider))
            throw new NullReferenceException("Missing StorageProvider instance.");

        var file = await provider.OpenFilePickerAsync(new FilePickerOpenOptions()
        {
            Title = "Import Survey",
            AllowMultiple = false,
        });

        return file.Count >= 1 ? file[0] : null;
    }

    public static async Task<IStorageFile?> OpenImageAsync()
    {
        if (!FileExplorerAvailable(out var provider))
            throw new NullReferenceException("Missing StorageProvider instance.");

        var file = await provider.OpenFilePickerAsync(new FilePickerOpenOptions()
        {
            Title = "Open Image File",
            AllowMultiple = false,
            FileTypeFilter = [FilePickerFileTypes.ImageAll]
        });

        return file.Count >= 1 ? file[0] : null;
    }
}