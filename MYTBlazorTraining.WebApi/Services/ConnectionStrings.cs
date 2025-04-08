using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;

namespace MYTBlazorTraining.WebApi.Services
{
    public class ConnectionStrings
    {
        public static SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "MYTDotNetCore",
            UserID = "sa",
            Password = "sasa@123",
            TrustServerCertificate = true,
        };
    }
}
