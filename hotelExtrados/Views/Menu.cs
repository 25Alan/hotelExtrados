using hotelExtrados.Business.Services;
using hotelExtrados.Data.Models;
using hotelExtrados.Models;
using hotelExtrados.Services;
using hotelExtrados.Views;
using System;

namespace hotelExtrados.Menu
{
    public class Menu
    {

        private Login_Register login_register = new Login_Register();
        private UserInput newUser = new UserInput();
        private PasswordI password = new PasswordI();
        ConsoleKeyInfo option = new ConsoleKeyInfo();

        public Menu() { }

        public void MenuPrincipal()
        {
            Console.Title = "WELCOME";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[1] - LOGIN\n[2] - Register");
            option = Console.ReadKey();

            if (option.Key == ConsoleKey.NumPad1 || option.Key == ConsoleKey.D1)
                Console.Clear(); MenuLogin();

            if (option.Key == ConsoleKey.NumPad2 || option.Key == ConsoleKey.D2)
                Console.Clear(); MenuRegister();
        }

        public void MenuLogin()
        {
            MenuAap menuAap = new MenuAap();
            MenuAdmin menuAdmin = new MenuAdmin();

            Console.Clear();
            Console.Title = "LOGIN";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("------LOGIN------");
            Console.WriteLine("ENTER USERNAME");
            newUser.Name_User = Console.ReadLine();
            Console.WriteLine("ENTER PASSWORD");
            password.Password = Console.ReadLine();

            if (login_register.LoginUser(password, newUser.Name_User) == true && login_register.ValidateRole(newUser.Name_User) == false) { Console.Clear(); menuAap.MenuPrincipalAap(); }
            else Console.Clear(); menuAdmin.MenuPrincipalAdmin(); 

            Console.Read();
            Console.Clear();
        }

        public void MenuRegister()
        {
            Console.Title = "REGISTER";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("------REGISTER-------");
            Console.WriteLine("ENTER USERNAME");
            newUser.Name_User = Console.ReadLine();
            Console.WriteLine("ENTER PASSWORD");
            password.Password = Console.ReadLine();
            Console.WriteLine("SELECT RANGE\n[1] - Aap (Public Attention)\n[2] - Admin (Administrator)");
            option = Console.ReadKey();
            if (option.Key == ConsoleKey.NumPad1 || option.Key == ConsoleKey.D1)
                newUser.Status_Aap = true; else newUser.Status_Admin = true;

            login_register.CreateUser(password.Password, newUser);
            Console.Clear();
            MenuPrincipal();
        }
    }
}