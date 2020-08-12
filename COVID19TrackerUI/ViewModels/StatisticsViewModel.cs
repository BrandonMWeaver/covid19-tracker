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

        private string _currentTime;

        public string CurrentTime
        {
            get { return this._currentTime; }
            set
            {
                this._currentTime = value;
                this.OnPropertyChanged(nameof(this.CurrentTime));
            }
        }

        public StatisticsViewModel()
        {
            new Clock(this.GetCurrentTime_Tick, 1000);
        }

        private void GetCurrentTime_Tick(object sender, object e)
        {
            this.CurrentTime = DateTime.Now.ToString("g");
        }
    }
}
