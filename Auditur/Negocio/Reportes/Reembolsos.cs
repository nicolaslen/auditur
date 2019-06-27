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
            lstTickets.Where(x => x.Moneda == Moneda.Peso).ToList().ForEach(x => AddReembolso(lstReembolsosPesos, x));
            if (lstReembolsosPesos.Count > 0)
            {
                lstReembolsosPesos.Add(new Reembolso { Cia = "TOTAL", NetoAPagar = lstReembolsosPesos.Sum(x => x.NetoAPagar) });
                lstReembolsos.AddRange(lstReembolsosPesos);
            }

            List<Reembolso> lstReembolsosDolares = new List<Reembolso>();
            lstTickets.Where(x => x.Moneda == Moneda.Dolar).ToList().ForEach(x => AddReembolso(lstReembolsosDolares, x));
            if (lstReembolsosDolares.Count > 0)
            {
                lstReembolsosDolares.Add(new Reembolso { Cia = "TOTAL", NetoAPagar = lstReembolsosDolares.Sum(x => x.NetoAPagar) });
                lstReembolsos.AddRange(lstReembolsosDolares);
            }

            return lstReembolsos;
        }

        private void AddReembolso(List<Reembolso> reembolsos, BSP_Ticket oBSP_Ticket)
        {
            Reembolso oReembolso = new Reembolso();

            oReembolso.Cia = oBSP_Ticket.Compania.Codigo;
            oReembolso.Tipo = oBSP_Ticket.Trnc;
            oReembolso.BoletoNro = oBSP_Ticket.NroDocumento.ToString();
            oReembolso.FechaEmision = AuditurHelpers.GetDateTimeString(oBSP_Ticket.FechaEmision);
            oReembolso.Moneda = oBSP_Ticket.Moneda == Moneda.Peso ? "$" : "D";
            oReembolso.FopCA = (oBSP_Ticket.Fop == "CA" ? oBSP_Ticket.ValorTransaccion : 0);
            oReembolso.FopCC = (oBSP_Ticket.Fop == "CC" ? oBSP_Ticket.ValorTransaccion : 0);
            oReembolso.TotalTransaccion =
                (oBSP_Ticket.Fop == "CC" || oBSP_Ticket.Fop == "CA" ? oBSP_Ticket.ValorTransaccion : 0);
            oReembolso.ValorTarifa = oBSP_Ticket.ValorTarifa;
            oReembolso.Imp = (oBSP_Ticket.ImpuestoCodigo != "DL" ? oBSP_Ticket.ImpuestoValor : 0);
            oReembolso.TyC = oBSP_Ticket.ImpuestoTyCValor;
            oReembolso.IVATarifa = (oBSP_Ticket.ImpuestoCodigo == "DL" ? oBSP_Ticket.ImpuestoValor : 0);
            oReembolso.Penalidad = oBSP_Ticket.ImpuestoPenValor;
            oReembolso.ComStdValor = oBSP_Ticket.ComisionStdValor;
            oReembolso.ComSuppValor = oBSP_Ticket.ComisionSuppValor;
            oReembolso.IVASinComision = oBSP_Ticket.ImpuestoSinComision;
            oReembolso.NetoAPagar = oBSP_Ticket.NetoAPagar;

            reembolsos.Add(oReembolso);

            foreach (var detalle in oBSP_Ticket.Detalle)
            {
                oReembolso = new Reembolso();

                oReembolso.Cia = "";
                oReembolso.Tipo = detalle.Trnc;
                oReembolso.BoletoNro = detalle.NroDocumento.ToString();
                oReembolso.FechaEmision = AuditurHelpers.GetDateTimeString(detalle.FechaEmision);
                oReembolso.Moneda = "";
                oReembolso.FopCA = (detalle.Fop == "CA" ? detalle.ValorTransaccion : 0);
                oReembolso.FopCC = (detalle.Fop == "CC" ? detalle.ValorTransaccion : 0);
                oReembolso.TotalTransaccion =
                    (detalle.Fop == "CC" || detalle.Fop == "CA" ? detalle.ValorTransaccion : 0);
                oReembolso.ValorTarifa = detalle.ValorTarifa;
                oReembolso.Imp = detalle.ImpuestoValor;
                oReembolso.TyC = detalle.ImpuestoTyCValor;
                oReembolso.IVATarifa = (detalle.ImpuestoCodigo == "DL" ? detalle.ImpuestoValor : 0);
                oReembolso.Penalidad = detalle.ImpuestoPenValor;
                oReembolso.ComStdValor = detalle.ComisionStdValor;
                oReembolso.ComSuppValor = detalle.ComisionSuppValor;
                oReembolso.IVASinComision = detalle.ImpuestoSinComision;
                oReembolso.NetoAPagar = detalle.NetoAPagar;

                reembolsos.Add(oReembolso);
            }
        }
    }
}
