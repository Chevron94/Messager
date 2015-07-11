using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media;
using Interpals;

namespace Messager
{
    class OnlineConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Все проверки для краткости выкинул
            return (bool)value == true ?
                new SolidColorBrush(Colors.LightGreen)
                : new SolidColorBrush(Colors.LightPink);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    class NewMessageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Все проверки для краткости выкинул
            return (bool)value == true ?
                new SolidColorBrush(Colors.White)
                : new SolidColorBrush(Colors.Pink);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    class TextMessageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Все проверки для краткости выкинул
            string text = ((Message)value).ToString();
            var tmp = text.Split('\n');
            text = tmp[0] +'\n'+ tmp[1];
            if (tmp.Length > 2)
                text += "...";
            if (text.Length <= 55)
                return text;
            else
            {
                text = text.Remove(52);
                text += "...";
                return text;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
