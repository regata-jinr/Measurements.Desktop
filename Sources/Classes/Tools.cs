using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measurements.UI.Desktop
{
    public static class Tools
    {
        public static string[] EnumToString(Type enumType)
        {
            return System.Enum.GetNames(enumType);
        }
    }
}
