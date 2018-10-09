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


        public SqlDataReader get_1st_column_1st_row_data(String query)
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
}