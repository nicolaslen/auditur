using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using Helpers;
using System.Data;

namespace Auditur.Negocio
{
    public class Conceptos : ABM<Concepto>
    {
        public Conceptos() : this(AccesoDatos.OpenConn())
        { }

        public Conceptos(SqlCeConnection connection)
            : base(tablename, DataReaderAObjeto, connection)
        { }

        private const string tablename = "Conceptos";

        public void Insertar(Concepto oConcepto)
        {
            string query =
                AuditurHelpers.QueryInsert(new string[] { "Nombre", "Tipo" }, tablename);
                
            List<SqlCeParameter> lstParameters = new List<SqlCeParameter>();
            lstParameters.Add(new SqlCeParameter("p1", oConcepto.Nombre));
            lstParameters.Add(new SqlCeParameter("p2", oConcepto.Tipo.ToString()));

            AccesoDatos.ExecuteNonQuery(query, CommandType.Text, lstParameters, conn);
            
            oConcepto.ID = GetLastID();
        }

        public void Modificar(Concepto oConcepto)
        {
            string query =
                AuditurHelpers.QueryUpdate(oConcepto.ID, new string[] { "Nombre", "Tipo" }, tablename);
            List<SqlCeParameter> lstParameters = new List<SqlCeParameter>();
            lstParameters.Add(new SqlCeParameter("p1", oConcepto.Nombre));
            lstParameters.Add(new SqlCeParameter("p2", oConcepto.Tipo.ToString()));
            AccesoDatos.ExecuteNonQuery(query, CommandType.Text, lstParameters, conn);
        }

        public override List<Concepto> GetAll()
        {
            List<Concepto> listadoConceptos = base.GetAll();
            listadoConceptos.OrderBy(x => x.Nombre);
            return listadoConceptos;
        }

        private static Concepto DataReaderAObjeto(SqlCeDataReader rdrLector)
        {
            Concepto oConcepto = new Concepto();
            oConcepto.ID = rdrLector.GetInt64(rdrLector.GetOrdinal("ID"));
            oConcepto.Nombre = rdrLector.GetString(rdrLector.GetOrdinal("Nombre"));
            oConcepto.Tipo = rdrLector.GetString(rdrLector.GetOrdinal("Tipo"))[0];
            return oConcepto;
        }
    }
}
