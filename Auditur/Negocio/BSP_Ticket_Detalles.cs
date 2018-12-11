using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using Helpers;
using System.Data;

namespace Auditur.Negocio
{
    public class BSP_Ticket_Detalles : ABM<BSP_Ticket_Detalle>
    {
        public BSP_Ticket_Detalles() : this(AccesoDatos.OpenConn())
        { }

        public BSP_Ticket_Detalles(SqlCeConnection connection)
            : base(tablename, DataReaderAObjeto, connection)
        { }

        private const string tablename = "BSP_Ticket_Detalles";

        public void Insertar(BSP_Ticket_Detalle oBSP_Ticket_Detalle)
        {
            string query =
                AuditurHelpers.QueryInsert(
                new string[] { 
                    "ISO", 
                    "ImpContado", 
                    "ImpCredito", 
                    "Observaciones",
                    "IVA21",
                    "TicketID" }, tablename);
 
            List<SqlCeParameter> lstParameters = new List<SqlCeParameter>();
            lstParameters.Add(new SqlCeParameter("p1", oBSP_Ticket_Detalle.ISO));
            lstParameters.Add(new SqlCeParameter("p2", oBSP_Ticket_Detalle.ImpContado));
            lstParameters.Add(new SqlCeParameter("p3", oBSP_Ticket_Detalle.ImpCredito));
            lstParameters.Add(new SqlCeParameter("p4", oBSP_Ticket_Detalle.Observaciones));
            lstParameters.Add(new SqlCeParameter("p5", oBSP_Ticket_Detalle.IVA21));
            lstParameters.Add(new SqlCeParameter("p6", oBSP_Ticket_Detalle.TicketID));

            if (AccesoDatos.ExecuteNonQuery(query, CommandType.Text, lstParameters, conn) > 0)
                oBSP_Ticket_Detalle.ID = GetLastID();
        }

        private static BSP_Ticket_Detalle DataReaderAObjeto(SqlCeDataReader rdrLector)
        {
            BSP_Ticket_Detalle oBSP_Ticket_Detalle = new BSP_Ticket_Detalle();
            oBSP_Ticket_Detalle.ID = rdrLector.GetInt64(rdrLector.GetOrdinal("ID"));
            oBSP_Ticket_Detalle.ISO = rdrLector.GetString(rdrLector.GetOrdinal("ISO"));
            oBSP_Ticket_Detalle.ImpContado = rdrLector.GetDecimal(rdrLector.GetOrdinal("ImpContado"));
            oBSP_Ticket_Detalle.ImpCredito = rdrLector.GetDecimal(rdrLector.GetOrdinal("ImpCredito"));
            oBSP_Ticket_Detalle.Observaciones = rdrLector.GetString(rdrLector.GetOrdinal("Observaciones"));
            oBSP_Ticket_Detalle.IVA21 = rdrLector.GetDecimal(rdrLector.GetOrdinal("IVA21"));
            oBSP_Ticket_Detalle.TicketID = rdrLector.GetInt64(rdrLector.GetOrdinal("TicketID"));
            return oBSP_Ticket_Detalle;
        }

        public List<BSP_Ticket_Detalle> ObtenerPorTicket(long TicketID)
        {
            List<BSP_Ticket_Detalle> lstReturn = new List<BSP_Ticket_Detalle>();

            List<SqlCeParameter> lstParameters = new List<SqlCeParameter>();
            lstParameters.Add(new SqlCeParameter("TicketID", TicketID));

            lstReturn = GetByParameters(lstParameters);

            return lstReturn;
        }
    }
}
