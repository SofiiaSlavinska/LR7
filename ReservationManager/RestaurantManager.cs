using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationManager
{
    internal class RestaurantManager
    {
        public Dictionary<string, Restaurant> rest { get; private set; } = null!; //there is restaurant's info

        public RestaurantManager()
        {
            rest = new Dictionary<string, Restaurant>();
        }

        public void AddRest(string name, int table) //adding restaurants to list
        {
            try
            {
                if (rest.ContainsKey(name))
                {
                    throw new ArgumentException($"Restaurant, with this name - {name}, already exists.");
                }
                Restaurant r = new Restaurant();
                r.name = name;
                r.table = new BookingTable[table];
                for (int i = 0; i < table; i++)
                {
                    r.table[i] = new BookingTable();
                }
                rest.Add(name, r);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while adding restaurant" + ex.Message);
                Console.WriteLine("Steck Trace: " + ex.StackTrace);
            }
        }
        private RestaurantManager resMan = null!;

        public RestaurantManager(RestaurantManager resMan)
        {
            this.resMan = resMan;
        }

        public bool BookTable(string rName, DateTime date, int tNumber)
        {
            foreach (KeyValuePair<string, Restaurant> pair in rest)
            {
                Restaurant r = pair.Value;
                BookingTable[] tables = r.table;
                if (r.name == rName)
                {
                    if (tNumber < 0 || tNumber >= r.table.Length)
                    {
                        throw new ArgumentException("Invalid table number");
                    }

                    return r.table[tNumber].Book(date);
                }
            }

            throw new ArgumentException("Restaurant not found");
        }
    }
}
