using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Econtact1.econtactClasses
{
    class contactClass
    {
        //Getter Setter Properties
        //Acts as Data Carrier in Our Application

        public int ContactID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ContactNo { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        //Selecting Data from Database

        public DataTable Select()

        {
            //STEP1 : Database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();

            try
            {
                //step2 writing sql Query
                string sql = "SELECT * FROM tbl_contact";
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Creating sql DataAdapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        //INSERTING DATA INTO DATABASE
        public bool Insert(contactClass c)
        {
            //creating a default written type and setting its value to false

            bool isSuccess = false;

            //step 1 Connect Databse
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //step 2create sql query to insert data
                string sql = "INSERT INTO tbl_contact (FirstName, LastName,  ContactNo,Address, Gender)VALUES (@FirstName, @LastName, @ContactNo, @Address, @Gender)";
                //sql command using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //create Parameters to add data
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);

                //connection Open Here
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //if the query runs successfully then the value will be greater than zero else its value will be 0

                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        //mETHOD TO UPDATE data in database from our application
        public bool Update(contactClass c)
        {


            //create a default return type and set it value to  false
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //sql to update data in our database
                string sql = "UPDATE tbl_contact SET FirstName=@FirstName, LastName=@LastName, ContactNo=@ContactNo, Address=@Address, Gender=@Gender WHERE ContactID=@ContactID";
                //sql command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Create Parameters to add value
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("ContactID", c.ContactID);
                //OPEN CONN DATABASE
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                //if the query successfully then the value of rows will be greater than zero

                if (rows > 0)
                {
                    isSuccess = true;

                }
                else
                {

                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        //Method to delete data from database
        public bool Delete(contactClass c)
        {
            //create a default return value and set its value to false
            bool isSuccess = false;
            //create sql connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //sql to delete data
                string sql = "DELETE FROM tbl_contact WHERE ContactID=@ContactID";

                //CREATING SQL COMMAND
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
                //open Connection
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //IF THE QUERY EXECUTES SUCCESSFULLY then value of rows is greater than zero
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                //Close Connection
                conn.Close();
            }
            return isSuccess;
        }

    }
}