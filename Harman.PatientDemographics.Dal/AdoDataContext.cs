using Harman.PatientDemographics.Dal.Contract;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Harman.PatientDemographics.Dal
{
    public class AdoDataContext : IAdoDataContext
    {
        private bool _disposed;

        private readonly SqlConnection _conn;

        public AdoDataContext(string connectionString)
        {
            _conn = new SqlConnection(connectionString);
        }

        public async Task<Dal> GetDbAsync()
        {
            var dbConnection = await CreateConnectionAsync();
            return await Task.FromResult(new Dal(dbConnection));
        }

        private async Task<SqlConnection> CreateConnectionAsync()
        {
            if (_conn != null)
            {
                await _conn.OpenAsync();
            }
            return await Task.FromResult(_conn);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _conn.Dispose();
            }
            _disposed = true;
        }
    }
}
