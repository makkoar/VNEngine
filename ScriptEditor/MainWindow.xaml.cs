namespace ScriptEditor;
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Application.Current.ShutdownMode = ShutdownMode.OnLastWindowClose;
    }

    private void CreateProject(object sender, RoutedEventArgs e)
    {
        CurrentProject = new();
        Save();
    }

    private static void Save()
    {
        if (CurrentProject is null)
        {
            _ = MessageBox.Show("Проект не создан.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        byte[] bytes = MessagePackSerializer.Serialize(CurrentProject);
        _ = Directory.CreateDirectory("Assets");
        _ = Directory.CreateDirectory(Path.Combine("Assets", "BGs"));
        _ = Directory.CreateDirectory(Path.Combine("Assets", "Characters"));
        File.WriteAllBytes($"{CurrentProject.Info.ProjectName}-v{CurrentProject.Info.Version}.script", bytes);
        HasChanges = false;
    }

    public void Open(object? sender = null, RoutedEventArgs? e = null)
    {
        OpenFileDialog openFileDialog = new()
        {
            Filter = "Файл сценария (*.script)|*.script"
        };
        if (openFileDialog.ShowDialog() == true)
        {
            string oldPath = new FileInfo(openFileDialog.FileName).Directory.FullName.Trim('/', '\\');
            string newPath = AppDomain.CurrentDomain.BaseDirectory.Trim('/', '\\');
            if (oldPath != newPath)
            {
                foreach (string dirPath in Directory.GetDirectories(oldPath, "*", SearchOption.AllDirectories))
                    _ = Directory.CreateDirectory(dirPath.Replace(oldPath, newPath));
                foreach (string filePath in Directory.GetFiles(oldPath, "*.*", SearchOption.AllDirectories))
                    File.Move(filePath, filePath.Replace(oldPath, newPath), true);
                Directory.Delete(oldPath, true);
            }
            try
            {
                CurrentProject = MessagePackSerializer.Deserialize<Game>(File.ReadAllBytes(openFileDialog.FileName));
            }
            catch
            {
                _ = MessageBox.Show("Не удалось загрузить сценарий.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Title = $"Редактор сценариев ({CurrentProject?.Info.Name})";
            }
        }
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
}
