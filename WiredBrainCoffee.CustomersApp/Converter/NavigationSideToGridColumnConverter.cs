using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WiredBrainCoffee.CustomersApp.ViewModel;

namespace WiredBrainCoffee.CustomersApp.Converter
{
    public class NavigationSideToGridColumnConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null) return 0;

            var navigationSide = (CustomersViewModel.ENavigationSide)value;
            return navigationSide == CustomersViewModel.ENavigationSide.Left
                ? 0 // Value for Grid.Column
                : 2;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
