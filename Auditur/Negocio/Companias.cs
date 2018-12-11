using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using Helpers;
using System.Data;

namespace Auditur.Negocio
{
    public class Companias : ABM<Compania>
    {
        public Companias() : this(AccesoDatos.OpenConn())
        { }

        public Companias(SqlCeConnection connection)
            : base(tablename, DataReaderAObjeto, connection)
        { }

        private const string tablename = "Companias";

        public void Insertar(Compania oCompania)
        {
            string query =
                AuditurHelpers.QueryInsert(new string[] { "ID", "Nombre", "Codigo" }, tablename);
                
            List<SqlCeParameter> lstParameters = new List<SqlCeParameter>();
            lstParameters.Add(new SqlCeParameter("p1", oCompania.ID));
            lstParameters.Add(new SqlCeParameter("p2", oCompania.Nombre)); 
            lstParameters.Add(new SqlCeParameter("p3", oCompania.Codigo));

            AccesoDatos.ExecuteNonQuery(query, CommandType.Text, lstParameters, conn);
        }

        public void Modificar(Compania oCompania)
        {
            string query =
                AuditurHelpers.QueryUpdate(oCompania.ID, new string[] { "Nombre", "Codigo" }, tablename);
            List<SqlCeParameter> lstParameters = new List<SqlCeParameter>();
            lstParameters.Add(new SqlCeParameter("p1", oCompania.Nombre));
            lstParameters.Add(new SqlCeParameter("p2", oCompania.Codigo));
            AccesoDatos.ExecuteNonQuery(query, CommandType.Text, lstParameters, conn);
        }

        public Compania ObtenerPorNombre(string Nombre)
        {
            Compania oCompania = null;
            SqlCeDataReader rdrLector;

            string query = "SELECT * FROM " + tablename + " WHERE Nombre = '" + Nombre + "'";

            rdrLector = AccesoDatos.ExecuteReader(query, CommandType.Text, conn);

            if (rdrLector != null)
            {
                if (rdrLector.Read())
                    oCompania = DataReaderAObjeto(rdrLector);
                rdrLector.Close();
            }

            return oCompania;
        }

        public override List<Compania> GetAll()
        {
            List<Compania> listadoCompanias = base.GetAll();
            listadoCompanias.OrderBy(x => x.Nombre.ToLower());
            return listadoCompanias;
        }

        private static Compania DataReaderAObjeto(SqlCeDataReader rdrLector)
        {
            Compania oCompania = new Compania();
            oCompania.ID = rdrLector.GetInt64(rdrLector.GetOrdinal("ID"));
            oCompania.Nombre = rdrLector.GetString(rdrLector.GetOrdinal("Nombre"));
            if (!rdrLector.IsDBNull(rdrLector.GetOrdinal("Codigo")))
                oCompania.Codigo = rdrLector.GetString(rdrLector.GetOrdinal("Codigo"));
            return oCompania;
        }
    }
}
