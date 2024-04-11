namespace ScriptEditor.CustomControls;
public partial class VersionProperty : UserControl
{
    public delegate void VersionChangedEventHandler(object sender, EventArgs e);
    public event VersionChangedEventHandler VersionChanged;

    private bool isInit = false;

    public string Major
    {
        get => TMajor.Text;
        set
        {
            if (!isInit)
            {
                if (value != OldMajor) OnVersionChanged();
                string OldText = TMajor.Text;
                TMajor.Text = value;
                if (OldText != TMajor.Text) TMajor.CaretIndex = TMajor.Text.Length;
            }
            else TMajor.Text = TextClear(value);
        }
    }
    public string Minor
    {
        get => TMinor.Text;
        set
        {
            if (!isInit)
            {
                if (value != OldMinor) OnVersionChanged();
                string OldText = TMinor.Text;
                TMinor.Text = TextClear(value);
                if (OldText != TMinor.Text) TMinor.CaretIndex = TMinor.Text.Length;
            }
            else TMinor.Text = TextClear(value);
        }
    }
    public string Build
    {
        get => TBuild.Text;
        set
        {
            if (!isInit)
            {
                if (value != OldBuild) OnVersionChanged();
                string OldText = TBuild.Text;
                TBuild.Text = TextClear(value);
                if (OldText != TBuild.Text) TBuild.CaretIndex = TBuild.Text.Length;
            }
            else TBuild.Text = TextClear(value);
        }
    }
    public string Revision
    {
        get => TRevision.Text;
        set
        {
            if (!isInit)
            {
                if (value != OldRevision) OnVersionChanged();
                string OldText = TRevision.Text;
                TRevision.Text = TextClear(value);
                if (OldText != TRevision.Text) TRevision.CaretIndex = TRevision.Text.Length;
            }
            else TRevision.Text = TextClear(value);
        }
    }

    public VersionProperty(Version version)
    {
        InitializeComponent();
        isInit = true;
        Version = version;
        DataContext = this;
        isInit = false;
    }

    public Version Version
    {
        get => new(int.Parse(Major), int.Parse(Minor), int.Parse(Build), int.Parse(Revision));
        set
        {
            Major = $"{value.Major}";
            Minor = $"{value.Minor}";
            Build = $"{value.Build}";
            Revision = $"{value.Revision}";
        }
    }

    protected virtual void OnVersionChanged() => VersionChanged?.Invoke(this, new EventArgs());
    private static string TextClear(string Text)
    {
        if (Text.Length > 1) Text = Text.TrimStart('0');
        if (Text.Length is 0) Text = "0";
        return Regex.Replace(Text, "[^0-9]", string.Empty);
    }

    private string OldMajor = string.Empty;
    private string OldMinor = string.Empty;
    private string OldBuild = string.Empty;
    private string OldRevision = string.Empty;

    private void MajorStartChanged(object sender, KeyEventArgs e) => OldMajor = TextClear(TMajor.Text);
    private void MinorStartChanged(object sender, KeyEventArgs e) => OldMinor = TextClear(TMinor.Text);
    private void BuildStartChanged(object sender, KeyEventArgs e) => OldBuild = TextClear(TBuild.Text);
    private void RevisionStartChanged(object sender, KeyEventArgs e) => OldRevision = TextClear(TRevision.Text);

    private void MajorStopChanged(object sender, KeyEventArgs e) => Major = TextClear(TMajor.Text);
    private void MinorStopChanged(object sender, KeyEventArgs e) => Minor = TextClear(TMinor.Text);
    private void BuildStopChanged(object sender, KeyEventArgs e) => Build = TextClear(TBuild.Text);
    private void RevisionStopChanged(object sender, KeyEventArgs e) => TRevision.Text = Revision = TextClear(TRevision.Text);
}