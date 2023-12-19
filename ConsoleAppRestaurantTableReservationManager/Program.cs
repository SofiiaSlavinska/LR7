using System;
using System.Collections.Generic;

// Main Application Class
public class MainApp
{
    static void Main(string[] args)
    {
        ResMan resMan = new ResMan();
        TableBook tableBook = new TableBook(resMan);
        resMan.AddRest("A", 10);
        resMan.AddRest("tableBook", 5);

        Console.WriteLine(tableBook.BookTable("A", new DateTime(2023, 12, 25), 3)); // True
        Console.WriteLine(tableBook.BookTable("A", new DateTime(2023, 12, 25), 3)); // False
    }
}
public class ResMan
{
    public Dictionary<string, Rest> rest { get; private set; }

    public ResMan()
    {
        rest = new Dictionary <string, Rest>();
    }

    public void AddRest(string name, int table) //adding restaurants to list
    {
        try
        {
            Rest r = new Rest();
            r.name = name;
            r.table = new RestTable[table];
            for (int i = 0; i < table; i++)
            {
                r.table[i] = new RestTable();
            }
            rest.Add(name, r);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error while adding restaurant" + ex.Message);
            Console.WriteLine("Steck Trace: " + ex.StackTrace);
        }
    }
}

public class FileLoad //loading data about restaurants
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

    public List<string> FindAllFreeTables(DateTime dateTime) //not booked
    {
        try
        {
            List<string> free = new List<string>();
            foreach (KeyValuePair<string, Rest> pair in resMan.rest)
            {
                Rest r = pair.Value;
                RestTable[] tables = r.table;
                for (int i = 0; i < r.table.Length; i++)
                {
                    if (!r.table[i].IsBooked(dateTime))
                    {
                        free.Add($"{r.name} - Table {i + 1}");
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

    public bool BookTable(string rName, DateTime date, int tNumber)
    {
        foreach (KeyValuePair<string, Rest> pair in resMan.rest)
        {
            Rest r = pair.Value;
            RestTable[] tables = r.table;
            if (r.name == rName)
            {
                if (tNumber < 0 || tNumber >= r.table.Length)
                {
                    throw new Exception(null); //Invalid table number
                }

                return r.table[tNumber].Book(date);
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


    public void SortRest(DateTime dateTime)
    {
        try
        {
            List<KeyValuePair<string, Rest>> restList = new List<KeyValuePair<string, Rest>>(resMan.rest);
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
    public int CountTable(Rest r, DateTime dateTime) //basically free tables
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

// Restaurant Class
public class Rest
{
    public string name { get; set; } //name
    public RestTable[] table; // tables
}

// Table Class
public class RestTable
{
    private HashSet<DateTime> bookedDate; //booked dates
 

    public RestTable()
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
    public bool IsBooked(DateTime date)
    {
        return bookedDate.Contains(date);
    }
}
