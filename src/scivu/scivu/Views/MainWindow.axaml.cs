using Avalonia.ReactiveUI;
using scivu.ViewModels;
using ReactiveUI;
using System.Threading.Tasks;

namespace scivu.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();

        this.WhenActivated(action =>
            action(ViewModel!._surveyTaker.ShowDialog.RegisterHandler(DoShowDialogAsync)));
    }

    private async Task DoShowDialogAsync(InteractionContext<ExitSurveyViewModel, bool> interaction)
    {
        var dialog = new ExitSurveyWindow();
        dialog.DataContext = interaction.Input;

        var result = await dialog.ShowDialog<bool>(this);
        interaction.SetOutput(result);
    }
}