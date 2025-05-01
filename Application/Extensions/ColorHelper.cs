using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIPos.Application.Extensions
{
    public static class ColorHelper
    {
        public static Color GetResourceColor(string resourceKey)
        {
            if (App.Current.Resources.TryGetValue(resourceKey, out var value) && value is Color color)
            {
                return color;
            }
            return Colors.Transparent;
        }
    }
}
