namespace MAUIPos.Application.Converter
{
    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return !(bool)value;
            }
            catch
            {
                return false;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return !(bool)value;
            }
            catch
            {
                return false;
            }
        }
    }
}
