using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace beauty_saloon.Converter
{
    internal class DiscountConverter:IValueConverter
    {
        public static beauty_saloonEntities5 DataEntitiesEmployee { get; set; }
        ObservableCollection<Service> ListEmployee;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DataEntitiesEmployee = new beauty_saloonEntities5();
            ListEmployee = new ObservableCollection<Service>();
            var employees = DataEntitiesEmployee.Services;

            return TextDecorations.Strikethrough;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var decoration = value as TextDecoration;
            if (decoration != null)
            {
                return decoration.Location == TextDecorationLocation.Strikethrough;
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
