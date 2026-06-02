using CommunityToolkit.Mvvm.ComponentModel;

namespace Bibliothek.ViewModels;

public sealed partial class SettingsViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool isDarkModePrepared;

    [ObservableProperty]
    private string databaseServer = "localhost,1433";
}
