using Helpers;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Auditur.Negocio.Reportes
{
    public class SituacionBSPs : IReport<SituacionBSP>
    {
        public List<SituacionBSP> Generar(Semana oSemana)
        {
            List<SituacionBSP> lstSituacionBSP = new List<SituacionBSP>();

            List<BSP_Ticket> lstTickets = oSemana.TicketsBSP.Where(bspT => bspT.Concepto.Nombre == "ISSUES" && bspT.Trnc == "TKTT" &&
                    !oSemana.TicketsBO.Any(boT => boT.Billete == bspT.NroDocumento &&
                        boT.Compania.Codigo == bspT.Compania.Codigo))
                .OrderBy(x => x.Compania.Codigo).ThenBy(x => x.NroDocumento)
                .ToList();

            foreach (BSP_Ticket oBSP_Ticket in lstTickets)
            {
                SituacionBSP oSituacionBSP = new SituacionBSP();

                oSituacionBSP.Cia = oBSP_Ticket.Compania.Codigo;
                oSituacionBSP.Tipo = oBSP_Ticket.Trnc;
                oSituacionBSP.BoletoNro = oBSP_Ticket.NroDocumento.ToString();
                oSituacionBSP.Ref = oBSP_Ticket.Detalle.Where(x => x.Trnc == "+RTDN").Select(x => x.NroDocumento.ToString()).FirstOrDefault();
                oSituacionBSP.FechaEmision = AuditurHelpers.GetDateTimeString(oBSP_Ticket.FechaEmision);
                oSituacionBSP.Moneda = oBSP_Ticket.Moneda == Moneda.Peso ? "$" : "D";
                oSituacionBSP.TourCode = oBSP_Ticket.Tour;
                oSituacionBSP.CodNr = oBSP_Ticket.Nr;
                oSituacionBSP.Stat = oBSP_Ticket.Rg == BSP_Rg.Doméstico ? "D" : "I";
                oSituacionBSP.FopCA = (oBSP_Ticket.Fop == "CA" ? oBSP_Ticket.ValorTransaccion : 0) + oBSP_Ticket.Detalle.Where(x => x.Fop == "CA").Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
                oSituacionBSP.FopCC = (oBSP_Ticket.Fop == "CC" ? oBSP_Ticket.ValorTransaccion : 0) + oBSP_Ticket.Detalle.Where(x => x.Fop == "CC").Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
                oSituacionBSP.TotalTransaccion = oBSP_Ticket.ValorTransaccion + oBSP_Ticket.Detalle.Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
                oSituacionBSP.ValorTarifa = oBSP_Ticket.ValorTarifa + oBSP_Ticket.Detalle.Select(x => x.ValorTarifa).DefaultIfEmpty(0).Sum();
                oSituacionBSP.Imp = oBSP_Ticket.ImpuestoValor + oBSP_Ticket.Detalle.Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
                oSituacionBSP.TyC = oBSP_Ticket.ImpuestoTyCValor + oBSP_Ticket.Detalle.Select(x => x.ImpuestoTyCValor).DefaultIfEmpty(0).Sum();
                oSituacionBSP.IVATarifa = (oBSP_Ticket.ImpuestoCodigo == "DL" ? oBSP_Ticket.ImpuestoValor : 0) +
                                      oBSP_Ticket.Detalle.Where(x => x.ImpuestoCodigo == "DL")
                                          .Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
                oSituacionBSP.Penalidad = oBSP_Ticket.ImpuestoPenValor + oBSP_Ticket.Detalle.Select(x => x.ImpuestoPenValor).DefaultIfEmpty(0).Sum();
                oSituacionBSP.Cobl = oBSP_Ticket.ImpuestoCobl + oBSP_Ticket.Detalle.Select(x => x.ImpuestoCobl).DefaultIfEmpty(0).Sum();
                oSituacionBSP.ComStdValor = oBSP_Ticket.ComisionStdValor + oBSP_Ticket.Detalle.Select(x => x.ComisionStdValor).DefaultIfEmpty(0).Sum();
                oSituacionBSP.ComSuppValor = oBSP_Ticket.ComisionSuppValor + oBSP_Ticket.Detalle.Select(x => x.ComisionSuppValor).DefaultIfEmpty(0).Sum();
                oSituacionBSP.IVASinComision = oBSP_Ticket.ImpuestoSinComision + oBSP_Ticket.Detalle.Select(x => x.ImpuestoSinComision).DefaultIfEmpty(0).Sum();
                oSituacionBSP.NetoAPagar = oBSP_Ticket.NetoAPagar + oBSP_Ticket.Detalle.Select(x => x.NetoAPagar).DefaultIfEmpty(0).Sum();
                oSituacionBSP.Observaciones = "No figura en su BO";

                lstSituacionBSP.Add(oSituacionBSP);
            }

            return lstSituacionBSP;
        }
    }
}