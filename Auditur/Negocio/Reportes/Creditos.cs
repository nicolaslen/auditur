using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helpers;

namespace Auditur.Negocio.Reportes
{
    public class Creditos : IReport<CreditoObj>
    {
        public List<CreditoObj> Generar(Semana oSemana)
        {
            List<CreditoObj> lstCredito = new List<CreditoObj>();

            List<BSP_Ticket> lstTickets = oSemana.TicketsBSP.Where(x => x.Concepto.Nombre == "CREDIT MEMOS").OrderBy(x => x.Compania.Codigo).ThenBy(x => x.Moneda).ThenBy(x => x.NroDocumento).ToList();

            List<CreditoObj> lstCreditoPesos = new List<CreditoObj>();
            lstTickets.Where(x => x.Moneda == Moneda.Peso).ToList().ForEach(x => lstCreditoPesos.Add(GetCredito(x)));
            if (lstCreditoPesos.Count > 0)
            {
                lstCreditoPesos.Add(new CreditoObj { Cia = "TOTAL", NetoAPagar = lstCreditoPesos.Sum(x => x.NetoAPagar) });
                lstCredito.AddRange(lstCreditoPesos);
            }

            List<CreditoObj> lstCreditoDolares = new List<CreditoObj>();
            lstTickets.Where(x => x.Moneda == Moneda.Dolar).ToList().ForEach(x => lstCreditoDolares.Add(GetCredito(x)));
            if (lstCreditoDolares.Count > 0)
            {
                lstCreditoDolares.Add(new CreditoObj { Cia = "TOTAL", NetoAPagar = lstCreditoDolares.Sum(x => x.NetoAPagar) });
                lstCredito.AddRange(lstCreditoDolares);
            }

            return lstCredito;
        }

        private CreditoObj GetCredito(BSP_Ticket oBSP_Ticket)
        {
            CreditoObj oCredito = new CreditoObj();

            oCredito.Cia = oBSP_Ticket.Compania.Codigo;
            oCredito.Tipo = oBSP_Ticket.Trnc;

            oCredito.RTDN = Validators.ConcatNumbers(oBSP_Ticket.Detalle.Where(x => x.Trnc == "+RTDN:").Select(x => x.NroDocumento.ToString()).FirstOrDefault(), oBSP_Ticket.Detalle.Where(x => x.Trnc == "+RTDN:").Select(x => x.NroDocumento.ToString()).Skip(1).ToList());
            if (oBSP_Ticket.Detalle.Any(x => x.Trnc == "+RTDN:" && x.Fop == "EX"))
                oCredito.RTDN += " (EX)";

            oCredito.NroDocumento = oBSP_Ticket.NroDocumento.ToString();
            oCredito.FechaEmision = AuditurHelpers.GetDateTimeString(oBSP_Ticket.FechaEmision);
            oCredito.Moneda = oBSP_Ticket.Moneda == Moneda.Peso ? "$" : "D";
            oCredito.Stat = oBSP_Ticket.Rg == BSP_Rg.Doméstico ? "D" : "I";
            oCredito.FopCA = (oBSP_Ticket.Fop == "CA" ? oBSP_Ticket.ValorTransaccion : 0);
            oCredito.FopCC = (oBSP_Ticket.Fop == "CC" ? oBSP_Ticket.ValorTransaccion : 0);
            oCredito.TotalTransaccion =
                (oBSP_Ticket.Fop == "CC" || oBSP_Ticket.Fop == "CA" ? oBSP_Ticket.ValorTransaccion : 0);
            oCredito.ValorTarifa = oBSP_Ticket.ValorTarifa;
            oCredito.Imp = oBSP_Ticket.ImpuestoValor;
            oCredito.TyC = oBSP_Ticket.ImpuestoTyCValor;
            oCredito.IVATarifa = oBSP_Ticket.ImpuestoCodigo == "DL" ? oBSP_Ticket.ImpuestoValor : 0;
            oCredito.Penalidad = oBSP_Ticket.ImpuestoPenValor;
            oCredito.Cobl = oBSP_Ticket.ImpuestoCobl;
            oCredito.ComStdValor = oBSP_Ticket.ComisionStdValor;
            oCredito.ComSuppValor = oBSP_Ticket.ComisionSuppValor;
            oCredito.IVASinComision = oBSP_Ticket.ImpuestoSinComision;
            oCredito.NetoAPagar = oBSP_Ticket.NetoAPagar;
            return oCredito;
        }
    }
}
