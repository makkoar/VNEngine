namespace CommonLib;
[MessagePackObject]
public class Game
{
    [Key(0)]
    public GameInfo Info { get; set; } = new();
    [Key(1)]
    public MainMenu Menu { get; set; } = new();
}
