using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelExtrados.Data.Models
{
    public class Book_details
    {
        public int Id_book_Booking { get; set; }
        public DateTime Since_Date { get; set; }
        public DateTime Until_Date { get; set; }
    }

    public class Book_Number_Room : Book_details
    {
        public int number_Select { get; set; }
    }
}
