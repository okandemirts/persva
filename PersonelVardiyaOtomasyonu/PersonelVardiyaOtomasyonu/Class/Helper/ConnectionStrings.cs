using System.Data;
using System.Data.SqlClient;

namespace PersonelVardiyaOtomasyonu.Class.Helper
{
    public static class ConnectionStrings
    {
        public static SqlConnection _sqlConnection = new SqlConnection(@"Server=.\SQLEXPRESS; Initial Catalog=PersonelVardiyaOtomasyonu; Integrated Security=true; MultipleActiveResultSets=true");

        public static void ConnectionControl()
        {
            if (_sqlConnection.State != ConnectionState.Open)
            {
                _sqlConnection.Open();
            }
        }
    }
}
