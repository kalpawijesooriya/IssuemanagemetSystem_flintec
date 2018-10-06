using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IssueManagementSystem
{
    public class dbController
    {
        public SqlConnection connectDb()
            {
                string connetionString;
                SqlConnection cnn;
                connetionString = @"Data Source=WIN-50GP30FGO75;Initial Catalog=Demodb;User ID=sa;Password=demol23";
                cnn = new SqlConnection(connetionString);
                cnn.Open();
                return cnn;
            }
    }
}