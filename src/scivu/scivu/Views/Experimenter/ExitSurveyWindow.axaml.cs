using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using scivu.ViewModels;
using ReactiveUI;

namespace scivu.Views;

public partial class ExitSurveyWindow : ReactiveWindow<ExitSurveyViewModel>
{
    public ExitSurveyWindow()
    {
        InitializeComponent();

        // To make the previewer happy
        if (Design.IsDesignMode) return;

        this.WhenActivated(action => action(ViewModel!.YesCommand.Subscribe(Close)));
        this.WhenActivated(action => action(ViewModel!.NoCommand.Subscribe(Close)));
    }
}