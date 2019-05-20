using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helpers;

namespace Auditur.Negocio.Reportes
{
    public class Debitos : IReport<Debito>
    {
        public List<Debito> Generar(Semana oSemana)
        {
            List<Debito> lstDebito = new List<Debito>();
            
            List<BSP_Ticket> lstTickets = oSemana.TicketsBSP.Where(x => x.Concepto.Tipo == 'D').OrderBy(x => x.Compania.Codigo).ThenBy(x => x.Moneda).ThenBy(x => x.NroDocumento).ToList();

            List<Debito> lstDebitoPesos = new List<Debito>();
            lstTickets.Where(x => x.Moneda == Moneda.Peso).ToList().ForEach(x => lstDebitoPesos.Add(GetDebito(x)));
            if (lstDebitoPesos.Count > 0)
            {
                lstDebitoPesos.Add(new Debito { RTDN = "TOTAL", NetoAPagar = lstDebitoPesos.Sum(x => x.NetoAPagar) });
                lstDebito.AddRange(lstDebitoPesos);
            }

            List<Debito> lstDebitoDolares = new List<Debito>();
            lstTickets.Where(x => x.Moneda == Moneda.Dolar).ToList().ForEach(x => lstDebitoDolares.Add(GetDebito(x)));
            if (lstDebitoDolares.Count > 0)
            {
                lstDebitoDolares.Add(new Debito { RTDN = "TOTAL", NetoAPagar = lstDebitoDolares.Sum(x => x.NetoAPagar) });
                lstDebito.AddRange(lstDebitoDolares);
            }

            return lstDebito;
        }

        private Debito GetDebito(BSP_Ticket oBSP_Ticket)
        {
            Debito oDebito = new Debito();

            oDebito.Cia = oBSP_Ticket.Compania.Codigo;
            oDebito.Tipo = oBSP_Ticket.Trnc;
            oDebito.RTDN = Validators.ConcatNumbers(oBSP_Ticket.Detalle.Where(x => x.Trnc == "+RTDN:").Select(x => x.NroDocumento.ToString()).FirstOrDefault(), oBSP_Ticket.Detalle.Where(x => x.Trnc == "+RTDN:").Select(x => x.NroDocumento.ToString()).Skip(1).ToList());
            if (oBSP_Ticket.Detalle.Any(x => x.Trnc == "+RTDN:" && x.Fop == "EX"))
                oDebito.RTDN += " (EX)";

            oDebito.BoletoNro = Validators.ConcatNumbers(oBSP_Ticket.NroDocumento.ToString(), oBSP_Ticket.Detalle.Where(x => x.Trnc == "+TKTT").Select(x => x.NroDocumento.ToString()).ToList());
            oDebito.FechaEmision = AuditurHelpers.GetDateTimeString(oBSP_Ticket.FechaEmision);
            oDebito.Moneda = oBSP_Ticket.Moneda == Moneda.Peso ? "$" : "D";
            oDebito.TourCode = oBSP_Ticket.Tour;
            oDebito.CodNr = oBSP_Ticket.Nr;
            oDebito.Stat = oBSP_Ticket.Rg == BSP_Rg.Doméstico ? "D" : "I";
            oDebito.FopCA = (oBSP_Ticket.Fop == "CA" ? oBSP_Ticket.ValorTransaccion : 0) + oBSP_Ticket.Detalle.Where(x => x.Fop == "CA").Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
            oDebito.FopCC = (oBSP_Ticket.Fop == "CC" ? oBSP_Ticket.ValorTransaccion : 0) + oBSP_Ticket.Detalle.Where(x => x.Fop == "CC").Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
            oDebito.TotalTransaccion = (oBSP_Ticket.Fop == "CC" || oBSP_Ticket.Fop == "CA" ? oBSP_Ticket.ValorTransaccion : 0) + oBSP_Ticket.Detalle.Where(x => x.Fop == "CC" || x.Fop == "CA").Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
            oDebito.ValorTarifa = oBSP_Ticket.ValorTarifa + oBSP_Ticket.Detalle.Select(x => x.ValorTarifa).DefaultIfEmpty(0).Sum();
            oDebito.Imp = oBSP_Ticket.ImpuestoValor + oBSP_Ticket.Detalle.Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
            oDebito.TyC = oBSP_Ticket.ImpuestoTyCValor + oBSP_Ticket.Detalle.Select(x => x.ImpuestoTyCValor).DefaultIfEmpty(0).Sum();
            oDebito.IVATarifa = (oBSP_Ticket.ImpuestoCodigo == "DL" ? oBSP_Ticket.ImpuestoValor : 0) +
                                  oBSP_Ticket.Detalle.Where(x => x.ImpuestoCodigo == "DL")
                                      .Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
            oDebito.Penalidad = oBSP_Ticket.ImpuestoPenValor + oBSP_Ticket.Detalle.Select(x => x.ImpuestoPenValor).DefaultIfEmpty(0).Sum();
            oDebito.Cobl = oBSP_Ticket.ImpuestoCobl + oBSP_Ticket.Detalle.Select(x => x.ImpuestoCobl).DefaultIfEmpty(0).Sum();
            oDebito.ComStdValor = oBSP_Ticket.ComisionStdValor + oBSP_Ticket.Detalle.Select(x => x.ComisionStdValor).DefaultIfEmpty(0).Sum();
            oDebito.ComSuppValor = oBSP_Ticket.ComisionSuppValor + oBSP_Ticket.Detalle.Select(x => x.ComisionSuppValor).DefaultIfEmpty(0).Sum();
            oDebito.IVASinComision = oBSP_Ticket.ImpuestoSinComision + oBSP_Ticket.Detalle.Select(x => x.ImpuestoSinComision).DefaultIfEmpty(0).Sum();
            oDebito.NetoAPagar = oBSP_Ticket.NetoAPagar + oBSP_Ticket.Detalle.Select(x => x.NetoAPagar).DefaultIfEmpty(0).Sum();


            return oDebito;
        }
    }
}
