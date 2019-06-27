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
                lstReembolsosPesos.Add(new Reembolso { Cia = "TOTAL", NetoAPagar = lstReembolsosPesos.Sum(x => x.NetoAPagar) });
                lstReembolsos.AddRange(lstReembolsosPesos);
            }

            List<Reembolso> lstReembolsosDolares = new List<Reembolso>();
            lstTickets.Where(x => x.Moneda == Moneda.Dolar).ToList().ForEach(x => lstReembolsosDolares.Add(GetReembolso(x)));
            if (lstReembolsosDolares.Count > 0)
            {
                lstReembolsosDolares.Add(new Reembolso { Cia = "TOTAL", NetoAPagar = lstReembolsosDolares.Sum(x => x.NetoAPagar) });
                lstReembolsos.AddRange(lstReembolsosDolares);
            }

            return lstReembolsos;
        }

        private Reembolso GetReembolso(BSP_Ticket oBSP_Ticket)
        {
            Reembolso oReembolso = new Reembolso();

            oReembolso.Cia = oBSP_Ticket.Compania.Codigo;
            oReembolso.Tipo = oBSP_Ticket.Trnc;
            oReembolso.RTDN = (oBSP_Ticket.Trnc == "+RTDN" ? oBSP_Ticket.NroDocumento : (oBSP_Ticket.Detalle.Where(x => x.Trnc == "+RTDN").Select(x => x.NroDocumento).FirstOrDefault())).ToString();
            oReembolso.BoletoNro = oBSP_Ticket.NroDocumento.ToString();
            oReembolso.FechaEmision = AuditurHelpers.GetDateTimeString(oBSP_Ticket.FechaEmision);
            oReembolso.Moneda = oBSP_Ticket.Moneda == Moneda.Peso ? "$" : "D";
            oReembolso.FopCA = (oBSP_Ticket.Fop == "CA" ? oBSP_Ticket.ValorTransaccion : 0) + oBSP_Ticket.Detalle.Where(x => x.Fop == "CA").Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
            oReembolso.FopCC = (oBSP_Ticket.Fop == "CC" ? oBSP_Ticket.ValorTransaccion : 0) + oBSP_Ticket.Detalle.Where(x => x.Fop == "CC").Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
            oReembolso.TotalTransaccion = (oBSP_Ticket.Fop == "CC" || oBSP_Ticket.Fop == "CA" ? oBSP_Ticket.ValorTransaccion : 0) + oBSP_Ticket.Detalle.Where(x => x.Fop == "CC" || x.Fop == "CA").Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
            oReembolso.ValorTarifa = oBSP_Ticket.ValorTarifa;
            
            var tycCodigos = new[]
            {
                "YQ", "YR"
            };
            oReembolso.Imp = (oBSP_Ticket.ImpuestoCodigo != "DL" && !tycCodigos.Contains(oBSP_Ticket.ImpuestoCodigo) ? oBSP_Ticket.ImpuestoValor : 0) 
                + oBSP_Ticket.Detalle.Where(x => x.ImpuestoCodigo != "DL" && !tycCodigos.Contains(oBSP_Ticket.ImpuestoCodigo)).Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
            oReembolso.TyC = (tycCodigos.Contains(oBSP_Ticket.ImpuestoCodigo) ? oBSP_Ticket.ImpuestoValor : 0) +
                             oBSP_Ticket.Detalle.Where(x => tycCodigos.Contains(x.ImpuestoCodigo))
                                 .Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
            oReembolso.IVATarifa = (oBSP_Ticket.ImpuestoCodigo == "DL" ? oBSP_Ticket.ImpuestoValor : 0)
                + oBSP_Ticket.Detalle.Where(x => x.ImpuestoCodigo == "DL").Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
            oReembolso.Penalidad = oBSP_Ticket.ImpuestoPenValor;
            oReembolso.ComStdValor = oBSP_Ticket.ComisionStdValor;
            oReembolso.ComSuppValor = oBSP_Ticket.ComisionSuppValor;
            oReembolso.IVASinComision = oBSP_Ticket.ImpuestoSinComision;
            oReembolso.NetoAPagar = oBSP_Ticket.NetoAPagar;

            return oReembolso;
        }
    }
}
