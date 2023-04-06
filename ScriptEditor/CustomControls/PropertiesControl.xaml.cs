namespace ScriptEditor.CustomControls;

public partial class PropertiesControl : UserControl
{
    public PropertiesControl() => InitializeComponent();

    public void SetPropertiesModel(List<PropertyItem> properties) => properties.ForEach(property => _ = Container.Children.Add(property));
}
