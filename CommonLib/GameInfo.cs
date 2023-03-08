namespace CommonLib;
[MessagePackObject]
public class GameInfo
{
    /// <summary>Версия проекта.</summary>
    [Key(0)]
    public Version Version { get; set; } = new(1, 0, 0, 0);
    /// <summary>Название проекта в виде неразрывной строки. Служит, как название папки для сохранений проекта.</summary>
    [Key(1)]
    public string ProjectName { get; set; } = "GameProject";
    /// <summary>Название игры, отображаемое в заголовке.</summary>
    [Key(2)]
    public string Name { get; set; } = "Название игры";
   
}
