using COVID19TrackerUI.Models;
using COVID19TrackerUI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVID19TrackerUI.ViewModels
{
    class StatisticsViewModel : NotificationBase
    {
        private Statistics _statistics;

        public Statistics Statistics
        {
            get { return this._statistics; }
            set
            {
                this._statistics = value;
                this.OnPropertyChanged(nameof(this.Statistics));
            }
        }
    }
}
