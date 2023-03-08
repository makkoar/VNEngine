using System.ComponentModel;

namespace ScriptEditor;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Application.Current.ShutdownMode = ShutdownMode.OnLastWindowClose;
    }
    private void ExitClick(object sender, RoutedEventArgs e) => Close();

    private void Exit(object? sender, CancelEventArgs e)
    {
        if (HasChanges)
            switch (MessageBox.Show("У вас остались не сохранённые изменения. Вы хотите их сохранить перед выходом?", "Внимание!", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel))
            {
                case MessageBoxResult.Yes: Save(); break;
                case MessageBoxResult.Cancel: e.Cancel = true; break;
            }
    }

    public void Save()
    {

    }
}
