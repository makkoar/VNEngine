namespace ScriptEditor;
public static class ChangeFunctions
{
    public static void ProjectName(string projectName) => CurrentProject.Info.ProjectName = projectName;
    public static void Name(string name) => Main.Title = $"Редактор сценариев ({CurrentProject.Info.Name = name})";
    public static void Version(Version version) => CurrentProject.Info.Version = version;
}
