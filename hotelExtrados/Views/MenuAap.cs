using hotelExtrados.Business.Services;
using hotelExtrados.Data.Models;
using System;
using System.Timers;
using Timer = System.Timers.Timer;

namespace hotelExtrados.Views
{
    public class MenuAap
    {
        private Room room = new Room();
        private static Aap aap = new Aap();
        private Client client = new Client();
        private Booking book = new Booking();
        private Book_details book_details = new Book_details();
        ConsoleKeyInfo option = new ConsoleKeyInfo();
        private static Timer timer;

        public MenuAap() { }

        public void MenuPrincipalAap()
        {
            Console.Clear();
            Console.Title = "AAP";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[1] - State of the rooms\n[2] - New client\n[3] - Book Room");
            option = Console.ReadKey();
            if (option.Key == ConsoleKey.NumPad1 || option.Key == ConsoleKey.D1) StateoftheRooms();
            if (option.Key == ConsoleKey.NumPad2 || option.Key == ConsoleKey.D2) newClient();
            if (option.Key == ConsoleKey.NumPad3 || option.Key == ConsoleKey.D3) newBook();
        }

        private void StateoftheRooms()
        {
            timer = new Timer(8 * 60 * 60 * 1000);
            timer.Elapsed += TimerElapsed;
            timer.Start();

            Console.Title = "STATE OF THE ROOMS";
            Console.WriteLine("[1] - LIST ROOM - PROPERTIES\n[2] - ROOM STATUS LIST");
            option = Console.ReadKey();
            if (option.Key == ConsoleKey.NumPad1 || option.Key == ConsoleKey.D1) aap.listRoomProperties();
            if (option.Key == ConsoleKey.NumPad2 || option.Key == ConsoleKey.D2) aap.listRoomStatus();
        }

        public void newClient()
        {
            Console.Title = "REGISTER CLIENT";
            Console.Write("DNI:");
            client.Dni_Client = Convert.ToInt32(Console.ReadLine());
            Console.Write("NAME:");
            client.Name_Client = Console.ReadLine();
            Console.Write("SURNAME:");
            client.Surname_Client = Console.ReadLine();
            Console.Write("EMAIL: ");
            client.Email_Client = Console.ReadLine();

            aap.newClient(client);
        }

        public void newBook()
        {
            Console.Title = "NEW BOOK";
            Console.WriteLine("SELECT ROOM");
            book.Id_Booking_Room = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("SELECT CLIENT (DNI)");
            book.Id_Booking_Client = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("UNTIL DATE (yyyy-mm-dd hh-mm-ss):");
            book_details.Until_Date = Convert.ToDateTime(Console.ReadLine());
            book_details.Since_Date = DateTime.Now;

            room.Number_Room = book.Id_Booking_Room;
            aap.newBook(aap.selectRoom(book.Id_Booking_Room), aap.selectClient(book.Id_Booking_Client));
            aap.newBookDetails(book_details);
            aap.changeStatusRoom(room.Number_Room);
        }

        private static void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            aap.updateStatusRoom();
        }
    }
}


/*
 * using System;
using System.Timers;

class Program
{
    static Timer _timer;

    static void Main(string[] args)
    {
        // Set the timer to fire every 24 hours
        _timer = new Timer(8 * 60 * 60 * 1000);
        _timer.Elapsed += TimerElapsed;
        _timer.Start();
    }

    static void TimerElapsed(object sender, ElapsedEventArgs e)
    {
        UpdateValue();
    }

    static void UpdateValue()
    {
        // Use Dapper to update the value in the database
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            var updateSql = "UPDATE mytable SET column1 = @newValue WHERE id = @id";
            var affectedRows = connection.Execute(updateSql, new { newValue = newValue, id = id });
        }
    }
}
using (var connection = new SqlConnection(connectionString))
{
    connection.Open();
    var updateSql = "UPDATE mytable SET column1 = @newValue WHERE id = @id";
    var affectedRows = connection.Execute(updateSql, new { newValue = newValue, id = id });
}
static void TimerElapsed(object sender, ElapsedEventArgs e)
{
    if(DateTime.Now.Hour == 10 && DateTime.Now.Minute == 0)
        UpdateValue();
}

 */