using Dapper;
using hotelExtrados.Data.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace hotelExtrados.Business.Services
{
    public class Aap
    {
        private static string connectHotel =
            ConfigurationManager.ConnectionStrings["hotelExtrados"].ConnectionString.ToString();
        SqlConnection connectSql = new SqlConnection(connectHotel);

        /// <summary>
        /// Room list 
        /// </summary>
        public void ListRoomProperties ()
        {
            using (connectSql)
            {
                List<Room_status> rooms = connectSql.Query<Room_status>("showListRoom", commandType:CommandType.StoredProcedure).ToList();

                foreach (var room in rooms )
                {
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("-----------------------------------------------");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    #region CWList
                    Console.WriteLine
                    ($"[Number Room: {room.Number_Room}]\n[Number Beds: {room.Number_Beds}]\n[Status Normal: {room.Status_Normal}]\n[Status VIP: {room.Status_Vip}]\n[Status TV: {room.Status_Tv}]\n[Status Garage: {room.Status_Garage}]\n[Status BreakFast: {room.Status_BreakFast}]\n[Status Room Service: {room.Status_RoomService}]\n[Status Whirlpool: {room.Status_Whirlpool}]"); 
                    #endregion
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("-----------------------------------------------");
                }
            }
        }

        public void ListRoomStatus()
        {
            using (connectSql)
            {
                List<Room> rooms = connectSql.Query<Room>("showListRoomStatus", commandType: CommandType.StoredProcedure).ToList();

                foreach (var room in rooms)
                {
                    Console.WriteLine($"Number Room: {room.Number_Room}\nNumber Beds: {room.Number_Beds}\nPrice Night: {room.Price_Night}\nStatus Room: {room.Status_Room}");
                }
            }
        }

        /// <summary>
        /// Add new client
        /// </summary>
        /// <param name="newClient">Client object and its values</param>
        public void newClient(Client newClient)
        {
            using (connectSql)
            {
                int newCl = connectSql.Execute("newClient_booking_Client", newClient, commandType: CommandType.StoredProcedure);

                Console.WriteLine($"{newCl} new client add");
                Console.Read();
            }
        }

        /// <summary>
        /// New reservation
        /// </summary>
         public void newBook(int id_Booking_Room, int id_Booking_Client)
        {
            using (connectSql)
            {
                string sql = @"INSERT INTO booking_room (id_Booking_Room, id_Booking_Client) VALUES (@id_Booking_Room, @id_Booking_Client";

                var confirmBook = connectSql.Execute(sql, new {id_Booking_Room,id_Booking_Client});

                Console.WriteLine($"{confirmBook} confirmed reservation");
            }
        }

        public int selectClient(int dni_CLient)
        {
            using (connectSql)
            {
                string sql = @"SELECT dni_Client from booking_client where dni_Client = @dni_Client";

                List<Client> indexClient = connectSql.Query<Client>(sql, new { dni_CLient }).ToList();

                return indexClient.Select(x => x.Dni_Client).FirstOrDefault();
            }
        }

        public int selectRoom(int number_Room)
        {
            using (connectSql)
            {
                string sql = @"SELECT id_Room from room where number_Room = @number_Room";

                List<Room> idRoom = connectSql.Query<Room>(sql, new { number_Room }).ToList();

                return idRoom.Select(x => x.Id_Room).FirstOrDefault();
            }
        }
    }
}
