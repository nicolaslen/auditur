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

            List<BSP_Ticket> lstTickets = oSemana.TicketsBSP.Where(x => (x.Concepto.Nombre == "ISSUES" && (x.Trnc == "TKTT" || x.Trnc == "CANX" || x.Trnc == "EMDA" || x.Trnc == "EMDS" || x.Trnc == "CANN" || x.Trnc == "TASF")) || x.Concepto.Nombre == "REFUNDS").OrderBy(x => x.Compania.Codigo).ThenBy(x => x.NroDocumento).ToList();

            foreach (BSP_Ticket oBSP_Ticket in lstTickets)
            {
                //BO_Ticket bo_ticket = oSemana.TicketsBO.Find(x => x.Billete == oBSP_Ticket.NroDocumento);

                Facturacion oFacturacion = new Facturacion();

                oFacturacion.Cia = oBSP_Ticket.Compania.Codigo;
                oFacturacion.BoletoNro = Validators.ConcatNumbers(oBSP_Ticket.NroDocumento.ToString(), oBSP_Ticket.Detalle.Where(x => x.Trnc == "+TKTT").Select(x => x.NroDocumento.ToString()).ToList());
                oFacturacion.Tipo = oBSP_Ticket.Trnc;
                
                oFacturacion.RTDN = Validators.ConcatNumbers(oBSP_Ticket.Detalle.Where(x => x.Trnc == "+RTDN:").Select(x => x.NroDocumento.ToString()).FirstOrDefault(), oBSP_Ticket.Detalle.Where(x => x.Trnc == "+RTDN:").Select(x => x.NroDocumento.ToString()).Skip(1).ToList());
                if (oBSP_Ticket.Detalle.Any(x => x.Trnc == "+RTDN:" && x.Fop == "EX"))
                    oFacturacion.RTDN += " (EX)";
                oFacturacion.FechaEmision = AuditurHelpers.GetDateTimeString(oBSP_Ticket.FechaEmision);
                oFacturacion.Stat = oBSP_Ticket.Rg == BSP_Rg.Doméstico ? "D" : "I";
                oFacturacion.ValorTarifa = oBSP_Ticket.ValorTarifa + oBSP_Ticket.Detalle.Select(x => x.ValorTarifa).DefaultIfEmpty(0).Sum();
                oFacturacion.QN = oFacturacion.Stat == "D" ? oFacturacion.ValorTarifa * (decimal)0.045 : 0;
                oFacturacion.TyC = oBSP_Ticket.ImpuestoTyCValor + oBSP_Ticket.Detalle.Select(x => x.ImpuestoTyCValor).DefaultIfEmpty(0).Sum();
                oFacturacion.IVATarifa = (oBSP_Ticket.ImpuestoCodigo == "DL" ? oBSP_Ticket.ImpuestoValor : 0) +
                                         oBSP_Ticket.Detalle.Where(x => x.ImpuestoCodigo == "DL")
                                             .Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
                oFacturacion.Impuestos = oBSP_Ticket.ImpuestoValor - oFacturacion.QN - oFacturacion.IVATarifa + oFacturacion.TyC;
                oFacturacion.TyC = 0;
                oFacturacion.ComStdValor = oBSP_Ticket.ComisionStdValor + oBSP_Ticket.Detalle.Select(x => x.ComisionStdValor).DefaultIfEmpty(0).Sum();
                oFacturacion.ComSuppValor = oBSP_Ticket.ComisionSuppValor + oBSP_Ticket.Detalle.Select(x => x.ComisionSuppValor).DefaultIfEmpty(0).Sum();
                oFacturacion.IVASinComision = oBSP_Ticket.ImpuestoSinComision + oBSP_Ticket.Detalle.Select(x => x.ImpuestoSinComision).DefaultIfEmpty(0).Sum();
                

                lstFacturacion.Add(oFacturacion);
            }
            return lstFacturacion;
        }
    }
}
