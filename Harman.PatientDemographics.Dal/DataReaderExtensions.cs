using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Harman.PatientDemographics.Dal
{
   public static class DataReaderExtensions
    {
        public static async Task<T> GetDrValue<T>(this IDataReader dr, int index)
        {
            if (dr.GetValue(index) == DBNull.Value)
                return default(T);
            return await Task.FromResult((T)dr.GetValue(index));
        }

        public static T GetDrValue<T>(this IDataReader dr, string name)
        {
            if (dr[name] == DBNull.Value)
                return default(T);
            return (T)dr[name];
        }

        public  static async Task<IEnumerable<TEntity>> ToListAsync<TEntity>(this IDataReader dr, Func<IDataReader, TEntity> mapper)
        {
            var entities = new List<TEntity>();
            while (dr.Read())
            {
                var entity = mapper(dr);
                entities.Add(entity);
            }
            return await Task.FromResult(entities);
        }

        public static async  Task<TEntity> Map<TEntity>(this IDataReader dr, Func<IDataReader, TEntity> mapper)
        {
            var entity = default(TEntity);
            if (dr.Read())
            {
                entity = mapper(dr);
            }
            return await Task.FromResult(entity);
        }
    }
}
