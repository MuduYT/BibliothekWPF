using System.Windows;

namespace Bibliothek.Views;

public partial class PublisherDialogView : Window
{
    public PublisherDialogView()
    {
        InitializeComponent();
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        if (Tag is Func<bool> commit && !commit())
        {
            MessageBox.Show("Bitte pruefe die Eingaben.", "Validierung", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        DialogResult = true;
    }

    private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
}
