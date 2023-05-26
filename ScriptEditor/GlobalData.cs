namespace ScriptEditor;
public static class GlobalData
{
    public static MainWindow? Main { get; set; }

    public static Game? CurrentProject { get; set; } = null;

    private static bool hasChanges = false;
    public static bool HasChanges { get => hasChanges; set { Main.BSave.IsEnabled = hasChanges = value; } }
}
