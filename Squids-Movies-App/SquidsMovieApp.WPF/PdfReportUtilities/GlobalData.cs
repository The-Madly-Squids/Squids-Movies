using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquidsMovieApp.WPF.PdfReportUtilities
{
    public class GlobalData
    {
        SqlConnection con = new SqlConnection("Server=.\\SQLEXPRESS;Database=MovieApp;Integrated Security=True;");
        public GlobalData() { }
        public DataTable GetData(string query)
        {
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dtToReturn = new DataTable();
            da.Fill(dtToReturn);
            return dtToReturn;
        }
    }
}
