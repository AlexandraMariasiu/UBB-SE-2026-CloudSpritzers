using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;
using System;

namespace CloudSpritzers1.src.view.faq
{
   

        public class BooleanToGlyphConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, string language)
            {
                bool isExpanded = value is bool b && b;
                return isExpanded ? "\uE70D" : "\uE76C";
            }

            public object ConvertBack(object value, Type targetType, object parameter, string language)
            {
                throw new NotImplementedException();
            }
        }
    
}
