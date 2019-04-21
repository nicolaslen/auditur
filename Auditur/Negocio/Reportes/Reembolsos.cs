using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helpers;

namespace Auditur.Negocio.Reportes
{
    public class Reembolsos : IReport<Reembolso>
    {
        public List<Reembolso> Generar(Semana oSemana)
        {
            List<Reembolso> lstReembolsos = new List<Reembolso>();

            List<BSP_Ticket> lstTickets = oSemana.TicketsBSP.Where(x => x.Concepto.Nombre == "REFUNDS").OrderBy(x => x.Compania.Codigo).ThenBy(x => x.Moneda).ThenBy(x => x.NroDocumento).ToList();

            List<Reembolso> lstReembolsosPesos = new List<Reembolso>();
            lstTickets.Where(x => x.Moneda == Moneda.Peso).ToList().ForEach(x => lstReembolsosPesos.Add(GetReembolso(x)));
            if (lstReembolsosPesos.Count > 0)
            {
                lstReembolsosPesos.Add(new Reembolso { RfndNro = "TOTAL", NetoAPagar = lstReembolsosPesos.Sum(x => x.NetoAPagar) });
                lstReembolsos.AddRange(lstReembolsosPesos);
            }

            List<Reembolso> lstReembolsosDolares = new List<Reembolso>();
            lstTickets.Where(x => x.Moneda == Moneda.Dolar).ToList().ForEach(x => lstReembolsosDolares.Add(GetReembolso(x)));
            if (lstReembolsosDolares.Count > 0)
            {
                lstReembolsosDolares.Add(new Reembolso { RfndNro = "TOTAL", NetoAPagar = lstReembolsosPesos.Sum(x => x.NetoAPagar) });
                lstReembolsos.AddRange(lstReembolsosDolares);
            }

            return lstReembolsos;
        }

        private Reembolso GetReembolso(BSP_Ticket oBSP_Ticket)
        {
            Reembolso oReembolso = new Reembolso();
            oReembolso.Cia = oBSP_Ticket.Compania.ID.ToString();
            oReembolso.RfndNro = oBSP_Ticket.NroDocumento.ToString();
            oReembolso.RtdnNro = oBSP_Ticket.Detalle.Where(x => x.Trnc == "+RTDN").Select(x => x.NroDocumento.ToString())
                .FirstOrDefault();
            oReembolso.Fecha = AuditurHelpers.GetDateTimeString(oBSP_Ticket.FechaEmision);
            oReembolso.Moneda = oBSP_Ticket.Moneda == Moneda.Peso ? "$" : "D";
            oReembolso.FopCA = oBSP_Ticket.Fop == "CA" ? "X" : "";
            oReembolso.FopCC = oBSP_Ticket.Fop == "CC" ? "X" : "";
            oReembolso.TotalTransaccion = oBSP_Ticket.ValorTransaccion + oBSP_Ticket.Detalle.Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
            oReembolso.ValorTarifa = oBSP_Ticket.ValorTarifa + oBSP_Ticket.Detalle.Select(x => x.ValorTarifa).DefaultIfEmpty(0).Sum();
            oReembolso.Imp = oBSP_Ticket.ImpuestoValor + oBSP_Ticket.Detalle.Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
            oReembolso.TyC = oBSP_Ticket.ImpuestoTyCValor + oBSP_Ticket.Detalle.Select(x => x.ImpuestoTyCValor).DefaultIfEmpty(0).Sum();
            oReembolso.IVATarifa = (oBSP_Ticket.ImpuestoCodigo == "DL" ? oBSP_Ticket.ImpuestoValor : 0) + 
                                    oBSP_Ticket.Detalle.Where(x => x.ImpuestoCodigo == "DL").Select(x => x.ImpuestoValor)
                                       .DefaultIfEmpty(0).Sum();
            oReembolso.Penalidad = oBSP_Ticket.ImpuestoPenValor + oBSP_Ticket.Detalle.Select(x => x.ImpuestoPenValor).DefaultIfEmpty(0).Sum();
            oReembolso.ComStdValor = oBSP_Ticket.ComisionStdValor + oBSP_Ticket.Detalle.Select(x => x.ComisionStdValor).DefaultIfEmpty(0).Sum();
            oReembolso.ComSuppValor = oBSP_Ticket.ComisionSuppValor + oBSP_Ticket.Detalle.Select(x => x.ComisionSuppValor).DefaultIfEmpty(0).Sum();
            oReembolso.IVASinComision = oBSP_Ticket.ImpuestoSinComision + oBSP_Ticket.Detalle.Select(x => x.ImpuestoSinComision).DefaultIfEmpty(0).Sum();
            oReembolso.NetoAPagar = oBSP_Ticket.NetoAPagar + oBSP_Ticket.Detalle.Select(x => x.NetoAPagar).DefaultIfEmpty(0).Sum();
            oReembolso.Observaciones = "";
            return oReembolso;
        }
    }
}
