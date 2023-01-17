using Dapper;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using hotelExtrados.Models;
using System.Data;
using System.Data.SqlTypes;
using System.Text;

namespace hotelExtrados.Services
{
    public class Login_Register
    {
        private static string connectHotel = 
            ConfigurationManager.ConnectionStrings["hotelExtrados"].ConnectionString.ToString();
        SqlConnection connectSql = new SqlConnection(connectHotel);

        /// <summary>
        /// Valid password check and login
        /// </summary>
        /// <param name="passwordInput">Object with password</param>
        /// <param name="name_User">Username entered</param>
        /// <returns>True if password is valid - False if password is not valid</returns>
        public bool LoginUser(PasswordI passwordInput, string name_User)
        {
            connectSql.Open();
                string sql = @"SELECT hash_Password FROM listUser WHERE name_User = @name_User";
                var user = connectSql.Query(sql, new { name_User }).FirstOrDefault();

            byte[] hashBytes = (byte[])user.hash_Password;
            SqlBinary hashBinary = new SqlBinary(hashBytes);
            connectSql.Close();

            return VerifyPassword(passwordInput.Password, hashBinary);
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

            connectSql.Open();
                var saltBinary = new SqlBinary(Encoding.UTF8.GetBytes(salt));
                var hashBinary = new SqlBinary(Encoding.UTF8.GetBytes(hash));

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@nameUser", db_user.Name_User, DbType.String, ParameterDirection.Input, 20);
                parameters.Add("@saltPassword", saltBinary.Value, DbType.Binary);
                parameters.Add("@hashPassword", hashBinary.Value, DbType.Binary);
                parameters.Add("@statusAap", db_user.Status_Aap);
                parameters.Add("@statusAdmin", db_user.Status_Admin);
                connectSql.Execute("newUserRegister", parameters, commandType: CommandType.StoredProcedure);
            connectSql.Close();
        }

        /// <summary>
        /// Validate Role
        /// </summary>
        /// <param name="name_User">Username entered</param>
        /// <returns>True = Admin / False = Aap</returns>
        public bool ValidateRole(string name_User)
        {
            connectSql.Open();
                string sql = @"SELECT status_Admin FROM listUser WHERE name_User = @name_User";
                var query = connectSql.ExecuteScalar<bool>(sql, new { name_User });
            connectSql.Close();
            return query;
        }

        /// <summary>
        /// Hash the entered password
        /// </summary>
        /// <param name="password">Password entered</param>
        /// <param name="salt">Salt generated</param>
        /// <param name="hash">Hash generated</param>
        private void CreatePassword(string password, out string salt, out string hash)
        {
            salt = BCrypt.Net.BCrypt.GenerateSalt();
            hash = BCrypt.Net.BCrypt.HashPassword(password, salt);            
        }

        /// <summary>
        /// Password verification
        /// </summary>
        /// <param name="passwordInput">Password entered</param>
        /// <param name="storedHash">Hash saved in database</param>
        /// <returns>True if password is valid | False if password is not valid</returns>
        private bool VerifyPassword(string passwordInput, SqlBinary storedHash)
        {
            byte[] hashBytes = storedHash.Value;
            string hash = Encoding.UTF8.GetString(hashBytes);
            return BCrypt.Net.BCrypt.Verify(passwordInput, hash);
        }
    }
}

#region Primer_INTENTO
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