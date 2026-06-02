using System.Collections.ObjectModel;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Bibliothek.ViewModels;

public abstract partial class ListViewModelBase<T> : ViewModelBase, ILoadableViewModel
{
    protected ListViewModelBase()
    {
        ItemsView = CollectionViewSource.GetDefaultView(Items);
        ItemsView.Filter = FilterItem;
    }

    public ObservableCollection<T> Items { get; } = [];
    public System.ComponentModel.ICollectionView ItemsView { get; }

    [ObservableProperty]
    private T? selectedItem;

    [ObservableProperty]
    private string searchText = string.Empty;

    partial void OnSearchTextChanged(string value) => ItemsView.Refresh();

    public abstract Task LoadAsync();

    protected async Task RunAsync(Func<Task> action)
    {
        IsBusy = true;
        ErrorMessage = null;
        try
        {
            await action();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsBusy = false;
        }
    }

    protected void ReplaceItems(IEnumerable<T> items)
    {
        Items.Clear();
        foreach (var item in items)
        {
            Items.Add(item);
        }
        ItemsView.Refresh();
    }

    protected abstract bool MatchesSearch(T item, string searchText);

    private bool FilterItem(object item)
        => item is T typed && MatchesSearch(typed, SearchText.Trim());
}
