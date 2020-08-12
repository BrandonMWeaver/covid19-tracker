using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVID19TrackerUI.Models
{
    class Response
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

        private string _recovered;

        public string Recovered
        {
            get { return int.Parse(this._recovered).ToString("N0"); }
            set { this._recovered = value; }
        }
    }
}
