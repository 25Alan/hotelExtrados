using Dapper;
using hotelExtrados.Data.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace hotelExtrados.Business.Services
{
    public class Aap
    {
        private static string connectHotel =
            ConfigurationManager.ConnectionStrings["hotelExtrados"].ConnectionString.ToString();
        SqlConnection connectSql = new SqlConnection(connectHotel);

        /// <summary>
        /// List of rooms with its properties
        /// </summary>
        public void ListRoomProperties ()
        {
            connectSql.Open ();
                List<Room_status> rooms = connectSql.Query<Room_status>("showListRoom", commandType:CommandType.StoredProcedure).ToList();

                foreach (var room in rooms )
                {
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("-----------------------------------------------");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    #region CWList
                    Console.WriteLine
                    ($"[Number Room: {room.Number_Room}]\n[Number Beds: {room.Number_Beds}]\n[Status Normal:{room.Status_Normal}]\n[Status VIP: {room.Status_Vip}]\n[Status TV: {room.Status_Tv}]\n[Status Garage: {room.Status_Garage}]\n[Status BreakFast: {room.Status_BreakFast}]\n[Status Room Service: {room.Status_RoomService}]\n[Status Whirlpool: {room.Status_Whirlpool}]"); 
                    #endregion
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("-----------------------------------------------");
                }
            connectSql.Close();
        }

        /// <summary>
        /// List of rooms with their prices and status 
        /// </summary>
        public void ListRoomStatus()
        {
            connectSql.Open();
                List<Room> rooms = connectSql.Query<Room>("showListRoomStatus", commandType: CommandType.StoredProcedure).ToList();

                foreach (var room in rooms)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("------------------------");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Number Room: {room.Number_Room}\nNumber Beds: {room.Number_Beds}\nPrice Night: {room.Price_Night}\nStatus Room: {room.Status_Room}");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("------------------------");
                }
            connectSql.Close();
        }

        /// <summary>
        /// Add new client
        /// </summary>
        /// <param name="newClient">Client object and its values</param>
        public void newClient(Client newClient)
        {
            connectSql.Open();
                int newCl = connectSql.Execute("newClient_booking_Client", newClient, commandType: CommandType.StoredProcedure);

                Console.WriteLine($"{newCl} new client add");
                Console.Read();
            connectSql.Close();
        }

        /// <summary>
        /// New reservation
        /// </summary>
        public void newBook(int id_Booking_Room, int id_Booking_Client)
        {
        connectSql.Open();
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@id_Booking_Room", id_Booking_Room, DbType.Int32);
        parameters.Add("@id_Booking_Client", id_Booking_Client, DbType.Int32);
        var confirmBook = connectSql.Execute("newBook", parameters, commandType:CommandType.StoredProcedure);
        connectSql.Close();
        try
        {
            if (confirmBook > 0) Console.WriteLine($"{confirmBook} confirmed reservation");
            else Console.WriteLine("Insert failed");
        }
        catch (Exception error)
        {
            Console.WriteLine($"Error, could not save the information | {error.Message}");
        }
        }

        public void newBookDetails(Book_details book_details)
        {
            int id = selectId();
            connectSql.Open();
            DynamicParameters parameters= new DynamicParameters();
            parameters.Add("@id_book_Booking", id, DbType.Int32);
            parameters.Add(@"since_Date", book_details.Since_Date, DbType.DateTime);
            parameters.Add("@until_Date", book_details.Until_Date, DbType.DateTime);
            var confirmDetails = connectSql.Execute("newBookDetails", parameters, commandType: CommandType.StoredProcedure);
            connectSql.Close();
        }

        public void changeStatusRoom(int number_Room)
        {
            connectSql.Open();
            string sql = @"UPDATE room SET status_Room = 'Ocupado' WHERE number_Room = @number_Room";
            var update = connectSql.Execute(sql, new { number_Room });
            connectSql.Close();
        }

        public int selectClient(int dni_CLient)
        {
            connectSql.Open();
                string sql = @"SELECT dni_Client from booking_client where dni_Client = @dni_Client";

                List<Client> indexClient = connectSql.Query<Client>(sql, new { dni_CLient }).ToList();

            connectSql.Close();
            return indexClient.Select(x => x.Dni_Client).FirstOrDefault();
        }

        public int selectRoom(int number_Room)
        {
            connectSql.Open();
                string sql = @"SELECT id_Room from room where number_Room = @number_Room";

                List<Room> idRoom = connectSql.Query<Room>(sql, new { number_Room }).ToList();
            
            connectSql.Close();
            return idRoom.Select(x => x.Id_Room).FirstOrDefault();
        }

        private int selectId ()
        {
            connectSql.Open();
            string sql = @"SELECT MAX(id_Booking) FROM booking_room";
            var idQuery = connectSql.ExecuteScalar<int>(sql);
            connectSql.Close();
            return idQuery;
        }
    }
}