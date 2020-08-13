using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace COVID19TrackerUI.ViewModels.Tools
{
    class Clock
    {
        private readonly DispatcherTimer dispatcherTimer;

        public Clock(EventHandler<object> callback, int milliseconds)
        {
            this.dispatcherTimer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(milliseconds)
            };
            this.dispatcherTimer.Tick += callback;
            this.dispatcherTimer.Start();
        }
    }
}
