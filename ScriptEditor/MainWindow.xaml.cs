namespace ScriptEditor;

public partial class MainWindow : Window
{
    public MainWindow() => InitializeComponent();

    private void Exit(object sender, RoutedEventArgs e)
    {
        if (HasChanges)
            switch (MessageBox.Show("У вас остались не сохранённые изменения. Вы хотите их сохранить перед выходом?", "Внимание!", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel))
            {
                case MessageBoxResult.Yes:
                    Save();
                    Application.Current.Shutdown();
                    break;
                case MessageBoxResult.No: Application.Current.Shutdown(); break;
            }
        else Application.Current.Shutdown();
    }

    public void Save()
    {

    }
}
