using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace beauty_saloon.Converter
{
    class ImagePathConverter: IValueConverter
    {//Конвертер для изображеий из бд
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"/Image/{value}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().Substring(8);
        }
    }
}
