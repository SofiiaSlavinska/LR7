using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationManager
{
  
    public class Restaurant
    {
        public string? name { get; set; }
        public BookingTable[] table = null!;
    }

}
