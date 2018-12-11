using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using Helpers;
using System.Data;

namespace Auditur.Negocio
{
    public class ABM<T>
        where T : class, new()
    {
        private string _tableName;
        private Func<SqlCeDataReader, T> dataReaderAObjeto;
        public SqlCeConnection conn;

        public ABM(string tableName, Func<SqlCeDataReader, T> function, SqlCeConnection connection)
        {
            dataReaderAObjeto = function;
            _tableName = tableName;
            conn = connection;
        }

        public void CloseConnection()
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public virtual List<T> GetAll()
        {
            T objeto = default(T);
            SqlCeDataReader rdrLector;
            List<T> returnList = new List<T>();

            string query = AuditurHelpers.QueryGetAll(_tableName);

            rdrLector = AccesoDatos.ExecuteReader(query, CommandType.Text, conn);

            if (rdrLector != null)
            {
                while (rdrLector.Read())
                {
                    objeto = dataReaderAObjeto(rdrLector);
                    if (!objeto.Equals(default(T)))
                        returnList.Add(objeto);
                }
                rdrLector.Close();
            }

            return returnList;
        }

        public T GetByID(long ID)
        {
            T objeto = default(T);
            SqlCeDataReader rdrLector;
            
            string query = AuditurHelpers.QueryGetByID(ID, _tableName);

            rdrLector = AccesoDatos.ExecuteReader(query, CommandType.Text, conn);

            if (rdrLector != null)
            {
                if (rdrLector.Read())
                    objeto = dataReaderAObjeto(rdrLector);
                rdrLector.Close();
            }
            return objeto;
        }

        public List<T> GetByParameters(List<SqlCeParameter> parameters)
        {
            List<T> returnList = new List<T>();
            T objeto = default(T);
            SqlCeDataReader rdrLector;

            List<string> Columns = new List<string>();
            parameters.ForEach(x => Columns.Add(x.ParameterName));

            string query = AuditurHelpers.QueryGetByParameters(Columns.ToArray(), _tableName);

            rdrLector = AccesoDatos.ExecuteReader(query, CommandType.Text, parameters, conn);

            if (rdrLector != null)
            {
                while (rdrLector.Read())
                {
                    objeto = dataReaderAObjeto(rdrLector);
                    if (!objeto.Equals(default(T)))
                        returnList.Add(objeto);
                }
                rdrLector.Close();
            }
            return returnList;
        }

        public long GetLastID()
        {
            long lngReturn = 0;

            string query = AuditurHelpers.QueryGetLastID(_tableName);

            SqlCeDataReader rdrLector = AccesoDatos.ExecuteReader(query, CommandType.Text, conn);

            if (rdrLector != null)
            {
                if (rdrLector.Read())
                    lngReturn = rdrLector.GetInt64(rdrLector.GetOrdinal("ID"));
                rdrLector.Close();
            }

            return lngReturn;
        }

        public int DeleteByParameters(List<SqlCeParameter> parameters)
        {
            int affectedRows = 0;
            List<string> Columns = new List<string>();
            parameters.ForEach(x => Columns.Add(x.ParameterName));

            string query = AuditurHelpers.QueryDeleteByParameters(Columns.ToArray(), _tableName);

            affectedRows = AccesoDatos.ExecuteNonQuery(query, CommandType.Text, parameters, conn);

            return affectedRows;
        }
    }
}
