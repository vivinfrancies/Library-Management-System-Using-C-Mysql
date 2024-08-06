using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libery
{
    public class MainConncet
    {
        public SqlConnection Connection()
        {
            string connectionstring = "Server = localhost\\SQLEXPRESS; Database = libery; Trusted_Connection = true";
            return new SqlConnection(connectionstring);
        }
    }
}
