using Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Auditur.Negocio.Reportes
{
    public class Emisiones : IReport<Emision>
    {
        public List<Emision> Generar(Semana oSemana)
        {
            List<Emision> lstEmisiones = new List<Emision>();

            List<BSP_Ticket> lstTickets = oSemana.TicketsBSP.Where(x => x.Concepto.Nombre == "ISSUES").OrderBy(x => x.Compania.Codigo).ThenBy(x => x.NroDocumento).ToList();

            foreach (BSP_Ticket oBSP_Ticket in lstTickets)
            {
                BO_Ticket bo_ticket = oSemana.TicketsBO.Find(x => x.Billete == oBSP_Ticket.NroDocumento);

                Emision oEmision = new Emision();

                oEmision.Cia = oBSP_Ticket.Compania.Codigo;
                oEmision.Tipo = oBSP_Ticket.Trnc;
                oEmision.BoletoNro = Validators.ConcatNumbers(oBSP_Ticket.NroDocumento.ToString(), oBSP_Ticket.Detalle.Where(x => x.Trnc == "+TKTT").Select(x => x.NroDocumento.ToString()).ToList());
                oEmision.RTDN = Validators.ConcatNumbers(oBSP_Ticket.Detalle.Where(x => x.Trnc == "+RTDN:").Select(x => x.NroDocumento.ToString()).FirstOrDefault(), oBSP_Ticket.Detalle.Where(x => x.Trnc == "+RTDN:").Select(x => x.NroDocumento.ToString()).Skip(1).ToList());
                if (oBSP_Ticket.Detalle.Any(x => x.Trnc == "+RTDN:" && x.Fop == "EX"))
                    oEmision.RTDN += " (EX)";
                oEmision.FechaEmision = AuditurHelpers.GetDateTimeString(oBSP_Ticket.FechaEmision);
                oEmision.Moneda = oBSP_Ticket.Moneda == Moneda.Peso ? "$" : "D";
                oEmision.Stat = oBSP_Ticket.Rg == BSP_Rg.Doméstico ? "D" : "I";
                oEmision.TourCode = oBSP_Ticket.Tour;
                oEmision.CodNr = oBSP_Ticket.Nr;
                oEmision.FopCA = (oBSP_Ticket.Fop == "CA" ? oBSP_Ticket.ValorTransaccion : 0) + oBSP_Ticket.Detalle.Where(x => x.Fop == "CA").Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
                oEmision.FopCC = (oBSP_Ticket.Fop == "CC" ? oBSP_Ticket.ValorTransaccion : 0) + oBSP_Ticket.Detalle.Where(x => x.Fop == "CC").Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
                oEmision.TotalTransaccion = (oBSP_Ticket.Fop == "CC" || oBSP_Ticket.Fop == "CA" ? oBSP_Ticket.ValorTransaccion : 0) + oBSP_Ticket.Detalle.Where(x => x.Fop == "CC" || x.Fop == "CA").Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
                oEmision.ValorTarifa = oBSP_Ticket.ValorTarifa + oBSP_Ticket.Detalle.Select(x => x.ValorTarifa).DefaultIfEmpty(0).Sum();
                oEmision.Imp = (oBSP_Ticket.ImpuestoCodigo != "O5" ? oBSP_Ticket.ImpuestoValor : 0) + oBSP_Ticket.Detalle.Where(x => x.ImpuestoCodigo != "O5").Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
                oEmision.Imp30 = (oBSP_Ticket.ImpuestoCodigo == "O5" ? oBSP_Ticket.ImpuestoValor : 0) + oBSP_Ticket.Detalle.Where(x => x.ImpuestoCodigo == "O5").Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
                oEmision.TyC = oBSP_Ticket.ImpuestoTyCValor + oBSP_Ticket.Detalle.Select(x => x.ImpuestoTyCValor).DefaultIfEmpty(0).Sum();
                oEmision.IVATarifa = (oBSP_Ticket.ImpuestoCodigo == "DL" ? oBSP_Ticket.ImpuestoValor : 0) +
                                      oBSP_Ticket.Detalle.Where(x => x.ImpuestoCodigo == "DL")
                                          .Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
                oEmision.Penalidad = oBSP_Ticket.ImpuestoPenValor + oBSP_Ticket.Detalle.Select(x => x.ImpuestoPenValor).DefaultIfEmpty(0).Sum();
                oEmision.ComStdValor = oBSP_Ticket.ComisionStdValor + oBSP_Ticket.Detalle.Select(x => x.ComisionStdValor).DefaultIfEmpty(0).Sum();
                oEmision.ComSuppValor = oBSP_Ticket.ComisionSuppValor + oBSP_Ticket.Detalle.Select(x => x.ComisionSuppValor).DefaultIfEmpty(0).Sum();
                oEmision.IVASinComision = oBSP_Ticket.ImpuestoSinComision + oBSP_Ticket.Detalle.Select(x => x.ImpuestoSinComision).DefaultIfEmpty(0).Sum();
                oEmision.NetoAPagar = oBSP_Ticket.NetoAPagar + oBSP_Ticket.Detalle.Select(x => x.NetoAPagar).DefaultIfEmpty(0).Sum();

                if (bo_ticket != null)
                {
                    oEmision.Operacion = bo_ticket.OperacionNro;
                    oEmision.Factura = bo_ticket.FacturaNro;
                    oEmision.Pasajero = bo_ticket.Pax;
                }

                lstEmisiones.Add(oEmision);
            }
            return lstEmisiones;
        }

        
    }
}