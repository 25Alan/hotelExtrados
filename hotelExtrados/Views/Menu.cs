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
        private Login_Register db_newUser = new Login_Register();
        private UserControl newUser = new UserControl();
        ConsoleKeyInfo option = new ConsoleKeyInfo();
        public Menu(){}

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
            newUser.Password_User = Console.ReadLine();

            #region REQUIRED_FALTA_SABER_APLICARLO
            //var context = new ValidationContext(db_newUser, null, null);
            //var results = new List<ValidationResult>();
            //var isValid = Validator.TryValidateObject(db_newUser, context, results, true);
            //if (!isValid)
            //{
            //    foreach (var validationResult in results)
            //    {
            //        Console.WriteLine(validationResult.ErrorMessage);
            //    }
            //}
            #endregion

            bool valid = db_newUser.ValidateLogin(newUser.Name_User, newUser.Password_User);

            if (valid == true) Console.WriteLine("CONECTADO"); else Console.WriteLine("USUARIO O CONTRASEÑA NO VALIDA");
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
            newUser.Password_User = Console.ReadLine();
            Console.WriteLine("SELECT RANGE\n[1] - Aap (Public Attention)\n[2] - Admin (Administrator)");
            option = Console.ReadKey();
            if (option.Key == ConsoleKey.NumPad1) newUser.Status_Aap = true; else newUser.Status_Admin = true;
            db_newUser.CreateUser(newUser);
            if (option.Key == ConsoleKey.Escape) Console.Clear();  MenuPrincipal();
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
        }

        public void MenuAdmin()
        {
            Console.Title = "ADMIN";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[1] - Add new room\n[2] - Change room status");
            int opc = Convert.ToInt32(Console.ReadLine());
        }

        public void StateoftheRooms()
        {
            Console.WriteLine("ESTADOS");
        }
    }
}