using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationManager
{
    internal class BookingTable
    {
        private HashSet<DateTime> bookedDate; //booked dates


        public BookingTable()
        {
            bookedDate = new HashSet<DateTime>();
        }

        // book
        public bool Book(DateTime date) //booking successful or not
        {
            try
            {
                if (bookedDate.Contains(date))
                {
                    return false;
                }
                //add to bookedDate
                bookedDate.Add(date);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while booking" + ex.Message);
                Console.WriteLine("Steck Trace: " + ex.StackTrace);
                return false;
            }
        }

        // is booked
        public bool IsBooked(DateTime date) //checking if table is booked
        {
            return bookedDate.Contains(date);
        }
    }
}
