using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknobyen.Models
{
    class DurationModel
    {
        public DurationModel(TimeSpan duration)
        {
            Duration = duration;
        }

        public TimeSpan Duration { get; set; }


        public string DurationString { get
            {
                return ToString();
            }
        }

        public override string ToString()
        {
            string timeString = "";

            timeString += Duration.Hours > 0 ? Duration.Hours.ToString() + " t " : "";
            timeString += Duration.Minutes > 0 ? Duration.Minutes.ToString() + " m" : "";

            return timeString;
        }

    }
}
