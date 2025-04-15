using System.Globalization;

namespace Multiple_Timer_App;

public class BoolToTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var split = (parameter as string)?.Split('|');
        if (value is bool b && split?.Length == 2)
        {
            return b ? split[0] : split[1];
        }

        return parameter;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}

