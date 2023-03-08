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

    private static void Save()
    {
        if (CurrentProject is null)
        {
            _ = MessageBox.Show("Проект не создан.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        byte[] bytes = MessagePackSerializer.Serialize(CurrentProject);
        _ = Directory.CreateDirectory(CurrentProject.Info.ProjectName);
        _ = Directory.CreateDirectory(Path.Combine(CurrentProject.Info.ProjectName, "Assets"));
        _ = Directory.CreateDirectory(Path.Combine(CurrentProject.Info.ProjectName, "Assets", "BGs"));
        _ = Directory.CreateDirectory(Path.Combine(CurrentProject.Info.ProjectName, "Assets", "Characters"));
        File.WriteAllBytes(Path.Combine(CurrentProject.Info.ProjectName, $"{CurrentProject.Info.ProjectName}-v{CurrentProject.Info.Version}.script"), bytes);
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
                DirectoryInfo? directoryInfo = new FileInfo(openFileDialog.FileName).Directory;
                DirectoryInfo? currentPath = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory;
                if (directoryInfo is not null && currentPath is not null)
                {
                    if (directoryInfo.FullName != Path.Combine(currentPath.FullName, directoryInfo.Name))
                        Directory.Move(directoryInfo.FullName, directoryInfo.Name);
                    _ = MessageBox.Show($"Сценарий \"{CurrentProject?.Info.Name}\" успешно загружен.");
                }
                else _ = MessageBox.Show("Не удалось загрузить сценарий.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
