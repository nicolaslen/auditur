using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using Helpers;
using System.Data;

namespace Auditur.Negocio
{
    public class BSP_Tickets : ABM<BSP_Ticket>
    {
        public BSP_Tickets() : this(AccesoDatos.OpenConn())
        { }

        public BSP_Tickets(SqlCeConnection connection)
            : base(tablename, DataReaderAObjeto, connection)
        { }

        private const string tablename = "BSP_Tickets";

        public void Insertar(BSP_Ticket oBSP_Ticket)
        {
            string query =
                AuditurHelpers.QueryInsert(
                new string[] { 
                    "Billete", 
                    "Tipo",
                    "FechaVenta", 
                    "TarContado", 
                    "TarCredito", 
                    "IVA105", 
                    "ComPorcentaje", 
                    "ComValor", 
                    "ComOver", 
                    "ComIVA", 
                    "Total", 
                    "Rg", 
                    "Moneda",
                    "IdConcepto", 
                    "IdCompania",
                    "SemanaID"
                    }, tablename);

            List<SqlCeParameter> lstParameters = new List<SqlCeParameter>();
            lstParameters.Add(new SqlCeParameter("p1", oBSP_Ticket.Billete));
            lstParameters.Add(new SqlCeParameter("p2", oBSP_Ticket.Tipo));
            lstParameters.Add(new SqlCeParameter("p3", oBSP_Ticket.FechaEmision));
            lstParameters.Add(new SqlCeParameter("p4", oBSP_Ticket.TarContado));
            lstParameters.Add(new SqlCeParameter("p5", oBSP_Ticket.TarCredito));
            lstParameters.Add(new SqlCeParameter("p6", oBSP_Ticket.IVA105));
            lstParameters.Add(new SqlCeParameter("p7", oBSP_Ticket.ComPorcentaje));
            lstParameters.Add(new SqlCeParameter("p8", oBSP_Ticket.ComValor));
            lstParameters.Add(new SqlCeParameter("p9", oBSP_Ticket.ComOver));
            lstParameters.Add(new SqlCeParameter("p10", oBSP_Ticket.ComIVA));
            lstParameters.Add(new SqlCeParameter("p11", oBSP_Ticket.Total));
            lstParameters.Add(new SqlCeParameter("p12", (uint)oBSP_Ticket.Rg));
            lstParameters.Add(new SqlCeParameter("p13", (uint)oBSP_Ticket.Moneda.Value));

            if (oBSP_Ticket.Concepto != null)
                lstParameters.Add(new SqlCeParameter("p14", oBSP_Ticket.Concepto.ID));
            else
                lstParameters.Add(new SqlCeParameter("p14", null));
            if (oBSP_Ticket.Compania != null)
                lstParameters.Add(new SqlCeParameter("p15", oBSP_Ticket.Compania.ID));
            else
                lstParameters.Add(new SqlCeParameter("p15", null));
            lstParameters.Add(new SqlCeParameter("p16", oBSP_Ticket.SemanaID));
            
            if (AccesoDatos.ExecuteNonQuery(query, CommandType.Text, lstParameters, conn) > 0)
                oBSP_Ticket.ID = GetLastID();
        }

        public int EliminarPorSemana(long SemanaID)
        {
            int intReturn = 0;
            List<SqlCeParameter> lstParameters = new List<SqlCeParameter>();
            lstParameters.Add(new SqlCeParameter("SemanaID", SemanaID));

            intReturn = DeleteByParameters(lstParameters);

            return intReturn;
        }

        public long TicketsCargados(long SemanaID)
        {
            long lngResult = 0;

            List<SqlCeParameter> lstParameters = new List<SqlCeParameter>();
            lstParameters.Add(new SqlCeParameter("SemanaID", SemanaID));

            string query = "SELECT COUNT(1) AS Result FROM " + tablename + " WHERE SemanaID = ?";

            SqlCeDataReader rdrLector = AccesoDatos.ExecuteReader(query, CommandType.Text, lstParameters, conn);

            if (rdrLector != null)
            {
                if (rdrLector.Read())
                {
                    lngResult = rdrLector.GetInt32(0);
                }
                rdrLector.Close();
            }
            return lngResult;
        }

        private static BSP_Ticket DataReaderAObjeto(SqlCeDataReader rdrLector)
        {
            BSP_Ticket oBSP_Ticket = new BSP_Ticket();
            oBSP_Ticket.ID = rdrLector.GetInt64(rdrLector.GetOrdinal("ID"));
            oBSP_Ticket.Billete = rdrLector.GetInt64(rdrLector.GetOrdinal("Billete"));
            oBSP_Ticket.Tipo = rdrLector.GetString(rdrLector.GetOrdinal("Tipo"));
            if (!rdrLector.IsDBNull(rdrLector.GetOrdinal("FechaVenta")))
                oBSP_Ticket.FechaEmision = rdrLector.GetDateTime(rdrLector.GetOrdinal("FechaVenta"));
            oBSP_Ticket.TarContado = rdrLector.GetDecimal(rdrLector.GetOrdinal("TarContado"));
            oBSP_Ticket.TarCredito = rdrLector.GetDecimal(rdrLector.GetOrdinal("TarCredito"));
            oBSP_Ticket.IVA105 = rdrLector.GetDecimal(rdrLector.GetOrdinal("IVA105"));
            oBSP_Ticket.ComPorcentaje = rdrLector.GetDecimal(rdrLector.GetOrdinal("ComPorcentaje"));
            oBSP_Ticket.ComValor = rdrLector.GetDecimal(rdrLector.GetOrdinal("ComValor"));
            oBSP_Ticket.ComOver = rdrLector.GetDecimal(rdrLector.GetOrdinal("ComOver"));
            oBSP_Ticket.ComIVA = rdrLector.GetDecimal(rdrLector.GetOrdinal("ComIVA"));
            oBSP_Ticket.Total = rdrLector.GetDecimal(rdrLector.GetOrdinal("Total"));
            oBSP_Ticket.Rg = rdrLector.GetBoolean(rdrLector.GetOrdinal("Rg")) ? BSP_Rg.Internacional : BSP_Rg.Doméstico;
            oBSP_Ticket.Moneda = rdrLector.GetBoolean(rdrLector.GetOrdinal("Moneda")) ? Moneda.Dolar : Moneda.Peso;

            oBSP_Ticket.Concepto = new Concepto { ID = rdrLector.GetInt64(rdrLector.GetOrdinal("IdConcepto"))};
            oBSP_Ticket.Compania = new Compania { ID = rdrLector.GetInt64(rdrLector.GetOrdinal("IdCompania"))};

            oBSP_Ticket.SemanaID = rdrLector.GetInt64(rdrLector.GetOrdinal("SemanaID"));
            return oBSP_Ticket;
        }

        public List<BSP_Ticket> ObtenerPorSemana(long SemanaID)
        {
            List<BSP_Ticket> lstReturn = new List<BSP_Ticket>();

            List<SqlCeParameter> lstParameters = new List<SqlCeParameter>();
            lstParameters.Add(new SqlCeParameter("SemanaID", SemanaID));

            lstReturn = GetByParameters(lstParameters);

            Conceptos Conceptos = new Conceptos(this.conn);
            List<Concepto> conceptos = Conceptos.GetAll();

            Companias Companias = new Companias(this.conn);
            List<Compania> companias = Companias.GetAll();

            BSP_Ticket_Detalles BSP_Ticket_Detalles = new BSP_Ticket_Detalles(this.conn);

            lstReturn.ForEach(x =>
            {
                x.Concepto = conceptos.Find(y => y.ID == x.Concepto.ID);
                x.Compania = companias.Find(y => y.ID == x.Compania.ID);
                x.Detalle = BSP_Ticket_Detalles.ObtenerPorTicket(x.ID);
            });

            return lstReturn;
        }
    }
}
