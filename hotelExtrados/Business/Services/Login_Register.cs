using Dapper;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using hotelExtrados.Models;
using System.Data;
using System.Data.SqlTypes;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using hotelExtrados.Data.Models;
using System.Security.Policy;
using System.Net.Mail;

namespace hotelExtrados.Services
{
    public class Login_Register
    {
        private static string connectHotel = 
            ConfigurationManager.ConnectionStrings["hotelExtrados"].ConnectionString.ToString();
        SqlConnection connectSql = new SqlConnection(connectHotel);

        public bool LoginUser(PasswordI passwordInput, string name_User)
        {
            using (connectSql)
            {
                string sql = @"SELECT hash_Password FROM listUser WHERE name_User = @name_User";

                var user = connectSql.Query<UserInput>(sql, new { name_User }).ToList();

                return VerifyPassword(passwordInput.Password, user.Select(x => x.Hash_Password).FirstOrDefault().Value);
            }
        }

        /// <summary>
        /// Create a new user | Admin / Aap |
        /// </summary>
        /// <param name="passwordInput">Password entered</param>
        /// <param name="db_user">Object with the data of the new user</param>
        public void CreateUser(string passwordInput, UserInput db_user)
        {
            string salt, hash;
            CreatePassword(passwordInput, out salt, out hash);

            using (connectSql)
            {
                var saltBinary = new SqlBinary(Encoding.UTF8.GetBytes(salt));
                var hashBinary = new SqlBinary(Encoding.UTF8.GetBytes(hash));

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@nameUser", db_user.Name_User, DbType.String, ParameterDirection.Input, 20);
                parameters.Add("@saltPassword", saltBinary.Value, DbType.Binary);
                parameters.Add("@hashPassword", hashBinary.Value, DbType.Binary);
                parameters.Add("@statusAap", db_user.Status_Aap);
                parameters.Add("@statusAdmin", db_user.Status_Admin);
                connectSql.Execute("newUserRegister", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public void CreatePassword(string password, out string salt, out string hash)
        {
            salt = BCrypt.Net.BCrypt.GenerateSalt();
            hash = BCrypt.Net.BCrypt.HashPassword(password, salt);            
        }

        public bool VerifyPassword(string passwordInput, SqlBinary storedHash)
        {
            byte[] hashBytes = storedHash.Value;
            string hash = Encoding.UTF8.GetString(hashBytes);
            return BCrypt.Net.BCrypt.Verify(passwordInput, hash);
        }
    }
}

#region 1INTENTO
//SqlBinary hashInput = Hash(passwordInput);
//using (connectSql)
//{
//    connectSql.Open();
//    string sql = "INSERT INTO listUser(name_User,salt_Password,hash_Password,status_Aap,status_Admin) VALUES (@name_User, @salt_Password, @hash_Password, @status_Aap, @status_Admin)";

//    DynamicParameters parameters = new DynamicParameters();
//    parameters.Add("@name_User", db_user.Name_User, DbType.String, ParameterDirection.Input, 20);
//    parameters.Add("@salt_Password", hashInput.Value.Take(16).ToArray());
//    parameters.Add("@hash_Password", hashInput.Value.Skip(16).Take(20).ToArray());
//    parameters.Add("@status_Aap", db_user.Status_Aap, DbType.Boolean, ParameterDirection.Input);
//    parameters.Add("@status_Admin", db_user.Status_Admin, DbType.Boolean, ParameterDirection.Input);

//    int rows = connectSql.Execute(sql, parameters);

//    Console.WriteLine($"\n{rows} new user");
//    Console.Read();
//}
#endregion