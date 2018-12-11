using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using Helpers;
using System.Data;

namespace Auditur.Negocio
{
    public class Agencias : ABM<Agencia>
    {
        public Agencias() : this(AccesoDatos.OpenConn())
        { }

        public Agencias(SqlCeConnection connection)
            : base(tablename, DataReaderAObjeto, connection)
        { }

        private const string tablename = "Agencias";

        public void Insertar(Agencia oAgencia)
        {
            string query =
                AuditurHelpers.QueryInsert(new string[] { "ID", "Nombre" }, tablename);
            List<SqlCeParameter> lstParameters = new List<SqlCeParameter>();
            lstParameters.Add(new SqlCeParameter("p1", oAgencia.ID));
            lstParameters.Add(new SqlCeParameter("p2", oAgencia.Nombre));
            AccesoDatos.ExecuteNonQuery(query, CommandType.Text, lstParameters, conn);
        }

        public void Modificar(Agencia oAgencia)
        {
            string query =
                AuditurHelpers.QueryUpdate(oAgencia.ID, new string[] { "Nombre" }, tablename);
            List<SqlCeParameter> lstParameters = new List<SqlCeParameter>();
            lstParameters.Add(new SqlCeParameter("p1", oAgencia.Nombre));
            AccesoDatos.ExecuteNonQuery(query, CommandType.Text, lstParameters, conn);
        }

        private static Agencia DataReaderAObjeto(SqlCeDataReader rdrLector)
        {
            Agencia oAgencia = new Agencia();
            oAgencia.ID = rdrLector.GetInt64(rdrLector.GetOrdinal("ID"));
            oAgencia.Nombre = rdrLector.GetString(rdrLector.GetOrdinal("Nombre"));
            return oAgencia;
        }
    }
}
