using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelExtrados.Data.Models
{
    public class Room
    {
        public int Id_Room { get; set; }
        public int Number_Room { get; set; }
        public int Number_Beds { get; set; }
        public decimal Price_Night { get; set; }
    }

    public class RoomVip : Room
    {
        public bool Status_Vip { get; set; }
    }
}
