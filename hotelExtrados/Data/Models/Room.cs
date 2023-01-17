namespace hotelExtrados.Data.Models
{
    public class Room
    {
        public int Id_Room { get; set; }
        public int Number_Room { get; set; }
        public int Number_Beds { get; set; }
        public decimal Price_Night { get; set; }
        public string Status_Room { get; set; }  
    }

    public class Room_status : Room
    {
        public int Id_Status_Room { get; set; }
        public bool Status_Vip { get; set; }
        public bool Status_Normal { get; set; }
        public bool Status_Tv { get; set; }
        public bool Status_Garage { get; set; }
        public bool Status_BreakFast { get; set; }
        public bool Status_RoomService { get; set; }
        public bool Status_Whirlpool { get; set; }
    }
}
