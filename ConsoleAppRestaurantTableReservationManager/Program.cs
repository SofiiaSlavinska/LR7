using System;
using System.Collections.Generic;

// Main Application Class
public class main
{
    static void Main(string[] args)
    {
        ResMan m = new ResMan();
        TableBooker b = new TableBooker(m);
        m.AddRestaurantMethod("A", 10);
        m.AddRestaurantMethod("B", 5);

        Console.WriteLine(b.BookTable("A", new DateTime(2023, 12, 25), 3)); // True
        Console.WriteLine(b.BookTable("A", new DateTime(2023, 12, 25), 3)); // False
    }
}

// Reservation Manager Class
/*public class ResMan
{
    // res
    public List<Rest> res;

    public ResMan()
    {
        res = new List<Rest>();
    }

    // Add Restaurant Method
    public void AddRestaurantMethod(string n, int t)
    {
        try
        {
            Rest r = new Rest();
            r.n = n;
            r.t = new RestTable[t];
            for (int i = 0; i < t; i++)
            {
                r.t[i] = new RestTable();
            }
            res.Add(r);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error");
        }
    }

    // Load Restaurants From
    // File
    private void LoadRestaurantsFromFileMethod(string fileP)
    {
        try
        {
            string[] ls = File.ReadAllLines(fileP);
            foreach (string l in ls)
            {
                var parts = l.Split(',');
                if (parts.Length == 2 && int.TryParse(parts[1], out int tableCount))
                {
                    AddRestaurantMethod(parts[0], tableCount);
                }
                else
                {
                    Console.WriteLine(l);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error");
        }
    }

    //Find All Free Tables
    public List<string> FindAllFreeTables(DateTime dt)
    {
        try
        { 
            List<string> free = new List<string>();
            foreach (var r in res)
            {
                for (int i = 0; i < r.t.Length; i++)
                {
                    if (!r.t[i].IsBooked(dt))
                    {
                        free.Add($"{r.n} - Table {i + 1}");
                    }
                }
            }
            return free;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error");
            return new List<string>();
        }
    }

    public bool BookTable(string rName, DateTime d, int tNumber)
    {
        foreach (var r in res)
        {
            if (r.n == rName)
            {
                if (tNumber < 0 || tNumber >= r.t.Length)
                {
                    throw new Exception(null); //Invalid table number
                }

                return r.t[tNumber].Book(d);
            }
        }

        throw new Exception(null); //Restaurant not found
    }

    public void SortRestaurantsByAvailabilityForUsersMethod(DateTime dt)
    {
        try
        { 
            bool swapped;
            do
            {
                swapped = false;
                for (int i = 0; i < res.Count - 1; i++)
                {
                    int avTc = CountAvailableTablesForRestAndDateTimeMethod(res[i], dt); // available tables current
                    int avTn = CountAvailableTablesForRestAndDateTimeMethod(res[i + 1], dt); // available tables next

                    if (avTc < avTn)
                    {
                        // Swap restaurants
                        var temp = res[i];
                        res[i] = res[i + 1];
                        res[i + 1] = temp;
                        swapped = true;
                    }
                }
            } while (swapped);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error");
        }
    }

    // count available tables in a restaurant
    public int CountAvailableTablesForRestAndDateTimeMethod(Rest r, DateTime dt)
    {
        try
        {
            int count = 0;
            foreach (var t in r.t)
            {
                if (!t.IsBooked(dt))
                {
                    count++;
                }
            }
            return count;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error");
            return 0;
        }
    }
}*/

public class ResMan
{
    public List<Rest> res;

    public ResMan()
    {
        res = new List<Rest>();
    }

    public void AddRestaurantMethod(string n, int t)
    {
        try
        {
            Rest r = new Rest();
            r.n = n;
            r.t = new RestTable[t];
            for (int i = 0; i < t; i++)
            {
                r.t[i] = new RestTable();
            }
            res.Add(r);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error");
        }
    }
}

public class FileLoader
{
    private ResMan resMan;

    public FileLoader(ResMan resMan)
    {
        this.resMan = resMan;
    }

    public void LoadRestaurantsFromFileMethod(string fileP)
    {
        try
        {
            string[] ls = File.ReadAllLines(fileP);
            foreach (string l in ls)
            {
                var parts = l.Split(',');
                if (parts.Length == 2 && int.TryParse(parts[1], out int tableCount))
                {
                    resMan.AddRestaurantMethod(parts[0], tableCount);
                }
                else
                {
                    Console.WriteLine(l);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error");
        }
    }
}

public class TableFinder
{
    private ResMan resMan;

    public TableFinder(ResMan resMan)
    {
        this.resMan = resMan;
    }

    public List<string> FindAllFreeTables(DateTime dt)
    {
        try
        {
            List<string> free = new List<string>();
            foreach (var r in resMan.res)
            {
                for (int i = 0; i < r.t.Length; i++)
                {
                    if (!r.t[i].IsBooked(dt))
                    {
                        free.Add($"{r.n} - Table {i + 1}");
                    }
                }
            }
            return free;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error");
            return new List<string>();
        }
    }
}

public class TableBooker
{
    private ResMan resMan;

    public TableBooker(ResMan resMan)
    {
        this.resMan = resMan;
    }

    public bool BookTable(string rName, DateTime d, int tNumber)
    {
        foreach (var r in resMan.res)
        {
            if (r.n == rName)
            {
                if (tNumber < 0 || tNumber >= r.t.Length)
                {
                    throw new Exception(null); //Invalid table number
                }

                return r.t[tNumber].Book(d);
            }
        }

        throw new Exception(null); //Restaurant not found
    }
}

public class RestaurantSorter
{
    private ResMan resMan;

    public RestaurantSorter(ResMan resMan)
    {
        this.resMan = resMan;
    }

    public void SortRestaurantsByAvailabilityForUsersMethod(DateTime dt)
    {
        try
        {
            bool swapped;
            do
            {
                swapped = false;
                for (int i = 0; i < resMan.Count - 1; i++)
                {
                    int avTc = CountAvailableTablesForRestAndDateTimeMethod(resMan.res[i], dt); // available tables current
                    int avTn = CountAvailableTablesForRestAndDateTimeMethod(resMan.res[i + 1], dt); // available tables next

                    if (avTc < avTn)
                    {
                        // Swap restaurants
                        var temp = resMan.res[i];
                        resMan.res[i] = resMan.res[i + 1];
                        resMan.res[i + 1] = temp;
                        swapped = true;
                    }
                }
            } while (swapped);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error");
        }
    }

    public int CountAvailableTablesForRestAndDateTimeMethod(Rest r, DateTime dt)
    {
        try
        {
            int count = 0;
            foreach (var t in r.t)
            {
                if (!t.IsBooked(dt))
                {
                    count++;
                }
            }
            return count;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error");
            return 0;
        }
    }
}

// Restaurant Class
public class Rest
{
    public string n; //name
    public RestTable[] t; // tables
}

// Table Class
public class RestTable
{
    private List<DateTime> bd; //booked dates


    public RestTable()
    {
        bd = new List<DateTime>();
    }

    // book
    public bool Book(DateTime d)
    {
        try
        { 
            if (bd.Contains(d))
            {
                return false;
            }
            //add to bd
            bd.Add(d);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error");
            return false;
        }
    }

    // is booked
    public bool IsBooked(DateTime d)
    {
        return bd.Contains(d);
    }
}
