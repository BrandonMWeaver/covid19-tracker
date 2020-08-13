using COVID19TrackerUI.Models;
using COVID19TrackerUI.ViewModels.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            new Clock(this.GetStatistics_Tick, 60000);
            this.GetCurrentTime();
            this.GetStatistics();
        }

        private void GetCurrentTime()
        {
            this.CurrentTime = DateTime.Now.ToString("g");
        }

        private async void GetStatistics()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync($"https://covid19-api.org/api/status/us");
                responseMessage.EnsureSuccessStatusCode();
                string json = await responseMessage.Content.ReadAsStringAsync();
                this.Statistics = new Statistics
                {
                    Response = JsonConvert.DeserializeObject<Response>(json)
                };
            }
        }

        private void GetCurrentTime_Tick(object sender, object e)
        {
            this.GetCurrentTime();
        }

        private void GetStatistics_Tick(object sender, object e)
        {
            this.GetStatistics();
        }
    }
}
