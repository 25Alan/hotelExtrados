using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelExtrados.Views
{
    public class MenuAdmin
    {
        public MenuAdmin() { }
        public void MenuPrincipalAdmin()
        {
            Console.Title = "ADMIN";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[1] - Add new room\n[2] - Change room status");
            int opc = Convert.ToInt32(Console.ReadLine());
        }
    }
}

