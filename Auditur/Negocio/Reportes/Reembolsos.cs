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
                lstReembolsosPesos.Add(new Reembolso { RfndNro = "TOTAL", NetoAPagar = lstReembolsosPesos.Sum(x => x.NetoAPagar), Contado = lstReembolsosPesos.Sum(x => x.Contado), Credito = lstReembolsosPesos.Sum(x => x.Credito), ImpContado = lstReembolsosPesos.Sum(x => x.ImpContado), ImpCredito = lstReembolsosPesos.Sum(x => x.ImpCredito), CC = lstReembolsosPesos.Sum(x => x.CC), Importe = lstReembolsosPesos.Sum(x => x.Importe), Comision = lstReembolsosPesos.Sum(x => x.Comision), Moneda = "$", IVA = lstReembolsosPesos.Sum(x => x.IVA) });
                lstReembolsos.AddRange(lstReembolsosPesos);
            }

            List<Reembolso> lstReembolsosDolares = new List<Reembolso>();
            lstTickets.Where(x => x.Moneda == Moneda.Dolar).ToList().ForEach(x => lstReembolsosDolares.Add(GetReembolso(x)));
            if (lstReembolsosDolares.Count > 0)
            {
                lstReembolsosDolares.Add(new Reembolso { RfndNro = "TOTAL", NetoAPagar = lstReembolsosDolares.Sum(x => x.NetoAPagar), Contado = lstReembolsosDolares.Sum(x => x.Contado), Credito = lstReembolsosDolares.Sum(x => x.Credito), ImpContado = lstReembolsosDolares.Sum(x => x.ImpContado), ImpCredito = lstReembolsosDolares.Sum(x => x.ImpCredito), CC = lstReembolsosDolares.Sum(x => x.CC), Importe = lstReembolsosDolares.Sum(x => x.Importe), Comision = lstReembolsosDolares.Sum(x => x.Comision), Moneda = "D", IVA = lstReembolsosDolares.Sum(x => x.IVA) });
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
            oReembolso.TotalTransaccion = oBSP_Ticket.ValorTransaccion;
            oReembolso.ValorTarifa = oBSP_Ticket.ValorTarifa;
            oReembolso.Imp = oBSP_Ticket.ImpuestoValor;
            oReembolso.TyC = oBSP_Ticket.ImpuestoTyCValor;
            oReembolso.IVATarifa = (oBSP_Ticket.ImpuestoCodigo == "DL" ? oBSP_Ticket.ImpuestoValor : 0) + 
                                    oBSP_Ticket.Detalle.Where(x => x.ImpuestoCodigo == "DL").Select(x => x.ImpuestoValor)
                                       .DefaultIfEmpty(0).Sum();
            oReembolso.Penalidad = oBSP_Ticket.ImpuestoPenValor;
            oReembolso.ComStdValor = oBSP_Ticket.ComisionStdValor;
            oReembolso.ComSuppValor = oBSP_Ticket.ComisionSuppValor;
            oReembolso.IVASinComision = oBSP_Ticket.ImpuestoSinComision;
            oReembolso.NetoAPagar = oBSP_Ticket.NetoAPagar;
            oReembolso.Observaciones = "FALTA";
            return oReembolso;
        }
    }
}
