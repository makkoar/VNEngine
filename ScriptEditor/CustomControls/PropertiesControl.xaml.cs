namespace ScriptEditor.CustomControls;

public partial class PropertiesControl : UserControl
{
    public PropertiesControl() => InitializeComponent();

    public void SetPropertiesModel(List<PropertyItem> properties)
    {
        Container.Children.Clear();
        properties.ForEach(property => _ = Container.Children.Add(property));
    }
}
