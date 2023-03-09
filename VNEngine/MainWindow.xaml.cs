namespace VNEngine;
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Open();
    }

    public void Open()
    {
        List<string> scripts = Directory.GetFiles(".", "*.script").ToList();
        if (scripts.Count is 0)
        {
            _ = MessageBox.Show("Сценарий не найден.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            Application.Current.Shutdown();
        }
        else if (scripts.Count > 1)
        {
            _ = MessageBox.Show("Найдено несколько сценариев. Будет загружен первый в списке.", "Информация!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        try
        {
            byte[] bytes = File.ReadAllBytes(scripts[0]);
            CurrentProject = MessagePackSerializer.Deserialize<Game>(bytes);
            Title = CurrentProject is not null ? CurrentProject.Info.Name : throw new("ScriptNotLoadException");
        }
        catch
        {
            _ = MessageBox.Show("Не удалось загрузить сценарий.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            Application.Current.Shutdown();
        }
    }
}
