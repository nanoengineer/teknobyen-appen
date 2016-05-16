using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknobyen.Models
{
    class TextAndValueModel
    {
        public TextAndValueModel(string desc, double value)
        {
            Description = desc;
            Value = value;
        }

        public string Description { get; set; }
        public double Value { get; set; }
    }
}
