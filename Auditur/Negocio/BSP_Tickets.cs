using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using Helpers;
using System.Data;
using System.Reflection;
using Auditur.Negocio.Reportes;

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
                    "Tour",
                    "Esac",
                    "Observaciones",
                    "Moneda",
                    "IdConcepto", 
                    "IdCompania",
                    "SemanaID"
                    }, tablename);

            List<SqlCeParameter> lstParameters = new List<SqlCeParameter>();
            int p = 1;
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket.Trnc));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket.NroDocumento));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket.FechaEmision));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket.Cpn));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket.Nr));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket.Stat));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket.Fop));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket.ValorTransaccion));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket.ValorTarifa));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket.ImpuestoValor));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket.ImpuestoCodigo));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket.ImpuestoTyCValor));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket.ImpuestoTyCCodigo));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket.ImpuestoPenValor));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket.ImpuestoPenCodigo));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket.ImpuestoCobl));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket.ComisionStdPorcentaje));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket.ComisionStdValor));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket.ComisionSuppPorcentaje));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket.ComisionSuppValor));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket.ImpuestoSinComision));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket.NetoAPagar));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket.Tour));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket.Esac));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket.Observaciones));
            lstParameters.Add(new SqlCeParameter("p" + p++, (uint)oBSP_Ticket.Moneda));

            if (oBSP_Ticket.Concepto != null)
                lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket.Concepto.ID));
            else
                lstParameters.Add(new SqlCeParameter("p" + p++, null));
            if (oBSP_Ticket.Compania != null)
                lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket.Compania.ID));
            else
                lstParameters.Add(new SqlCeParameter("p" + p++, null));
            lstParameters.Add(new SqlCeParameter("p" + p++, oBSP_Ticket.SemanaID));
            
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

        public int EliminarPorCompania(long CompaniaID)
        {
            int intReturn = 0;
            List<SqlCeParameter> lstParameters = new List<SqlCeParameter>();
            lstParameters.Add(new SqlCeParameter("CompaniaID", CompaniaID));

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
            oBSP_Ticket.Trnc = rdrLector.GetStringOrNull(rdrLector.GetOrdinal("Trnc"));
            oBSP_Ticket.NroDocumento = rdrLector.GetInt64(rdrLector.GetOrdinal("NroDocumento"));
            oBSP_Ticket.FechaEmision = rdrLector.GetDateTimeOrNull(rdrLector.GetOrdinal("FechaEmision"));
            oBSP_Ticket.Cpn = rdrLector.GetStringOrNull(rdrLector.GetOrdinal("Cpn"));
            oBSP_Ticket.Nr = rdrLector.GetStringOrNull(rdrLector.GetOrdinal("Nr"));
            oBSP_Ticket.Stat = rdrLector.GetStringOrNull(rdrLector.GetOrdinal("Stat"));
            oBSP_Ticket.Fop = rdrLector.GetStringOrNull(rdrLector.GetOrdinal("Fop"));
            oBSP_Ticket.ValorTransaccion = rdrLector.GetDecimal(rdrLector.GetOrdinal("ValorTransaccion"));
            oBSP_Ticket.ValorTarifa = rdrLector.GetDecimal(rdrLector.GetOrdinal("ValorTarifa"));
            oBSP_Ticket.ImpuestoValor = rdrLector.GetDecimal(rdrLector.GetOrdinal("ImpuestoValor"));
            oBSP_Ticket.ImpuestoCodigo = rdrLector.GetStringOrNull(rdrLector.GetOrdinal("ImpuestoCodigo"));
            oBSP_Ticket.ImpuestoTyCValor = rdrLector.GetDecimal(rdrLector.GetOrdinal("ImpuestoTyCValor"));
            oBSP_Ticket.ImpuestoTyCCodigo = rdrLector.GetStringOrNull(rdrLector.GetOrdinal("ImpuestoTyCCodigo"));
            oBSP_Ticket.ImpuestoPenValor = rdrLector.GetDecimal(rdrLector.GetOrdinal("ImpuestoPenValor"));
            oBSP_Ticket.ImpuestoPenCodigo = rdrLector.GetStringOrNull(rdrLector.GetOrdinal("ImpuestoPenCodigo"));
            oBSP_Ticket.ImpuestoCobl = rdrLector.GetDecimal(rdrLector.GetOrdinal("ImpuestoCobl"));
            oBSP_Ticket.ComisionStdPorcentaje = rdrLector.GetDecimal(rdrLector.GetOrdinal("ComisionStdPorcentaje"));
            oBSP_Ticket.ComisionStdValor = rdrLector.GetDecimal(rdrLector.GetOrdinal("ComisionStdValor"));
            oBSP_Ticket.ComisionSuppPorcentaje = rdrLector.GetDecimal(rdrLector.GetOrdinal("ComisionSuppPorcentaje"));
            oBSP_Ticket.ComisionSuppValor = rdrLector.GetDecimal(rdrLector.GetOrdinal("ComisionSuppValor"));
            oBSP_Ticket.ImpuestoSinComision = rdrLector.GetDecimal(rdrLector.GetOrdinal("ImpuestoSinComision"));
            oBSP_Ticket.NetoAPagar = rdrLector.GetDecimal(rdrLector.GetOrdinal("NetoAPagar"));
            oBSP_Ticket.Tour = rdrLector.GetStringOrNull(rdrLector.GetOrdinal("Tour"));
            oBSP_Ticket.Esac = rdrLector.GetStringOrNull(rdrLector.GetOrdinal("Esac"));
            oBSP_Ticket.Observaciones = rdrLector.GetStringOrNull(rdrLector.GetOrdinal("Observaciones"));

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
