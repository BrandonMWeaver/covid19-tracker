using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVID19TrackerUI.ViewModels.Tools
{
    static class StringHelper
    {
        public static double DoubleFromCommaSeparatedString(string s)
        {
            return double.Parse(string.Join(string.Empty, s.Split(',')));
        }
    }
}
