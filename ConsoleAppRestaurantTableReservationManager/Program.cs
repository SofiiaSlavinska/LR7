using System;
using System.Collections.Generic;

// Main Application Class
public class main
{
    static void Main(string[] args)
    {
        ResMan m = new ResMan();
        TableBook b = new TableBook(m);
        m.AddRest("A", 10);
        m.AddRest("B", 5);

        Console.WriteLine(b.BookTable("A", new DateTime(2023, 12, 25), 3)); // True
        Console.WriteLine(b.BookTable("A", new DateTime(2023, 12, 25), 3)); // False
    }
}
public class ResMan
{
    public List<Rest> res;

    public ResMan()
    {
        res = new List<Rest>();
    }

    public void AddRest(string n, int t)
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
            Console.WriteLine("Error while adding restaurant" + ex.Message);
            Console.WriteLine("Steck Trace: " + ex.StackTrace);
        }
    }
}

public class FileLoad
{
    private ResMan resMan;

    public FileLoad(ResMan resMan)
    {
        this.resMan = resMan;
    }

    public void LoadRest(string fileP)
    {
        try
        {
            string[] ls = File.ReadAllLines(fileP);
            foreach (string l in ls)
            {
                var parts = l.Split(',');
                if (parts.Length == 2 && int.TryParse(parts[1], out int tableCount))
                {
                    resMan.AddRest(parts[0], tableCount);
                }
                else
                {
                    Console.WriteLine(l);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error while loading restaurant" + ex.Message);
            Console.WriteLine("Steck Trace: " + ex.StackTrace);
        }
    }
}

public class TableFind
{
    private ResMan resMan;

    public TableFind(ResMan resMan)
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
            Console.WriteLine("Error while finding free tables" + ex.Message);
            Console.WriteLine("Steck Trace: " + ex.StackTrace);
            return new List<string>();
        }
    }
}

public class TableBook
{
    private ResMan resMan;

    public TableBook(ResMan resMan)
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

public class RestSort
{
    private ResMan resMan;

    public RestSort(ResMan resMan)
    {
        this.resMan = resMan;
    }


    public void SortRest(DateTime dt)
    {
        try
        {
            bool swapped;
            do
            {
                swapped = false;
                for (int i = 0; i < resMan.res.Count - 1; i++)
                {
                    int avTc = CountTable(resMan.res[i], dt); // available tables current
                    int avTn = CountTable(resMan.res[i + 1], dt); // available tables next

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
            Console.WriteLine("Error while sorting restaurants" + ex.Message);
            Console.WriteLine("Steck Trace: " + ex.StackTrace);
        }
    }

    public int CountTable(Rest r, DateTime dt)
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
            Console.WriteLine("Error while counting tables" + ex.Message);
            Console.WriteLine("Steck Trace: " + ex.StackTrace);
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
            Console.WriteLine("Error while booking" + ex.Message);
            Console.WriteLine("Steck Trace: " + ex.StackTrace);
            return false;
        }
    }

    // is booked
    public bool IsBooked(DateTime d)
    {
        return bd.Contains(d);
    }
}
