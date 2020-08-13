using COVID19TrackerUI.Models;
using COVID19TrackerUI.ViewModels.Base;
using COVID19TrackerUI.ViewModels.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace COVID19TrackerUI.ViewModels
{
    class StatusViewModel : NotificationBase
    {
        private Status _status;

        public Status Status
        {
            get { return this._status; }
            set
            {
                this._status = value;
                this.OnPropertyChanged(nameof(this.Status));
            }
        }

        public StatusViewModel()
        {
            this.GetStatistics();
            new Clock(this.GetStatistics_Tick, 60000);
        }

        private async void GetStatistics()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync($"https://covid19-api.org/api/status/us");
                responseMessage.EnsureSuccessStatusCode();
                string json = await responseMessage.Content.ReadAsStringAsync();
                this.Status = JsonConvert.DeserializeObject<Status>(json);
            }
        }

        private void GetStatistics_Tick(object sender, object e)
        {
            this.GetStatistics();
        }
    }
}
