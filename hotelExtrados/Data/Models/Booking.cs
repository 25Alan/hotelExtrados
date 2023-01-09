using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelExtrados.Data.Models
{
    public class Booking
    {
        public int Id_Booking { get; set; }
        public int Id_Booking_Room { get; set; }
        public int Id_Booking_Client { get; set; }
    }
}
