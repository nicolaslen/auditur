using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using Helpers;
using System.Data;

namespace Auditur.Negocio
{
    public class BO_Tickets : ABM<BO_Ticket>
    {
        public BO_Tickets() : this(AccesoDatos.OpenConn())
        { }

        public BO_Tickets(SqlCeConnection connection)
            : base(tablename, DataReaderAObjeto, connection)
        { }

        private const string tablename = "BO_Tickets";

        public void Insertar(BO_Ticket oBO_Ticket)
        {
            string query =
                AuditurHelpers.QueryInsert(
                new string[] { 
                    "IATA", 
                    "Billete", 
                    "Fecha", 
                    "Void", 
                    "CIA",
                    "Tarifa", 
                    "TarContado", 
                    "TarCredito", 
                    "Impuestos",
                    "Comision",
                    "ComOver",
                    "Expediente",
                    "IVA10",
                    "IVAComision",
                    "ComValor",
                    "Total",
                    "Factura",
                    "Pasajero",
                    "Vendedor",
                    "Moneda",
                    "SemanaID" }, tablename);

            List<SqlCeParameter> lstParameters = new List<SqlCeParameter>();
            lstParameters.Add(new SqlCeParameter("p1", oBO_Ticket.IATA));
            lstParameters.Add(new SqlCeParameter("p2", oBO_Ticket.Billete));
            lstParameters.Add(new SqlCeParameter("p3", oBO_Ticket.Fecha));
            lstParameters.Add(new SqlCeParameter("p4", oBO_Ticket.Void));
            if (oBO_Ticket.Compania != null)
                lstParameters.Add(new SqlCeParameter("p5", oBO_Ticket.Compania.Codigo));
            else
                lstParameters.Add(new SqlCeParameter("p5", null));
            lstParameters.Add(new SqlCeParameter("p6", oBO_Ticket.Tarifa));
            lstParameters.Add(new SqlCeParameter("p7", oBO_Ticket.TarContado));
            lstParameters.Add(new SqlCeParameter("p8", oBO_Ticket.TarCredito));
            lstParameters.Add(new SqlCeParameter("p9", oBO_Ticket.Impuestos));
            lstParameters.Add(new SqlCeParameter("p10", oBO_Ticket.Comision));
            lstParameters.Add(new SqlCeParameter("p11", oBO_Ticket.ComOver));
            lstParameters.Add(new SqlCeParameter("p12", oBO_Ticket.Expediente));
            lstParameters.Add(new SqlCeParameter("p13", oBO_Ticket.IVA105));
            lstParameters.Add(new SqlCeParameter("p14", oBO_Ticket.IVAComision));
            lstParameters.Add(new SqlCeParameter("p15", oBO_Ticket.ComValor));
            lstParameters.Add(new SqlCeParameter("p16", oBO_Ticket.Total));
            lstParameters.Add(new SqlCeParameter("p17", oBO_Ticket.Factura));
            lstParameters.Add(new SqlCeParameter("p18", oBO_Ticket.Pasajero));
            lstParameters.Add(new SqlCeParameter("p19", oBO_Ticket.Vendedor));
            lstParameters.Add(new SqlCeParameter("p20", (uint)oBO_Ticket.Moneda.Value));
            lstParameters.Add(new SqlCeParameter("p21", oBO_Ticket.SemanaID));
            
            if (AccesoDatos.ExecuteNonQuery(query, CommandType.Text, lstParameters, conn) > 0)
                oBO_Ticket.ID = GetLastID();
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

        private static BO_Ticket DataReaderAObjeto(SqlCeDataReader rdrLector)
        {
            BO_Ticket oBO_Ticket = new BO_Ticket();
            oBO_Ticket.ID = rdrLector.GetInt64(rdrLector.GetOrdinal("ID"));
            oBO_Ticket.IATA = rdrLector.GetInt64(rdrLector.GetOrdinal("IATA"));
            oBO_Ticket.Billete = rdrLector.GetInt64(rdrLector.GetOrdinal("Billete"));
            if (!rdrLector.IsDBNull(rdrLector.GetOrdinal("Fecha")))
                oBO_Ticket.Fecha = rdrLector.GetDateTime(rdrLector.GetOrdinal("Fecha"));
            oBO_Ticket.Void = rdrLector.GetBoolean(rdrLector.GetOrdinal("Void"));
            if (!rdrLector.IsDBNull(rdrLector.GetOrdinal("CIA")))
                oBO_Ticket.Compania = new Compania { Codigo = rdrLector.GetString(rdrLector.GetOrdinal("CIA")) };

            oBO_Ticket.Tarifa = rdrLector.GetDecimal(rdrLector.GetOrdinal("Tarifa"));
            oBO_Ticket.TarContado = rdrLector.GetDecimal(rdrLector.GetOrdinal("TarContado"));
            oBO_Ticket.TarCredito = rdrLector.GetDecimal(rdrLector.GetOrdinal("TarCredito"));
            oBO_Ticket.Impuestos = rdrLector.GetDecimal(rdrLector.GetOrdinal("Impuestos"));
            oBO_Ticket.Comision = rdrLector.GetDecimal(rdrLector.GetOrdinal("Comision"));
            oBO_Ticket.ComOver = rdrLector.GetDecimal(rdrLector.GetOrdinal("ComOver"));
            oBO_Ticket.Expediente = rdrLector.GetString(rdrLector.GetOrdinal("Expediente"));
            oBO_Ticket.IVA105 = rdrLector.GetDecimal(rdrLector.GetOrdinal("IVA10"));
            oBO_Ticket.IVAComision = rdrLector.GetDecimal(rdrLector.GetOrdinal("IVAComision"));
            oBO_Ticket.ComValor = rdrLector.GetDecimal(rdrLector.GetOrdinal("ComValor"));
            oBO_Ticket.Total = rdrLector.GetDecimal(rdrLector.GetOrdinal("Total"));
            oBO_Ticket.Factura = rdrLector.GetString(rdrLector.GetOrdinal("Factura"));
            oBO_Ticket.Pasajero = rdrLector.GetString(rdrLector.GetOrdinal("Pasajero"));
            oBO_Ticket.Vendedor = rdrLector.GetString(rdrLector.GetOrdinal("Vendedor"));
            oBO_Ticket.Moneda = rdrLector.GetBoolean(rdrLector.GetOrdinal("Moneda")) ? Moneda.Dolar : Moneda.Peso;
            oBO_Ticket.SemanaID = rdrLector.GetInt64(rdrLector.GetOrdinal("SemanaID"));
            return oBO_Ticket;
        }


        public List<BO_Ticket> ObtenerPorSemana(long SemanaID)
        {
            List<BO_Ticket> lstReturn = new List<BO_Ticket>();

            List<SqlCeParameter> lstParameters = new List<SqlCeParameter>();
            lstParameters.Add(new SqlCeParameter("SemanaID", SemanaID));

            lstReturn = GetByParameters(lstParameters);
            Companias Companias = new Companias(this.conn);
            List<Compania> lstCompanias = Companias.GetAll();

            lstReturn.ForEach(x => {
                Compania oCompania = lstCompanias.Find(y => y.Codigo == x.Compania.Codigo);
                if (oCompania != null)
                    x.Compania = oCompania;
            });

            return lstReturn;
        }
    }
}
