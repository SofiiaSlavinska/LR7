using NUnit.Framework;
using System;

[TestFixture]
public class ResMan
{
    private ResMan  resMan;

    [SetUp]
    public void Setup()
    {
         resMan = new ResMan();
    }

    [Test]
    public void AddRest() //if ok adding restaurant
    {
        string name = "Test";
        int table = 5;

         resMan.AddRest(name, table);

        Assert.IsTrue( resMan.rest.ContainsKey(name));
        Assert.AreEqual(table,  resMan.rest[name].table.Length);
    }

    [Test]
    public void Exception() //exception while adding
    {
        string name = "Test";
        int table = 5;
         resMan.AddRest(name, table);

        Assert.Throws<ArgumentException>(() =>  resMan.AddRest(name, table));
    }
}
[TestFixture]
public class FileLoad 
{
    private ResMan resMan;
    private FileLoad fileLoad;
    [SetUp]
    public void Setup()
    {
        resMan = new ResMan();
        fileLoad = new FileLoad( resMan);
    }
    [Test]
    public void LoadRestGood() //loading if file ok
    {
        string filePath = "good.txt";
        fileLoad.LoadRest(filePath);
    }
    [Test]
    public void LoadRestBad() //loading if file isnt ok
    {
        string filePath = "bad.txt";
        Assert.Throws<Exception>(() =>  fileLoad.LoadRest(filePath));
    }
}

[TestFixture]
public class TableFind 
{
    private ResMan resMan;
    private TableFind tableFind;

    [SetUp]
    public void Setup()
    {
        resMan = new ResMan();
        tableFind = new TableFind(resMan);
        resMan.AddRest("TestRestaurant", 5);
    }

    [Test]
    public void AllFreeTables() //tables returns/all free
    {
        DateTime date = new DateTime(2023, 12, 25);
        List<string> result = tableFind.FindAllFreeTables(date);
        Assert.AreEqual(5, result.Count);
    }

    [Test]
    public void SomeFreeTables() //tables returns/ some bookes
    {
        DateTime date = new DateTime(2023, 12, 25);
        TableBook tableBook = new TableBook(resMan);
        tableBook.BookTable("Test", date, 2);
        List<string> result = tableFind.FindAllFreeTables(date);
        Assert.AreEqual(4, result.Count);
    }
}

[TestFixture]
public class TableBook
{
    private ResMan  resMan;
    private TableBook  tableBook;

    [SetUp]
    public void Setup()
    {
         resMan = new ResMan();
         tableBook = new TableBook( resMan);
         resMan.AddRest("Test", 5);
    }

    [Test]
    public void BookTableCorrect()  //if booking table ok
    {
        string rName = "Test";
        DateTime date = new DateTime(2023, 12, 25);
        int tNumber = 2;

        bool result =  tableBook.BookTable(rName, date, tNumber);

        Assert.IsTrue(result);
    }

    [Test]
    public void BookTableNo() //exception on booking table while non-existent restaurant 
    {
        string rName = "None";
        DateTime date = new DateTime(2023, 12, 25);
        int tNumber = 2;

        Assert.Throws<Exception>(() =>  tableBook.BookTable(rName, date, tNumber));
    }

    [Test]
    public void BookTableBooked() //if table is booked
    {
        string rName = "Test";
        DateTime date = new DateTime(2023, 12, 25);
        int tNumber = 2;
         tableBook.BookTable(rName, date, tNumber);

        bool result =  tableBook.BookTable(rName, date, tNumber);

        Assert.IsFalse(result);
    }
}
[TestFixture]
public class RestSort
{
    private ResMan resMan;
    private RestSort restSort;
    [SetUp]
    public void Setup()
    {
        resMan = new ResMan();
        restSort = new RestSort(resMan);
        resMan.AddRest("Test1", 5);
        resMan.AddRest("Test2", 3);
    }
    [Test]
    public void SortRest() //if sorting correct
    {
        
        DateTime date = new DateTime(2023, 12, 25);
        TableBook tableBook = new TableBook(resMan);
        tableBook.BookTable("Test1", date, 2);
        tableBook.BookTable("Test1", date, 3);
        tableBook.BookTable("Test2", date, 1);
        restSort.SortRest(date);
       
        restSort.SortRest(date);

        
        int freeTablesInFirstRestaurant = restSort.CountTable(resMan.rest["TestRestaurant1"], date);
        int freeTablesInSecondRestaurant = restSort.CountTable(resMan.rest["TestRestaurant2"], date);
        Assert.IsTrue(freeTablesInFirstRestaurant < freeTablesInSecondRestaurant);
    }
}
[TestFixture]
public class Rest
{
    private Rest rest;
    [SetUp]
    public void Setup()
    {
        rest = new Rest();
    }
    [Test]
    public void NameSet() //if name is correct
    {
        string name = "Testt";
        rest.name = name;

        Assert.AreEqual(name, rest.name);
    }

    [Test]
    public void Table SetValidTable TableIsSet() //if table is correct
    {
        RestTable[] table = new RestTable[5];

        rest.table = table;

        Assert.AreEqual(table, rest.table);
    }
}
[TestFixture]
public class RestTable
{
    private RestTable  restTable;

    [SetUp]
    public void Setup()
    {
         restTable = new RestTable();
    }

    [Test]
    public void BookValid() //true 
    {
        DateTime date = new DateTime(2023, 12, 25);

        bool result =  restTable.Book(date);

        Assert.IsTrue(result);
    }

    [Test]
    public void BookBooked() //false
    {
        DateTime date = new DateTime(2023, 12, 25);
         restTable.Book(date);

        bool result =  restTable.Book(date);

        Assert.IsFalse(result);
    }

    [Test]
    public void BookedNot() //false
    {
        DateTime date = new DateTime(2023, 12, 25);

        bool result =  restTable.IsBooked(date);

        Assert.IsFalse(result);
    }

    [Test]
    public void Booked() //true
    {
        DateTime date = new DateTime(2023, 12, 25);
         restTable.Book(date);

        bool result =  restTable.IsBooked(date);

        Assert.IsTrue(result);
    }
}



