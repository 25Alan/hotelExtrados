using hotelExtrados.Business.Services;
using hotelExtrados.Data.Models;
using hotelExtrados.Models;
using hotelExtrados.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelExtrados.Menu
{
    public class Menu
    {
        private Login_Register login_register = new Login_Register();
        private UserInput newUser = new UserInput();
        private PasswordI password = new PasswordI();

        private Aap aap = new Aap();
        private Room room = new Room();
        private Client client = new Client();

        private Book_details book_Details = new Book_details();
        private Book_Number_Room book_NumberRoom = new Book_Number_Room();

        private Booking book = new Booking();

        ConsoleKeyInfo option = new ConsoleKeyInfo();
        public Menu() { }

        public void MenuPrincipal()
        {
            Console.Title = "WELCOME";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[1] - LOGIN\n[2] - Register");
            option = Console.ReadKey();
            if (option.Key == ConsoleKey.NumPad1 || option.Key == ConsoleKey.D1) MenuLogin();
            if (option.Key == ConsoleKey.NumPad2 || option.Key == ConsoleKey.D1) MenuRegister();
        }

        public void MenuLogin()
        {
            Console.Clear();
            Console.Title = "LOGIN";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("------LOGIN------");
            Console.WriteLine("ENTER USERNAME");
            newUser.Name_User = Console.ReadLine();
            Console.WriteLine("ENTER PASSWORD");
            password.Password = Console.ReadLine();

            if (login_register.LoginUser(password, newUser.Name_User) == true) MenuAap();
            Console.Read();
            Console.Clear();
        }

        public void MenuRegister()
        {
            Console.Clear();
            Console.Title = "REGISTER";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("------REGISTER-------");
            Console.WriteLine("ENTER USERNAME");
            newUser.Name_User = Console.ReadLine();
            Console.WriteLine("ENTER PASSWORD");
            password.Password = Console.ReadLine();
            Console.WriteLine("SELECT RANGE\n[1] - Aap (Public Attention)\n[2] - Admin (Administrator)");
            option = Console.ReadKey();
            if (option.Key == ConsoleKey.NumPad1) newUser.Status_Aap = true; else newUser.Status_Admin = true;

            login_register.CreateUser(password.Password, newUser);
            Console.Clear();
            MenuPrincipal();
        }

        public void MenuAap()
        {
            Console.Title = "AAP";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[1] - State of the rooms\n[2] - New client\n[3] - Book Room\n[4] - Change room status");
            option = Console.ReadKey();
            if (option.Key == ConsoleKey.NumPad1 || option.Key == ConsoleKey.D1) StateoftheRooms();
            if (option.Key == ConsoleKey.NumPad2 || option.Key == ConsoleKey.D2) newClient();
            if (option.Key == ConsoleKey.NumPad3 || option.Key == ConsoleKey.D3) newBook();
            if (option.Key == ConsoleKey.NumPad4 || option.Key == ConsoleKey.D4) newChangeStatus();
        }

        public void MenuAdmin()
        {
            Console.Title = "ADMIN";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[1] - Add new room\n[2] - Change room status");
            int opc = Convert.ToInt32(Console.ReadLine());
        }

        private void StateoftheRooms()
        {
            Console.Title = "STATE OF THE ROOMS";
            Console.WriteLine("[1] - LIST ROOM - PROPERTIES\n[2] - ROOM STATUS LIST");
            option = Console.ReadKey();
            if (option.Key == ConsoleKey.NumPad1 || option.Key == ConsoleKey.D1) aap.ListRoomProperties();
            if (option.Key == ConsoleKey.NumPad2 || option.Key == ConsoleKey.D2) aap.ListRoomStatus();
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

        public Book_Number_Room newbookDetails()
        {
            Console.Title = "DETAILS BOOK";
            Console.WriteLine("SELECT NUMBER OF ROOM");
            book_NumberRoom.number_Select = Convert.ToInt32(Console.ReadLine());
            book_NumberRoom.Since_Date = DateTime.Now;
            Console.WriteLine("UNTIL DATE (yyyy-mm-dd-hh-mm-ss:");
            book_NumberRoom.Until_Date = Convert.ToDateTime(Console.ReadLine());

            return book_NumberRoom;
        }

        public void newBook()
        {
            Console.Title = "NEW BOOK";
            Console.WriteLine("SELECT ROOM");
            book.Id_Booking_Room = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("SELECT CLIENT (DNI)");
            book.Id_Booking_Client= Convert.ToInt32(Console.ReadLine());

            aap.newBook(aap.selectRoom(book.Id_Booking_Room), aap.selectClient(book.Id_Booking_Client));
        }

        private void newChangeStatus()
        {
            Console.Title = "CHANGE STATUS";
            Console.WriteLine("NUMBER ROOM:");
            room.Number_Room = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("NEW STATUS:");
            room.Status_Room = Console.ReadLine();
        }
    }
}