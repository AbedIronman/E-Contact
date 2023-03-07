using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EContact.econtactClasses
{
    class ContactClass
    {
        // we need getters and setters
        // Data Carrier
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        static string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        // selecting data from database
        public DataTable Select()
        {
            // step1: database connection
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();
            try
            {
                // step 2: write sql query
                string sql = "SELECT * FROM tbl_contact";
                // step 3: write sql command using sql and connection
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception e)
            { 
            
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        // inserting data to database
        public bool Insert(ContactClass c)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                string sql = "INSERT INTO tbl_contact (FirstName, LastName, ContactNo, Address, Gender)" +
                    " Values(@FirstName, @LastName, @ContactNo, @Address, @Gender)";
                
                // sql command using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                
                // create parameters to add data
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);


                //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                int rows = cmd.ExecuteNonQuery(); // return how many rows affected
                if (rows > 0)
                    isSuccess = true;
                else
                    isSuccess = false;
            }
            catch(Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
            
            return isSuccess;

        }

        // method to update data
        public bool Update(ContactClass c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstring);
           
            try
            {
                // SQL to update data in our database
                string sql = "UPDATE tbl_contact SET FirstName=@FirstName, " +
                    "LastName=@LastName, ContactNo=@ContactNo, Address=@Address, " +
                    "Gender=@Gender WHERE ContactID=@ContactID";
                SqlCommand cmd = new SqlCommand(sql, conn);

                // Create parameter to add value
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);

                // open database conntection
                conn.Open();
               
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                    isSuccess = true;
                else
                    isSuccess = false;
            }
            catch(Exception ex)
            {
             
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        public bool Delete(ContactClass c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstring);

            try
            {
                // SQL statements
                string sql = "DELETE FROM tbl_contact WHERE ContactID = @ContactID";

                // SQL Command
                SqlCommand cmd = new SqlCommand(sql, conn);

                // Parameters
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);

                // open connection
                conn.Open();
                // rows affected
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                    isSuccess = true;
                else
                    isSuccess = false;

            }
            catch(Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
            
            return isSuccess;
        }
    }
}
