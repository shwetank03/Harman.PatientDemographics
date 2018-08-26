using Harman.PatientDemographics.Dal.Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Harman.PatientDemographics.Dal
{
    public class Dal : IDal
    {
        private readonly SqlCommand _cmd;
        private readonly SqlConnection _conn;

        public Dal(SqlConnection conn)
        {
            _conn = conn;
            _cmd = _conn.CreateCommand();
        }

        public async Task<SqlCommand> CreateCommandAsync(string sqlText, CommandType commandType)
        {
            return await CreateCommandAsync(sqlText, null, commandType);
        }

        public async Task<SqlCommand> CreateCommandAsync(string sqlText, IDataParameter[] dbParams, CommandType commandType)
        {
            if (_conn.State == ConnectionState.Closed)
                throw new Exception("Database connection is not open");
            _cmd.CommandText = sqlText;
            _cmd.CommandType = commandType;
            return await CreateCommandAsync(_conn, _cmd, dbParams);
        }

        public async Task<SqlCommand> CreateCommandAsync(SqlConnection conn, SqlCommand cmd, IDataParameter[] cmdParams)
        {
            if (cmdParams != null)
            {
                cmd.Parameters.Clear();
                foreach (var param in cmdParams)
                {
                    cmd.Parameters.Add(param);
                }
            }
            cmd.CommandTimeout = 99999999;
            return await Task.FromResult(cmd);
        }

        public async Task<IDataParameter> GetParameterAsync<T>(string paramName, T paramValue)
        {
            IDataParameter param = _cmd.CreateParameter();
            param.ParameterName = paramName;
            param.Value = paramValue;
            return await Task.FromResult(param);
        }

        private async Task<IDataReader> GetReaderAsync(string sqlText, CommandType commandType)
        {
            return await GetReaderAsync(sqlText, null, commandType);
        }

        private async Task<IDataReader> GetReaderAsync(string sqlText, IDataParameter[] param, CommandType commandType)
        {
            return await GetReaderAsync(await CreateCommandAsync(sqlText, param, commandType));
        }

        private async Task<IDataReader> GetReaderAsync(SqlCommand cmd)
        {
            return await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        }

        public async Task<T> SelectAsync<T>(SqlCommand cmd, Func<IDataReader, T> mapper)
        {
            using (IDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection))
            {
                var results = mapper(dr);
                return await Task.FromResult(results);
            }
        }

        public async Task<T> SelectAsync<T>(string storedProcName, Func<IDataReader, T> mapper)
        {
            using (var dr = await GetReaderAsync(storedProcName, CommandType.StoredProcedure))
            {
                var results = mapper(dr);
                return await Task.FromResult(results);
            }
        }

        public async Task<T> SelectAsync<T>(string storedProcName, IDataParameter[] parameters, Func<IDataReader, T> mapper)
        {
            using (var dr = await GetReaderAsync(storedProcName, parameters, CommandType.StoredProcedure))
            {
                var results = await dr.Map(mapper);
                return await Task.FromResult(results);
            }
        }

        public async Task<T> SelectSqlAsync<T>(string storedProcName, Func<IDataReader, T> mapper)
        {
            using (var dr = await GetReaderAsync(storedProcName, CommandType.Text))
            {
                var results = mapper(dr);
                return await Task.FromResult(results);
            }
        }

        public async Task<IDataReader> SelectAsync(string storedProcName, IDataParameter[] parameters = null)
        {
            return await GetReaderAsync(storedProcName, parameters, CommandType.StoredProcedure);
        }

        public async Task<IDataReader> SelectSqlAsync(string sql)
        {
            return await GetReaderAsync(sql, null, CommandType.Text);
        }

        public async Task<int> ExecuteSqlAsync(SqlCommand cmd)
        {
            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<int> ExecuteSqlAsync(string sqlText, IDataParameter[] param, CommandType commandType)
        {
            return await ExecuteSqlAsync(CreateCommandAsync(sqlText, param, commandType).Result);
        }

        public async Task<int> ExecuteSqlAsync(string sqlText, CommandType commandType)
        {
            return await ExecuteSqlAsync(sqlText, null, commandType);
        }

        public async Task<T> ExecuteScalarAsync<T>(string sqlText, IDataParameter[] param, CommandType commandType)
        {
            return await ExecuteScalarAsync<T>(await CreateCommandAsync(sqlText, param, commandType));
        }

        public async Task<T> ExecuteScalarAsync<T>(SqlCommand cmd)
        {
            return (T)await cmd.ExecuteScalarAsync();
        }

        public void Dispose()
        {
            if (_conn.State == ConnectionState.Open)
                _conn.Close();
        }
    }
}
