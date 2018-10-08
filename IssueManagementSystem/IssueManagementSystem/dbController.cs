using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IssueManagementSystem
{
    public class dbController
    {

        public SqlConnection cnn;


        public  dbController()
            {
                string connetionString;
                connetionString = @"Data Source=RASITHA;Initial Catalog=IMS_TEMP_DB;User ID=admin;Password=1234";
                //Add correct connection string 
                cnn = new SqlConnection(connetionString);
                cnn.Open();

            }

        public void runQuery_update_or_delete(String query) {

            // updateCmd.Parameters.Clear();
            //updateCmd.Parameters.AddWithValue(@key, MyKey);


            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            cmd.ExecuteNonQuery();

        }

        public SqlDataReader runQuery_select(String query)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            SqlDataReader reader = cmd.ExecuteReader();

              /*
                    if (reader.HasRows)
                    {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0}\t{1}", reader.GetInt32(0),
                                reader.GetString(1));
                            }
                        }
                        else
                        {
                            Console.WriteLine("No rows found.");
                        }
                        reader.Close();
                    }
             */

            return reader;
        }



    }
}