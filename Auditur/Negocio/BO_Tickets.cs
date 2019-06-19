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
            //TODO: Cambiar tabla en DB
            string query =
                AuditurHelpers.QueryInsert(
                new string[] {
                    "IATA",
                    "CIA",
                    "Billete",
                    "Fecha",
                    "Moneda",
                    "CA",
                    "CC",
                    "TotalTransaccion",
                    "ValorTarifa",
                    "Impuestos",
                    "TasasCargos",
                    "IVATarifa",
                    "ComStd",
                    "ComSupl",
                    "IVACom",
                    "Neto",
                    "Pax",
                    "OperacionNro",
                    "FacturaNro",
                    "SemanaID" }, tablename);

            List<SqlCeParameter> lstParameters = new List<SqlCeParameter>();
            lstParameters.AddParameters(oBO_Ticket.IATA,
                oBO_Ticket.Compania?.Codigo,
                oBO_Ticket.Billete,
                oBO_Ticket.Fecha,
                (uint)oBO_Ticket.Moneda,
                oBO_Ticket.CA,
                oBO_Ticket.CC,
                oBO_Ticket.TotalTransaccion,
                oBO_Ticket.ValorTarifa,
                oBO_Ticket.Impuestos,
                oBO_Ticket.TasasCargos,
                oBO_Ticket.IVATarifa,
                oBO_Ticket.ComStd,
                oBO_Ticket.ComSupl,
                oBO_Ticket.IVACom,
                oBO_Ticket.Neto,
                oBO_Ticket.Pax,
                oBO_Ticket.OperacionNro,
                oBO_Ticket.FacturaNro,
                oBO_Ticket.SemanaID);

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

            if (!rdrLector.IsDBNull(rdrLector.GetOrdinal("CIA")))
                oBO_Ticket.Compania = new Compania { Codigo = rdrLector.GetString(rdrLector.GetOrdinal("CIA")) };

            oBO_Ticket.Billete = rdrLector.GetInt64(rdrLector.GetOrdinal("Billete"));
            if (!rdrLector.IsDBNull(rdrLector.GetOrdinal("Fecha")))
                oBO_Ticket.Fecha = rdrLector.GetDateTime(rdrLector.GetOrdinal("Fecha"));
            oBO_Ticket.Moneda = rdrLector.GetBoolean(rdrLector.GetOrdinal("Moneda")) ? Moneda.Dolar : Moneda.Peso;
            oBO_Ticket.CA = rdrLector.GetDecimal(rdrLector.GetOrdinal("CA"));
            oBO_Ticket.CC = rdrLector.GetDecimal(rdrLector.GetOrdinal("CC"));
            oBO_Ticket.TotalTransaccion = rdrLector.GetDecimal(rdrLector.GetOrdinal("TotalTransaccion"));
            oBO_Ticket.ValorTarifa = rdrLector.GetDecimal(rdrLector.GetOrdinal("ValorTarifa"));
            oBO_Ticket.Impuestos = rdrLector.GetDecimal(rdrLector.GetOrdinal("Impuestos"));
            oBO_Ticket.TasasCargos = rdrLector.GetDecimal(rdrLector.GetOrdinal("TasasCargos"));
            oBO_Ticket.IVATarifa = rdrLector.GetDecimal(rdrLector.GetOrdinal("IVATarifa"));
            oBO_Ticket.ComStd = rdrLector.GetDecimal(rdrLector.GetOrdinal("ComStd"));
            oBO_Ticket.ComSupl = rdrLector.GetDecimal(rdrLector.GetOrdinal("ComSupl"));
            oBO_Ticket.IVACom = rdrLector.GetDecimal(rdrLector.GetOrdinal("IVACom"));
            oBO_Ticket.Neto = rdrLector.GetDecimal(rdrLector.GetOrdinal("Neto"));
            oBO_Ticket.Pax = rdrLector.GetString(rdrLector.GetOrdinal("Pax"));
            oBO_Ticket.OperacionNro = rdrLector.GetString(rdrLector.GetOrdinal("OperacionNro"));
            oBO_Ticket.FacturaNro = rdrLector.GetString(rdrLector.GetOrdinal("FacturaNro"));
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

            lstReturn.ForEach(x =>
            {
                Compania oCompania = lstCompanias.Find(y => y.Codigo == x.Compania.Codigo);
                if (oCompania != null)
                    x.Compania = oCompania;
            });

            return lstReturn;
        }
    }
}
