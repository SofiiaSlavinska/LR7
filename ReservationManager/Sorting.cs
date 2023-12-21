using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationManager
{
    internal class Sorting
    {
        private RestaurantManager resMan;

        public Sorting(RestaurantManager resMan)
        {
            this.resMan = resMan;
        }


        public void SortRest(DateTime dateTime)
        {
            try
            {
                List<KeyValuePair<string, Restaurant>> restList = new List<KeyValuePair<string, Restaurant>>(resMan.rest);
                restList.Sort((pair1, pair2) => CountTable(pair1.Value, dateTime).CompareTo(CountTable(pair2.Value, dateTime)));

                bool swapped;
                do
                {
                    swapped = false;
                    for (int i = 0; i < restList.Count - 1; i++)
                    {
                        int availTablesC = CountTable(restList[i].Value, dateTime); // available tables current
                        int availTablesN = CountTable(restList[i + 1].Value, dateTime); // available tables next

                        if (availTablesC < availTablesN)
                        {
                            // Swap restaurants
                            var temp = restList[i];
                            restList[i] = restList[i + 1];
                            restList[i + 1] = temp;
                            swapped = true;
                        }
                    }
                } while (swapped);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while sorting restaurants" + ex.Message);
                Console.WriteLine("Steck Trace: " + ex.StackTrace);
            }
        }
        public int CountTable(Restaurant r, DateTime dateTime) //basically free tables
        {
            try
            {
                int count = 0;
                foreach (var table in r.table)
                {
                    if (!table.IsBooked(dateTime))
                    {
                        count++;
                    }
                }
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while counting tables" + ex.Message);
                Console.WriteLine("Steck Trace: " + ex.StackTrace);
                return 0;
            }
        }
    }
}
