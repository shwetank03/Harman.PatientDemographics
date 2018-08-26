using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Harman.PatientDemographics.Dal.Contract
{
  public  interface IDal:IDisposable
    {
        Task<SqlCommand> CreateCommandAsync(string sqlText, CommandType commandType);
        Task<SqlCommand> CreateCommandAsync(string sqlText, IDataParameter[] dbParams, CommandType commandType);
        Task<SqlCommand> CreateCommandAsync(SqlConnection conn, SqlCommand cmd, IDataParameter[] cmdParams);
        Task<IDataParameter> GetParameterAsync<T>(string paramName, T paramValue);
        Task<T> SelectAsync<T>(SqlCommand cmd, Func<IDataReader, T> mapper);
        Task<T> SelectAsync<T>(string storedProcName, Func<IDataReader, T> mapper);
        Task<T> SelectAsync<T>(string storedProcName, IDataParameter[] parameters, Func<IDataReader, T> mapper);
        Task<T> SelectSqlAsync<T>(string storedProcName, Func<IDataReader, T> mapper);
        Task<IDataReader> SelectAsync(string storedProcName, IDataParameter[] parameters = null);
        Task<IDataReader> SelectSqlAsync(string sql);
        Task<int> ExecuteSqlAsync(SqlCommand cmd);
        Task<int> ExecuteSqlAsync(string sqlText, IDataParameter[] param, CommandType commandType);
        Task<int> ExecuteSqlAsync(string sqlText, CommandType commandType);
        Task<T> ExecuteScalarAsync<T>(string sqlText, IDataParameter[] param, CommandType commandType);
        Task<T> ExecuteScalarAsync<T>(SqlCommand cmd);
    }
}
