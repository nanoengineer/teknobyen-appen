using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknobyen.Models
{
    public class LaundryBalanceModel
    {
        public LaundryBalanceModel(DateTime retrieved, double balance)
        {
            Retrieved = retrieved;
            Balance = balance;
        }

        [Key]
        public DateTime Retrieved { get; set; }
        public double Balance { get; set; }
    }
}
