using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationManager
{
    internal class TableFind
    {
        private RestaurantManager resMan;

        public TableFind(RestaurantManager resMan)
        {
            this.resMan = resMan;
        }

        public List<string> FindAllFreeTables(DateTime dateTime) //not booked
        {
            try
            {
                List<string> free = new List<string>();
                foreach (KeyValuePair<string, Restaurant> pair in resMan.rest)
                {
                    Restaurant r = pair.Value;
                    BookingTable[] tables = r.table;
                    for (int i = 0; i < r.table.Length; i++)
                    {
                        if (!r.table[i].IsBooked(dateTime))
                        {
                            free.Add($"{r.name} - Table {i + 1}");
                        }
                    }
                }
                return free; //list of free tables
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while finding free tables" + ex.Message);
                Console.WriteLine("Steck Trace: " + ex.StackTrace);
                return new List<string>();
            }
        }
    }
}
