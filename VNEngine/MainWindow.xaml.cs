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
            _ = MessageBox.Show("Сценарий не был найден.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            Application.Current.Shutdown();
        }
        else if (scripts.Count > 1)
        {
            _ = MessageBox.Show("Было найдено несколько сценариев. Будет загружен первый в списке.", "Информация!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        try
        {
            byte[] bytes = File.ReadAllBytes(scripts[0]);
            CurrentProject = MessagePackSerializer.Deserialize<Game>(bytes);
        }
        catch
        {
            _ = MessageBox.Show("Не удалось загрузить сценарий.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            Application.Current.Shutdown();
        }
        finally
        {
            if (CurrentProject is not null)
            {
                Title = CurrentProject.Info.Name;
            }
            else
            {
                _ = MessageBox.Show("Сценарий не был загружен.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }
    }
}
