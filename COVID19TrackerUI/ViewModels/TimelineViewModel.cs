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

        public TimelineViewModel()
        {
            this.GetTimeline();
        }

        private async void GetTimeline()
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
    }
}
