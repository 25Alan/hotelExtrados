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
        public void ListRoom ()
        {
            using (connectSql)
            {
                List<RoomVip> rooms = connectSql.Query<RoomVip>("showListRoom", commandType:CommandType.StoredProcedure).ToList();

                foreach (var room in rooms )
                {
                    Console.WriteLine($"[Status VIP : { room.Status_Vip }]"); //FALTA AGREGAR
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
        public void newBook()
        {
            using (connectSql)
            {

            }
        }
    }
}
