using System.Linq;
using Xceed.Wpf.Toolkit;

namespace ScriptEditor.CustomControls;

public partial class PropertyItem : UserControl
{
    public PropertyItem(string propertyName, object value)
    {
        InitializeComponent();
        PropertyName.ToolTip = PropertyName.Text = propertyName;
        if (value == null) return;
        Control control = new();
        if (value is string s)
        {
            control = new TextBox() { Text = s };
        }
        else if (value is int i)
        {
            control = new IntegerUpDown { Value = i, Minimum = 0, Maximum = int.MaxValue, Background = new SolidColorBrush(Color.FromRgb(31, 31, 31)), Foreground = Brushes.White, BorderBrush = new SolidColorBrush(Color.FromRgb(66, 66, 66)) };
        }
        else if (value is bool b)
        {
            control = new CheckBox { IsChecked = b };
        }
        else if (value.GetType().IsEnum)
        {
            control = new ComboBox
            {
                ItemsSource = Enum.GetValues(value.GetType()).Cast<object>().ToList(),
                SelectedItem = value,
                Foreground = Brushes.White,
                Template = FindResource("ComboBoxTemplate") as ControlTemplate
            };
        }
        control.SetValue(Grid.ColumnProperty, 1);
        _ = Container.Children.Add(control);
    }
}
