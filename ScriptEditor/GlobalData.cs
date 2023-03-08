namespace ScriptEditor;
public static class GlobalData
{
    public static Game? CurrentProject { get; set; } = null;

    public static bool HasChanges { get; set; } = false;
}
