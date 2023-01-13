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

        private Room room = new Room();
        private Aap aap = new Aap();
        private Client client = new Client();
        private Book_details book_Details = new Book_details();

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
            if (option.Key == ConsoleKey.NumPad3 || option.Key == ConsoleKey.D3) newbookDetails();
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
            aap.ListRoom();
        }

        private void newClient()
        {
            Console.Title = "REGISTER CLIENT";
            Console.WriteLine("DATA:");
            Console.WriteLine("DNI:");
            client.Dni_Client = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("NAME:");
            client.Name_Client = Console.ReadLine();
            Console.WriteLine("SURNAME:");
            client.Surname_Client = Console.ReadLine();
            Console.WriteLine("EMAIL: ");
            client.Email_Client = Console.ReadLine();
        }

        private void newbookDetails()
        {
            Console.Title = "DETAILS BOOK";
            Console.WriteLine("SINCE DATE:");
            book_Details.Since_Date = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("UNTIL DATE:");
            book_Details.Until_Date = Convert.ToDateTime(Console.ReadLine());
        }

        private void newChangeStatus()
        {
            Console.Title = "CHANGE STATUS";
            Console.WriteLine("NUMBER ROOM:");
            room.Number_Room = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("NEW STATUS:");
            book_Details.Status_Room = Console.ReadLine();
        }
    }
}