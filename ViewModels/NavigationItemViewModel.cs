using CommunityToolkit.Mvvm.ComponentModel;

namespace Bibliothek.ViewModels;

public sealed partial class NavigationItemViewModel(
    string title,
    string icon,
    ViewModelBase viewModel) : ObservableObject
{
    public string Title { get; } = title;
    public string Icon { get; } = icon;
    public ViewModelBase ViewModel { get; } = viewModel;

    [ObservableProperty]
    private bool isSelected;
}
