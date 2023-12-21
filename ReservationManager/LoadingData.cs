using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationManager
{
    internal class LoadingData
    {
        private RestaurantManager resMan;

        public LoadingData(RestaurantManager resMan)
        {
            this.resMan = resMan;
        }

        public void LoadRest(string fileP)
        {
            try
            {
                string[] lines = File.ReadAllLines(fileP);
                foreach (string line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int tableCount))
                    {
                        resMan.AddRest(parts[0], tableCount);
                    }
                    else
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("File not found: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while loading restaurant: " + ex.Message);
                throw;
            }
        }
    }
}
