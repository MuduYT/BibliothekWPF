using CommunityToolkit.Mvvm.ComponentModel;

namespace Bibliothek.ViewModels;

public abstract partial class ViewModelBase : ObservableObject
{
    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private string? errorMessage;
}
