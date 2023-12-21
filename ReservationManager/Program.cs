using System;
using System.Collections.Generic;
using ReservationManager;
public class MainApp
{
    static void Main(string[] args)
    {
        RestaurantManager resMan = new RestaurantManager();
        try
        {
            resMan.AddRest("A", 10);
            resMan.AddRest("tableBook", 5);

        Console.WriteLine(resMan.BookTable("A", new DateTime(2023, 12, 25), 3)); // True
        Console.WriteLine(resMan.BookTable("A", new DateTime(2023, 12, 25), 3)); // False
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in Main: " + ex.Message);
        }
    }
}

/*public class RestaurantManager
{

    
}
public class Restaurant //restaurant's data(name and table)
{
    
}
public class LoadingData //loading data about restaurants
{
   
}


public class TableFind //finding free tables
{
    
}
public class Sorting //by free tables
{
    
}

public class BookingTable
{
    
}
*/