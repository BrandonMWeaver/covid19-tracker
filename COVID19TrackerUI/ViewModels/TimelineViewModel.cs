using COVID19TrackerUI.Models;
using COVID19TrackerUI.ViewModels.Base;
using COVID19TrackerUI.ViewModels.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace COVID19TrackerUI.ViewModels
{
    class TimelineViewModel : NotificationBase
    {
        private Timeline _timeline;

        public Timeline Timeline
        {
            get { return this._timeline; }
            set
            {
                this._timeline = value;
                this.OnPropertyChanged(nameof(this.Timeline));
            }
        }

        private Status _currentStatus;

        public Status CurrentStatus
        {
            get { return this._currentStatus; }
            set
            {
                this._currentStatus = value;
                this.OnPropertyChanged(nameof(this.CurrentStatus));
            }
        }

        public TimelineViewModel()
        {
            this.TimelineViewModelAsync(); 
        }

        private async void TimelineViewModelAsync()
        {
            await this.GetTimeline();
            this.CurrentStatus = this.Timeline.Statuses.First();
            foreach (Status status in this.Timeline.Statuses)
            {
                status.StatusSelected += this.OnStatusSelected;
            }
        }

        private async Task GetTimeline()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync("https://covid19-api.org/api/timeline/us");
                responseMessage.EnsureSuccessStatusCode();
                string json = await responseMessage.Content.ReadAsStringAsync();
                string pascalCaseFriendlyJson = string.Join(string.Empty, json.Split('_'));
                this.Timeline = new Timeline()
                {
                    Statuses = new List<Status>(JsonConvert.DeserializeObject<List<Status>>(pascalCaseFriendlyJson).Take(30))
                };
            }
        }

        public void OnStatusSelected(object sender, EventArgs e)
        {
            this.CurrentStatus = (Status) sender;
        }
    }
}
