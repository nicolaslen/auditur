using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using Helpers;
using System.Data;

namespace Auditur.Negocio
{
    public class Semanas : ABM<Semana>
    {
        public Semanas() : this(AccesoDatos.OpenConn())
        { }

        public Semanas(SqlCeConnection connection)
            : base(tablename, DataReaderAObjeto, connection)
        { }

        private const string tablename = "Semanas";

        public void Insertar(Semana oSemana)
        {
            string query =
                AuditurHelpers.QueryInsert(new string[] { "Periodo", "FechaDesde", "FechaHasta", "IdAgencia" }, tablename);

            List<SqlCeParameter> lstParameters = new List<SqlCeParameter>();
            lstParameters.Add(new SqlCeParameter("p1", oSemana.Periodo));
            lstParameters.Add(new SqlCeParameter("p2", oSemana.FechaDesde));
            lstParameters.Add(new SqlCeParameter("p3", oSemana.FechaHasta));
            if (oSemana.Agencia != null)
                lstParameters.Add(new SqlCeParameter("p4", oSemana.Agencia.ID));
            else
                lstParameters.Add(new SqlCeParameter("p4", null));

            AccesoDatos.ExecuteNonQuery(query, CommandType.Text, lstParameters, conn);

            oSemana.ID = GetLastID();
        }

        public Semana GetByAgenciaPeriodo(long idAgencia, DateTime Periodo)
        {
            List<Semana> lstSemanas = null;
            Semana oSemana = null;

            List<SqlCeParameter> lstParameters = new List<SqlCeParameter>();
            lstParameters.Add(new SqlCeParameter("idAgencia", idAgencia));
            lstParameters.Add(new SqlCeParameter("Periodo", Periodo));

            lstSemanas = GetByParameters(lstParameters);
            if (lstSemanas.Count == 1)
            {
                oSemana = lstSemanas[0];
                VerificarTicketsCargados(oSemana);
            }

            return oSemana;
        }

        public void VerificarTicketsCargados(Semana oSemana)
        {
            BSP_Tickets BSP_Tickets = new BSP_Tickets(conn);
            BO_Tickets BO_Tickets = new BO_Tickets(conn);

            oSemana.BOCargado = BO_Tickets.TicketsCargados(oSemana.ID) > 0;
            oSemana.BSPCargado = BSP_Tickets.TicketsCargados(oSemana.ID) > 0;
        }

        public List<Semana> GetByAgenciaAno(Agencia oAgencia, int Ano)
        {
            List<Semana> lstReturn = new List<Semana>();

            List<SqlCeParameter> lstParameters = new List<SqlCeParameter>();
            lstParameters.Add(new SqlCeParameter("DATEPART(YEAR, Periodo)", Ano));
            lstParameters.Add(new SqlCeParameter("idAgencia", oAgencia.ID));

            lstReturn = GetByParameters(lstParameters);

            Agencias Agencias = new Agencias();
            lstReturn.ForEach(x =>
            {
                VerificarTicketsCargados(x); 
                x.Agencia = Agencias.GetByID(x.Agencia.ID);
            });

            return lstReturn;
        }

        public List<int> GetAñosByAgencia(int idAgencia)
        {
            int Año = 0;
            SqlCeDataReader rdrLector;
            List<int> returnList = new List<int>();

            string query = "SELECT DISTINCT DATEPART(YEAR, Periodo) AS Ano FROM " + tablename + " WHERE idAgencia = ?";

            List<SqlCeParameter> lstParameters = new List<SqlCeParameter>();
            lstParameters.Add(new SqlCeParameter("idAgencia", idAgencia));

            rdrLector = AccesoDatos.ExecuteReader(query, CommandType.Text, lstParameters, conn);

            if (rdrLector != null)
            {
                while (rdrLector.Read())
                {
                    Año = rdrLector.GetInt32(rdrLector.GetOrdinal("Ano"));
                    if (Año > 0)
                        returnList.Add(Año);
                }
                rdrLector.Close();
            }

            return returnList;
        }

        private static Semana DataReaderAObjeto(SqlCeDataReader rdrLector)
        {
            Semana Semana = new Semana();
            Semana.ID = rdrLector.GetInt64(rdrLector.GetOrdinal("ID"));
            Semana.Periodo = rdrLector.GetDateTime(rdrLector.GetOrdinal("Periodo"));
            Semana.FechaDesde = rdrLector.GetDateTime(rdrLector.GetOrdinal("FechaDesde"));
            Semana.FechaHasta = rdrLector.GetDateTime(rdrLector.GetOrdinal("FechaHasta"));
            Semana.Agencia = new Agencia { ID = rdrLector.GetInt64(rdrLector.GetOrdinal("IdAgencia")) };

            /*if (!rdrLector.IsDBNull(rdrLector.GetOrdinal("IdAgencia")))
            {
                Agencias Agencias = new Agencias(conn);
                Semana.Agencia = Agencias.GetByID(rdrLector.GetInt64(rdrLector.GetOrdinal("IdAgencia")));
            }*/

            /*BSP_Tickets BSP_Tickets = new BSP_Tickets();
            Semana.TicketsBSP = BSP_Tickets.ObtenerPorSemana(Semana.ID);

            BO_Tickets BO_Tickets = new BO_Tickets();
            Semana.TicketsBO = BO_Tickets.ObtenerPorSemana(Semana.ID);*/
            return Semana;
        }
    }
}
