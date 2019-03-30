using Helpers;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;

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
                        "Trnc",
                        "NroDocumento",
                        "FechaEmision",
                        "Cpn",
                        "Nr",
                        "Stat",
                        "Fop",
                        "ValorTransaccion",
                        "ValorTarifa",
                        "ImpuestoValor",
                        "ImpuestoCodigo",
                        "ImpuestoTyCValor",
                        "ImpuestoTyCCodigo",
                        "ImpuestoPenValor",
                        "ImpuestoPenCodigo",
                        "ImpuestoCobl",
                        "ComisionStdPorcentaje",
                        "ComisionStdValor",
                        "ComisionSuppPorcentaje",
                        "ComisionSuppValor",
                        "ImpuestoSinComision",
                        "NetoAPagar",
                        "TicketID" }, tablename);

            List<SqlCeParameter> lstParameters = new List<SqlCeParameter>();
            int p = 1;
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket_Detalle.Trnc));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket_Detalle.NroDocumento));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket_Detalle.FechaEmision));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket_Detalle.Cpn));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket_Detalle.Nr));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket_Detalle.Stat));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket_Detalle.Fop));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket_Detalle.ValorTransaccion));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket_Detalle.ValorTarifa));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket_Detalle.ImpuestoValor));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket_Detalle.ImpuestoCodigo));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket_Detalle.ImpuestoTyCValor));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket_Detalle.ImpuestoTyCCodigo));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket_Detalle.ImpuestoPenValor));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket_Detalle.ImpuestoPenCodigo));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket_Detalle.ImpuestoCobl));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket_Detalle.ComisionStdPorcentaje));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket_Detalle.ComisionStdValor));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket_Detalle.ComisionSuppPorcentaje));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket_Detalle.ComisionSuppValor));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket_Detalle.ImpuestoSinComision));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket_Detalle.NetoAPagar));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket_Detalle.TicketID));

            if (AccesoDatos.ExecuteNonQuery(query, CommandType.Text, lstParameters, conn) > 0)
                oBSP_Ticket_Detalle.ID = GetLastID();
        }

        private static BSP_Ticket_Detalle DataReaderAObjeto(SqlCeDataReader rdrLector)
        {
            BSP_Ticket_Detalle oBSP_Ticket_Detalle = new BSP_Ticket_Detalle();
            oBSP_Ticket_Detalle.ID = rdrLector.GetInt64(rdrLector.GetOrdinal("ID"));
            oBSP_Ticket_Detalle.Trnc = rdrLector.GetString(rdrLector.GetOrdinal("Trnc"));
            oBSP_Ticket_Detalle.NroDocumento = rdrLector.GetInt64(rdrLector.GetOrdinal("NroDocumento"));
            oBSP_Ticket_Detalle.FechaEmision = rdrLector.GetDateTime(rdrLector.GetOrdinal("FechaEmision"));
            oBSP_Ticket_Detalle.Cpn = rdrLector.GetString(rdrLector.GetOrdinal("Cpn"));
            oBSP_Ticket_Detalle.Nr = rdrLector.GetString(rdrLector.GetOrdinal("Nr"));
            oBSP_Ticket_Detalle.Stat = rdrLector.GetString(rdrLector.GetOrdinal("Stat"));
            oBSP_Ticket_Detalle.Fop = rdrLector.GetString(rdrLector.GetOrdinal("Fop"));
            oBSP_Ticket_Detalle.ValorTransaccion = rdrLector.GetDecimal(rdrLector.GetOrdinal("ValorTransaccion"));
            oBSP_Ticket_Detalle.ValorTarifa = rdrLector.GetDecimal(rdrLector.GetOrdinal("ValorTarifa"));
            oBSP_Ticket_Detalle.ImpuestoValor = rdrLector.GetDecimal(rdrLector.GetOrdinal("ImpuestoValor"));
            oBSP_Ticket_Detalle.ImpuestoCodigo = rdrLector.GetString(rdrLector.GetOrdinal("ImpuestoCodigo"));
            oBSP_Ticket_Detalle.ImpuestoTyCValor = rdrLector.GetDecimal(rdrLector.GetOrdinal("ImpuestoTyCValor"));
            oBSP_Ticket_Detalle.ImpuestoTyCCodigo = rdrLector.GetString(rdrLector.GetOrdinal("ImpuestoTyCCodigo"));
            oBSP_Ticket_Detalle.ImpuestoPenValor = rdrLector.GetDecimal(rdrLector.GetOrdinal("ImpuestoPenValor"));
            oBSP_Ticket_Detalle.ImpuestoPenCodigo = rdrLector.GetString(rdrLector.GetOrdinal("ImpuestoPenCodigo"));
            oBSP_Ticket_Detalle.ImpuestoCobl = rdrLector.GetDecimal(rdrLector.GetOrdinal("ImpuestoCobl"));
            oBSP_Ticket_Detalle.ComisionStdPorcentaje = rdrLector.GetDecimal(rdrLector.GetOrdinal("ComisionStdPorcentaje"));
            oBSP_Ticket_Detalle.ComisionStdValor = rdrLector.GetDecimal(rdrLector.GetOrdinal("ComisionStdValor"));
            oBSP_Ticket_Detalle.ComisionSuppPorcentaje = rdrLector.GetDecimal(rdrLector.GetOrdinal("ComisionSuppPorcentaje"));
            oBSP_Ticket_Detalle.ComisionSuppValor = rdrLector.GetDecimal(rdrLector.GetOrdinal("ComisionSuppValor"));
            oBSP_Ticket_Detalle.ImpuestoSinComision = rdrLector.GetDecimal(rdrLector.GetOrdinal("ImpuestoSinComision"));
            oBSP_Ticket_Detalle.NetoAPagar = rdrLector.GetDecimal(rdrLector.GetOrdinal("NetoAPagar"));
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