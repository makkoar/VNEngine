using System.ComponentModel;

namespace ScriptEditor;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Closing += (s, e) => Exit(e);
    }

    private void Exit(object? sender = null, RoutedEventArgs? e = null)
    {
        if (HasChanges)
            switch (MessageBox.Show("У вас остались не сохранённые изменения. Вы хотите их сохранить перед выходом?", "Внимание!", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel))
            {
                case MessageBoxResult.Yes:
                    Save();
                    Application.Current.Shutdown();
                    break;
                case MessageBoxResult.No: Application.Current.Shutdown(); break;
                case MessageBoxResult.Cancel:
                    if (sender is CancelEventArgs ev) ev.Cancel = true;
                    break;
            }
        else Application.Current.Shutdown();
    }

    public void Save()
    {

    }
}
