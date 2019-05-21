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
                lstCreditoPesos.Add(new CreditoObj { RTDN = "TOTAL", NetoAPagar = lstCreditoPesos.Sum(x => x.NetoAPagar) });
                lstCredito.AddRange(lstCreditoPesos);
            }

            List<CreditoObj> lstCreditoDolares = new List<CreditoObj>();
            lstTickets.Where(x => x.Moneda == Moneda.Dolar).ToList().ForEach(x => lstCreditoDolares.Add(GetCredito(x)));
            if (lstCreditoDolares.Count > 0)
            {
                lstCreditoDolares.Add(new CreditoObj { RTDN = "TOTAL", NetoAPagar = lstCreditoDolares.Sum(x => x.NetoAPagar) });
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

            oCredito.BoletoNro = Validators.ConcatNumbers(oBSP_Ticket.NroDocumento.ToString(), oBSP_Ticket.Detalle.Where(x => x.Trnc == "+TKTT").Select(x => x.NroDocumento.ToString()).ToList());
            oCredito.FechaEmision = AuditurHelpers.GetDateTimeString(oBSP_Ticket.FechaEmision);
            oCredito.Moneda = oBSP_Ticket.Moneda == Moneda.Peso ? "$" : "D";
            oCredito.TourCode = oBSP_Ticket.Tour;
            oCredito.CodNr = oBSP_Ticket.Nr;
            oCredito.Stat = oBSP_Ticket.Rg == BSP_Rg.Doméstico ? "D" : "I";
            oCredito.FopCA = (oBSP_Ticket.Fop == "CA" ? oBSP_Ticket.ValorTransaccion : 0) + oBSP_Ticket.Detalle.Where(x => x.Fop == "CA").Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
            oCredito.FopCC = (oBSP_Ticket.Fop == "CC" ? oBSP_Ticket.ValorTransaccion : 0) + oBSP_Ticket.Detalle.Where(x => x.Fop == "CC").Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
            oCredito.TotalTransaccion = (oBSP_Ticket.Fop == "CC" || oBSP_Ticket.Fop == "CA" ? oBSP_Ticket.ValorTransaccion : 0) + oBSP_Ticket.Detalle.Where(x => x.Fop == "CC" || x.Fop == "CA").Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
            oCredito.ValorTarifa = oBSP_Ticket.ValorTarifa + oBSP_Ticket.Detalle.Select(x => x.ValorTarifa).DefaultIfEmpty(0).Sum();
            oCredito.Imp = oBSP_Ticket.ImpuestoValor + oBSP_Ticket.Detalle.Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
            oCredito.TyC = oBSP_Ticket.ImpuestoTyCValor + oBSP_Ticket.Detalle.Select(x => x.ImpuestoTyCValor).DefaultIfEmpty(0).Sum();
            oCredito.IVATarifa = (oBSP_Ticket.ImpuestoCodigo == "DL" ? oBSP_Ticket.ImpuestoValor : 0) +
                                  oBSP_Ticket.Detalle.Where(x => x.ImpuestoCodigo == "DL")
                                      .Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
            oCredito.Penalidad = oBSP_Ticket.ImpuestoPenValor + oBSP_Ticket.Detalle.Select(x => x.ImpuestoPenValor).DefaultIfEmpty(0).Sum();
            oCredito.Cobl = oBSP_Ticket.ImpuestoCobl + oBSP_Ticket.Detalle.Select(x => x.ImpuestoCobl).DefaultIfEmpty(0).Sum();
            oCredito.ComStdValor = oBSP_Ticket.ComisionStdValor + oBSP_Ticket.Detalle.Select(x => x.ComisionStdValor).DefaultIfEmpty(0).Sum();
            oCredito.ComSuppValor = oBSP_Ticket.ComisionSuppValor + oBSP_Ticket.Detalle.Select(x => x.ComisionSuppValor).DefaultIfEmpty(0).Sum();
            oCredito.IVASinComision = oBSP_Ticket.ImpuestoSinComision + oBSP_Ticket.Detalle.Select(x => x.ImpuestoSinComision).DefaultIfEmpty(0).Sum();
            oCredito.NetoAPagar = oBSP_Ticket.NetoAPagar + oBSP_Ticket.Detalle.Select(x => x.NetoAPagar).DefaultIfEmpty(0).Sum();


            return oCredito;
        }
    }
}
