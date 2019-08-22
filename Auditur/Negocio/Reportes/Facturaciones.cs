using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auditur.Negocio.Reportes
{
    public class Facturaciones : IReport<Facturacion>
    {
        public List<Facturacion> Generar(Semana oSemana)
        {
            List<Facturacion> lstFacturacion = new List<Facturacion>();

            List<BSP_Ticket> lstTickets = oSemana.TicketsBSP.Where(x => x.Concepto.Nombre == "ISSUES" || x.Concepto.Nombre == "REFUNDS").OrderBy(x => x.Compania.Codigo).ThenBy(x => x.NroDocumento).ToList();

            foreach (BSP_Ticket oBSP_Ticket in lstTickets)
            {
                Facturacion oFacturacion = new Facturacion();

                oFacturacion.Cia = oBSP_Ticket.Compania.Codigo;
                oFacturacion.BoletoNro = Validators.ConcatNumbers(oBSP_Ticket.NroDocumento.ToString(), oBSP_Ticket.Detalle.Where(x => x.Trnc == "+TKTT").Select(x => x.NroDocumento.ToString()).ToList());
                oFacturacion.Tipo = oBSP_Ticket.Trnc;
                oFacturacion.RTDN = Validators.ConcatNumbers(oBSP_Ticket.Detalle.Where(x => x.Trnc == "+RTDN:").Select(x => x.NroDocumento.ToString()).FirstOrDefault(), oBSP_Ticket.Detalle.Where(x => x.Trnc == "+RTDN:").Select(x => x.NroDocumento.ToString()).Skip(1).ToList());
                if (oBSP_Ticket.Detalle.Any(x => x.Trnc == "+RTDN:" && x.Fop == "EX"))
                    oFacturacion.RTDN += " (EX)";
                oFacturacion.FechaEmision = AuditurHelpers.GetDateTimeString(oBSP_Ticket.FechaEmision);
                
                oFacturacion.ValorTarifa = oBSP_Ticket.ValorTarifa + oBSP_Ticket.Detalle.Select(x => x.ValorTarifa).DefaultIfEmpty(0).Sum();
                oFacturacion.QN = (oBSP_Ticket.ImpuestoCodigo == "QN"
                                      ? oBSP_Ticket.ImpuestoValor
                                      : 0) +
                                  oBSP_Ticket.Detalle
                                      .Where(x => x.ImpuestoCodigo == "QN")
                                      .Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
                oFacturacion.IVATarifa = (oBSP_Ticket.ImpuestoCodigo == "DL" ? oBSP_Ticket.ImpuestoValor : 0) +
                                         oBSP_Ticket.Detalle.Where(x => x.ImpuestoCodigo == "DL")
                                             .Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
                oFacturacion.Penalidad = oBSP_Ticket.ImpuestoPenValor;
                oFacturacion.Impuestos = (oBSP_Ticket.ImpuestoCodigo != "QN" && oBSP_Ticket.ImpuestoCodigo != "DL"
                                             ? oBSP_Ticket.ImpuestoValor
                                             : 0) +
                                         oBSP_Ticket.Detalle
                                             .Where(x => x.ImpuestoCodigo != "QN" && x.ImpuestoCodigo != "DL")
                                             .Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum() +
                                         oBSP_Ticket.ImpuestoTyCValor +
                                         oBSP_Ticket.Detalle.Select(x => x.ImpuestoTyCValor).DefaultIfEmpty(0).Sum();
                oFacturacion.ComStdValor = oBSP_Ticket.ComisionStdValor + oBSP_Ticket.Detalle.Select(x => x.ComisionStdValor).DefaultIfEmpty(0).Sum();
                oFacturacion.ComSuppValor = oBSP_Ticket.ComisionSuppValor + oBSP_Ticket.Detalle.Select(x => x.ComisionSuppValor).DefaultIfEmpty(0).Sum();
                oFacturacion.IVASinComision = oBSP_Ticket.ImpuestoSinComision + oBSP_Ticket.Detalle.Select(x => x.ImpuestoSinComision).DefaultIfEmpty(0).Sum();
                oFacturacion.Stat = oBSP_Ticket.Rg == BSP_Rg.Doméstico ? "D" : "I";
                oFacturacion.CA = (oBSP_Ticket.Fop == "CA" ? oBSP_Ticket.ValorTransaccion : 0) + oBSP_Ticket.Detalle.Where(x => x.Fop == "CA").Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
                oFacturacion.CC = (oBSP_Ticket.Fop == "CC" ? oBSP_Ticket.ValorTransaccion : 0) + oBSP_Ticket.Detalle.Where(x => x.Fop == "CC").Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();

                lstFacturacion.Add(oFacturacion);
            }
            return lstFacturacion;
        }
    }
}
