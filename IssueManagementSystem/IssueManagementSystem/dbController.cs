using IssueManagementSystem.Models;
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

        private  SqlConnection cnn;
        static dbController dbCtrlObject ;

        public static dbController getInstance()
        {
            if (dbCtrlObject==null) {
                dbCtrlObject=  new dbController();
                return dbCtrlObject;
            }

            else
                return dbCtrlObject;
        }

        private dbController()
            {
                string connetionString;
                connetionString = System.Configuration.ConfigurationManager.ConnectionStrings["ims"].ConnectionString;
                cnn = new SqlConnection(connetionString);
                cnn.Open();
            }

        public void runQuery_update_or_delete(String query) {

            //updateCmd.Parameters.Clear();
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

            return reader;
        }


        public string get_1st_column_1st_row_data(String query)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            String data = null;
            SqlDataReader reader = cmd.ExecuteReader();

            
                      if (reader.HasRows)
                      {
                            while (reader.Read())
                            {
                                data = reader.GetValue(0).ToString();
                            }
                      }
                      else
                      {
                          Console.WriteLine("No rows found.");
                      }
                      reader.Close();
                 
            return data;
        }
        public issue_management_systemEntities1 Databaseobject()
        {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                return db;
            }

        }
    }
   
}
