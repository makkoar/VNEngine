namespace ScriptEditor;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Application.Current.ShutdownMode = ShutdownMode.OnLastWindowClose;
        Main = this;
        HasChanges = false;
        PropertiesControl.Visibility = ObjectTree.Visibility = Visibility.Collapsed;

        List<PropertyItem> propertys = new();
        PropertiesControl.SetPropertiesModel(propertys);
    }

    private void SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if (e.NewValue is UIElement selectedItem)
        {
            List<PropertyItem> propertys = new();
            string id = AutomationProperties.GetAutomationId(selectedItem);
            switch (id)
            {
                case "Root":
                    propertys.Add(new($"Название проекта", CurrentProject.Info.ProjectName, ChangeFunctions.ProjectName, "\\/:*?\"<>|"));
                    propertys.Add(new($"Название игры", CurrentProject.Info.Name, ChangeFunctions.Name));
                    propertys.Add(new($"Версия игры", CurrentProject.Info.Version, ChangeFunctions.Version));
                    break;
                default:
                    break;
            }
            PropertiesControl.SetPropertiesModel(propertys);
        }
    }

    private void CreateClick(object sender, RoutedEventArgs e)
    {
        CurrentProject = new();
        Save();
        PropertiesControl.Visibility = ObjectTree.Visibility = Visibility.Visible;
        Title = $"Редактор сценариев ({CurrentProject.Info.Name})";
    }
    private void OpenClick(object? sender = null, RoutedEventArgs? e = null)
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
                PropertiesControl.Visibility = ObjectTree.Visibility = Visibility.Visible;
                Title = $"Редактор сценариев ({CurrentProject.Info.Name})";
            }
        }
    }
    private void ExitClick(object sender, RoutedEventArgs e) => Close();
    private void SaveClick(object sender, RoutedEventArgs e) => Save();

    private static void Save()
    {
        if (CurrentProject is null)
        {
            _ = MessageBox.Show("Проект не создан.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        foreach (string path in Directory.GetFiles(".", "*.script", SearchOption.TopDirectoryOnly)) File.Delete(path);
        byte[] bytes = MessagePackSerializer.Serialize(CurrentProject);
        _ = Directory.CreateDirectory("Logs");
        _ = Directory.CreateDirectory(Path.Combine("Assets", "BGs"));
        _ = Directory.CreateDirectory(Path.Combine("Assets", "Characters"));
        File.WriteAllBytes($"{CurrentProject.Info.ProjectName}-v{CurrentProject.Info.Version}.script", bytes);
        HasChanges = false;
    }


    private bool ControlPressed = false;
    private void KeyDownEv(object sender, KeyEventArgs e)
    {
        if (ControlPressed)
            switch (e.Key)
            {
                case Key.S: Save(); break;
                case Key.T: _ = MessageBox.Show(CurrentProject.Info.Version.ToString()); break;
                default: break;
            }
        else if (e.Key is Key.LeftCtrl) ControlPressed = true;
    }
    private void KeyUpEv(object sender, KeyEventArgs e)
    {
        if (e.Key is Key.LeftCtrl) ControlPressed = false;
    }

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
