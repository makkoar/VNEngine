namespace ScriptEditor.CustomControls;
public partial class PropertyItem : UserControl
{
    public PropertyItem(string propertyName, string startValue, Action<string> changeFunc, string forbiddenChars = "")
    {
        InitializeComponent();
        PropertyName.ToolTip = PropertyName.Text = propertyName;
        TextBox control = new() { Text = startValue };
        control.TextChanged += (s, e) =>
        {
            foreach (char symbol in forbiddenChars)
            {
                if (((TextBox)s).Text.Contains(symbol)) _ = MessageBox.Show($"В данном поле нельзя использовать следующие символы: \"{string.Join(", ", forbiddenChars.Split())}\"", "Внимание!");
                ((TextBox)s).Text = ((TextBox)s).Text.Replace($"{symbol}", string.Empty);
            }
            changeFunc(((TextBox)s).Text);
            ((TextBox)s).CaretIndex = ((TextBox)s).Text.Length;
            HasChanges = true;
        };
        control.SetValue(Grid.ColumnProperty, 1);
        _ = Container.Children.Add(control);
    }

    public PropertyItem(string propertyName, Version startValue, Action<Version> changeFunc)
    {
        InitializeComponent();
        PropertyName.ToolTip = PropertyName.Text = propertyName;
        VersionProperty control = new(startValue);
        control.VersionChanged += (s, e) =>
        {
            changeFunc(((VersionProperty)s).Version);
            HasChanges = true;
        };
        control.SetValue(Grid.ColumnProperty, 1);
        _ = Container.Children.Add(control);
    }

    //public PropertyItem(string propertyName, ref object value)
    //{
    //    Control control = new();
    //    //if (value is int i)
    //    //{
    //    //    control = new IntegerUpDown { Value = i, Minimum = 0, Maximum = int.MaxValue, Background = new SolidColorBrush(Color.FromRgb(31, 31, 31)), Foreground = Brushes.White, BorderBrush = new SolidColorBrush(Color.FromRgb(66, 66, 66)) };
    //    //    ((IntegerUpDown)control).ValueChanged += (s, e) => HasChanges = true;
    //    //}
    //    //else
    //    if (value is bool b)
    //    {
    //        control = new CheckBox { IsChecked = b };
    //        ((CheckBox)control).Checked += (s, e) => HasChanges = true;
    //        ((CheckBox)control).Unchecked += (s, e) => HasChanges = true;
    //    }
    //    else if (value.GetType().IsEnum)
    //    {
    //        control = new ComboBox
    //        {
    //            ItemsSource = Enum.GetValues(value.GetType()).Cast<object>().ToList(),
    //            SelectedItem = value,
    //            Foreground = Brushes.White,
    //            Template = FindResource("ComboBoxTemplate") as ControlTemplate
    //        };
    //        ((ComboBox)control).SelectionChanged += (s, e) => HasChanges = true;

    //    }
    //    control.SetValue(Grid.ColumnProperty, 1);
    //    _ = Container.Children.Add(control);
    //}
}
