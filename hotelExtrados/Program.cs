using hotelExtrados.Business.Services;
using hotelExtrados.Data.Models;
using hotelExtrados.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelExtrados
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Menu.Menu menu = new Menu.Menu();
            //menu.MenuPrincipal();

            Aap aap = new Aap();

            #region CosolaTabla
            //List<List<string>> table = new List<List<string>>
            //{
            //new List<string> { "ID", "Nombre", "Apellido" },
            //new List<string> { "1", "Juan", "Pérez" },
            //new List<string> { "2", "Ana", "González" }
            //};

            //foreach (List<string> row in table)
            //{
            //    foreach (string cell in row)
            //    {
            //        Console.Write("|" + cell.PadRight(15));
            //    }
            //    Console.WriteLine("|");
            //}
            #endregion
            Console.Read();
        }
    }
}
