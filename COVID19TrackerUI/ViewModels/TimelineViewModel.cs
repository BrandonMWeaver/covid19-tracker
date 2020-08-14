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

        private ObservableCollection<string> _percentages;

        public ObservableCollection<string> Percentages
        {
            get { return this._percentages; }
            set
            {
                this._percentages = value;
                this.OnPropertyChanged(nameof(this.Percentages));
            }
        }

        public TimelineViewModel()
        {
            this.TimelineViewModelAsync(); 
        }

        private async void TimelineViewModelAsync()
        {
            await this.GetTimelineAsync();
            this.CurrentStatus = this.Timeline.Statuses.Last();
            this.Percentages = new ObservableCollection<string>()
            {
                "%0.00",
                "%0.00",
                "%0.00"
            };
            foreach (Status status in this.Timeline.Statuses)
            {
                status.StatusSelected += this.OnStatusSelected;
            }
        }

        private async Task GetTimelineAsync()
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
            double casesMax = StringHelper.DoubleFromCommaSeparatedString(this.Timeline.Statuses.First().Cases);
            double deathsMax = StringHelper.DoubleFromCommaSeparatedString(this.Timeline.Statuses.First().Deaths);
            double recoveredMax = StringHelper.DoubleFromCommaSeparatedString(this.Timeline.Statuses.First().Recovered);
            double casesDifference = casesMax - StringHelper.DoubleFromCommaSeparatedString(this.CurrentStatus.Cases);
            double deathsDifference = deathsMax - StringHelper.DoubleFromCommaSeparatedString(this.CurrentStatus.Deaths);
            double recoveredDifference = recoveredMax - StringHelper.DoubleFromCommaSeparatedString(this.CurrentStatus.Recovered);
            this.Percentages[0] = $"%{casesDifference / casesMax * 100:0.00}";
            this.Percentages[1] = $"%{deathsDifference / deathsMax * 100:0.00}";
            this.Percentages[2] = $"%{recoveredDifference / recoveredMax * 100:0.00}";
        }
    }
}
