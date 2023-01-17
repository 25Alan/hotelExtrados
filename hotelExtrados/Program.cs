using hotelExtrados.Business.Services;
using System;

namespace hotelExtrados
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu.Menu menu = new Menu.Menu();
            menu.MenuPrincipal();

            Console.Read();
        }
    }
}
