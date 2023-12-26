using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationManagerTests
{
    [TestClass]
    public class RestaurantTests
    {
        [TestMethod]
        public void RestaurantTest()//if setting name and table is correct
        {
            var restaurant = new Restaurant();
            string name = "Restaurant";
            var table = new BookingTable[5];

            restaurant.name = name;
            restaurant.table = table;

            Assert.AreEqual(name, restaurant.name);
            Assert.AreEqual(table, restaurant.table);
        }

    }
}
namespace ReservationManagerTests
{
    [TestClass]
    public class LoadingDataTests
    {
        [TestMethod]
        public void LoadRestV()// when valid file
        {
            var restaurantManager = new RestaurantManager();
            var loadingData = new LoadingData(restaurantManager);
            string filePath = "testFile.txt";
            File.WriteAllLines(filePath, new string[] { "Restaurant1,5", "Restaurant2,3" });

            loadingData.LoadRest(filePath);

            Assert.IsTrue(restaurantManager.rest.ContainsKey("Restaurant1"));
            Assert.IsTrue(restaurantManager.rest.ContainsKey("Restaurant2"));
            Assert.AreEqual(5, restaurantManager.rest["Restaurant1"].table.Length);
            Assert.AreEqual(3, restaurantManager.rest["Restaurant2"].table.Length);

            File.Delete(filePath);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void LoadRestI()//when invalid file
        {
            var restaurantManager = new RestaurantManager();
            var loadingData = new LoadingData(restaurantManager);
            string filePath = "NonExistentFile.txt";

            loadingData.LoadRest(filePath);
        }
    }
}
namespace ReservationManagerTests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void BookTestTrue() //return true when date is not booked
        {
            var bookingTable = new BookingTable();
            DateTime date = new DateTime(2023, 12, 24);

            bool result = bookingTable.Book(date);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void BookTestFalse() //return false when date is already booked
        {
            var bookingTable = new BookingTable();
            DateTime date = new DateTime(2023, 12, 24);
            bookingTable.Book(date);

            bool result = bookingTable.Book(date);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsBookedTrue() //return true when date is booked
        {
            var bookingTable = new BookingTable();
            DateTime date = new DateTime(2023, 12, 24);
            bookingTable.Book(date);

            bool result = bookingTable.IsBooked(date);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsBookedFalse()//return false when date isn't booked
        {
            var bookingTable = new BookingTable();
            DateTime date = new DateTime(2023, 12, 24);

            bool result = bookingTable.IsBooked(date);

            Assert.IsFalse(result);
        }
    }
}

namespace ReservationManagerTests
{
    [TestClass]
    public class RestaurantManagerTests
    {
        [TestMethod]
        public void AddRestTest() //valid
        {

            var restaurantManager = new RestaurantManager();
            string name = "Restaurant";
            int table = 5;

            restaurantManager.AddRest(name, table);

            Assert.IsTrue(restaurantManager.rest.ContainsKey(name));
            Assert.AreEqual(table, restaurantManager.rest[name].table.Length);
        }

        [TestMethod]
        public void BookTableTestV()//valid
        {
            var restaurantManager = new RestaurantManager();
            string name = "Restaurant";
            int table = 5;
            restaurantManager.AddRest(name, table);
            DateTime date = new DateTime(2023, 12, 24);
            int tNumber = 2;

            bool result = restaurantManager.BookTable(name, date, tNumber);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BookTableTestI() //invalid
        {
            var restaurantManager = new RestaurantManager();
            string name = "Restaurant";
            int table = 5;
            restaurantManager.AddRest(name, table);
            DateTime date = new DateTime(2023, 12, 24);
            int tNumber = 6;

            restaurantManager.BookTable(name, date, tNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BookTableTestNone() //restaurant isn't exist
        {
            var restaurantManager = new RestaurantManager();
            string name = "Restaurant";
            int table = 5;
            restaurantManager.AddRest(name, table);
            DateTime date = new DateTime(2023, 12, 24);
            int tNumber = 2;

            restaurantManager.BookTable("Non Existing Restaurant", date, tNumber);
        }
    }
}
namespace ReservationManagerTests
{
    [TestClass]
    public class SortingTests
    {
        [TestMethod]
        public void SortRestTest()//valid
        {

            var restaurantManager = new RestaurantManager();
            restaurantManager.AddRest("Restaurant 1", 5);
            restaurantManager.AddRest("Restaurant 2", 3);
            restaurantManager.BookTable("Restaurant 1", new DateTime(2023, 12, 24), 2);
            restaurantManager.BookTable("Restaurant 2", new DateTime(2023, 12, 24), 1);
            var sorting = new Sorting(restaurantManager);
            DateTime dateTime = new DateTime(2023, 12, 24);

            sorting.SortRest(dateTime);
        }

        [TestMethod]
        public void CountTableTest()//valid
        {

            var restaurantManager = new RestaurantManager();
            restaurantManager.AddRest("Restaurant", 5);
            restaurantManager.BookTable("Restaurant", new DateTime(2023, 12, 24), 2);
            var sorting = new Sorting(restaurantManager);
            DateTime dateTime = new DateTime(2023, 12, 24);

            int count = sorting.CountTable(restaurantManager.rest["Restaurant"], dateTime);

            Assert.AreEqual(4, count);
        }
    }
}
namespace ReservationManagerTests
{
    [TestClass]
    public class TableFindTests
    {
        [TestMethod]
        public void FindAllFreeTables_ShouldReturnCorrectFreeTables_WhenCalledWithValidDateTime() //return free tables when called with valid date time
        {
            var restaurantManager = new RestaurantManager();
            restaurantManager.AddRest("Restaurant 1", 5);
            restaurantManager.AddRest("Restaurant 2", 3);
            restaurantManager.BookTable("Restaurant 1", new DateTime(2023, 12, 24), 2);
            restaurantManager.BookTable("Restaurant 2", new DateTime(2023, 12, 24), 1);
            var tableFind = new TableFind(restaurantManager);
            DateTime dateTime = new DateTime(2023, 12, 24);

            var freeTables = tableFind.FindAllFreeTables(dateTime);

            Assert.AreEqual(6, freeTables.Count);
            Assert.AreEqual(4, freeTables.Count(t => t.StartsWith("Restaurant 1")));
            Assert.AreEqual(2, freeTables.Count(t => t.StartsWith("Restaurant 2")));
        }
    }
}