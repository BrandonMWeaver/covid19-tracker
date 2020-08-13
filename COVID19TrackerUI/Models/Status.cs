using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVID19TrackerUI.Models
{
    class Status
    {
        private string _cases;

        public string Cases
        {
            get { return int.Parse(this._cases).ToString("N0"); }
            set { this._cases = value; }
        }

        private string _deaths;

        public string Deaths
        {
            get { return int.Parse(this._deaths).ToString("N0"); }
            set { this._deaths = value; }
        }

        private string _lastUpdate;

        public string LastUpdate
        {
            get { return string.Join(" - ", this._lastUpdate.Split('T')[0].Split('-')); }
            set { this._lastUpdate = value; }
        }

        private string _recovered;

        public string Recovered
        {
            get { return int.Parse(this._recovered).ToString("N0"); }
            set { this._recovered = value; }
        }
    }
}
