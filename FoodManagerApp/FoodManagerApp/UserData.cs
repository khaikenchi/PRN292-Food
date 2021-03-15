using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManagerApp
{
    public class UserData
    {
        public string strConnection;

        public UserData()
        {
            strConnection = getConnectionString();
        }

        public string getConnectionString()
        {
            string connection = "server=localhost;database=ProductData;uid=sa;pwd=123123";
            return connection;
        }

        public User CheckLogin(string Username, string Password)
        {
            string sql = "SELECT * " +
                         "FROM [User] " +
                         "WHERE Username = @ID AND Password = @Password";
            SqlConnection conn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ID", Username);
            cmd.Parameters.AddWithValue("@Password", Password);
            SqlDataReader reader;
            User result = null;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new User(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), 
                            reader.GetString(4), reader.GetString(5), reader.GetBoolean(6), reader.GetBoolean(7));
                    }
                }
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }//end CheckLogin

        public bool LockUser(string username)
        {
            bool status = false;
            bool result;
            SqlConnection cnn = new SqlConnection(strConnection);
            string SQL = "Update [User] " +
                         "Set Status=@Status " +
                         "Where Username=@ID";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@ID", username);
            cmd.Parameters.AddWithValue("@Status", status);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { cnn.Close(); }
            return result;
        }//end LockUser

        public bool RegisterNewAccount(User u)
        {
            bool result;
            SqlConnection cnn = new SqlConnection(strConnection);
            string sql = "Insert [User] " +
                         "Values(@Username,@Password,@Fullname,@Addr,@Email,@Phone,@Role,@Status)";
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@Username", u.username);
            cmd.Parameters.AddWithValue("@Password", u.password);
            cmd.Parameters.AddWithValue("@Fullname", u.fullname);
            cmd.Parameters.AddWithValue("@Addr", u.address);
            cmd.Parameters.AddWithValue("@Email", u.email);
            cmd.Parameters.AddWithValue("@Phone", u.phone);
            cmd.Parameters.AddWithValue("@Role", u.role);
            cmd.Parameters.AddWithValue("@Status", u.status);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally { cnn.Close(); }
            return result;
        }//end RegisterNewAccount

        public bool checkUsernameExist(string username)
        {
            bool result = false;
            string sql = "SELECT Fullname " +
                         "FROM [User] " +
                         "WHERE Username=@Username";
            SqlConnection conn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Username", username);
            SqlDataReader reader;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string tmp = reader.GetString(0);
                        if (!string.IsNullOrEmpty(tmp))
                        {
                            result = true;
                        }
                    }
                }
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }//end checkUsernameExist

    }//end class
}
