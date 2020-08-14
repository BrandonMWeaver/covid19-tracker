using COVID19TrackerUI.ViewModels.Base;
using COVID19TrackerUI.ViewModels.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVID19TrackerUI.ViewModels
{
    class TimeViewModel : NotificationBase
    {
        private string _time;

        public string Time
        {
            get { return this._time; }
            set
            {
                this._time = value;
                this.OnPropertyChanged(nameof(this.Time));
            }
        }

        public TimeViewModel()
        {
            this.Time = DateTime.Now.ToString("g");
            new Clock(this.GetCurrentTime_Tick, 1000);
        }

        private void GetCurrentTime()
        {
            this.Time = DateTime.Now.ToString("g");
        }

        private void GetCurrentTime_Tick(object sender, object e)
        {
            this.GetCurrentTime();
        }
    }
}
