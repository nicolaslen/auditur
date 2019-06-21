﻿using Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Auditur.Negocio.Reportes
{
    public class EmisionesMasBackOffices : IReport<EmisionesMasBackOffice>
    {
        public List<EmisionesMasBackOffice> Generar(Semana oSemana)
        {
            List<EmisionesMasBackOffice> lstBSPNroOP = new List<EmisionesMasBackOffice>();

            List<BSP_Ticket> lstTickets = oSemana.TicketsBSP.Where(x => x.Concepto.Nombre == "ISSUES" && (x.Trnc == "TKTT" || x.Trnc == "CANX" || x.Trnc == "EMDA" || x.Trnc == "CANN")).OrderBy(x => x.Compania.Codigo).ThenBy(x => x.NroDocumento).ToList();

            foreach (BSP_Ticket oBSP_Ticket in lstTickets)
            {
                BO_Ticket bo_ticket = oSemana.TicketsBO.Find(x => x.Billete == oBSP_Ticket.NroDocumento);

                EmisionesMasBackOffice oBspMasBackOffice = new EmisionesMasBackOffice();

                oBspMasBackOffice.Cia = oBSP_Ticket.Compania.Codigo;
                oBspMasBackOffice.Tipo = oBSP_Ticket.Trnc;
                oBspMasBackOffice.BoletoNro = Validators.ConcatNumbers(oBSP_Ticket.NroDocumento.ToString(), oBSP_Ticket.Detalle.Where(x => x.Trnc == "+TKTT").Select(x => x.NroDocumento.ToString()).ToList());
                oBspMasBackOffice.RTDN = Validators.ConcatNumbers(oBSP_Ticket.Detalle.Where(x => x.Trnc == "+RTDN:").Select(x => x.NroDocumento.ToString()).FirstOrDefault(), oBSP_Ticket.Detalle.Where(x => x.Trnc == "+RTDN:").Select(x => x.NroDocumento.ToString()).Skip(1).ToList());
                if (oBSP_Ticket.Detalle.Any(x => x.Trnc == "+RTDN:" && x.Fop == "EX"))
                    oBspMasBackOffice.RTDN += " (EX)";
                oBspMasBackOffice.FechaEmision = AuditurHelpers.GetDateTimeString(oBSP_Ticket.FechaEmision);
                oBspMasBackOffice.Moneda = oBSP_Ticket.Moneda == Moneda.Peso ? "$" : "D";
                oBspMasBackOffice.TourCode = oBSP_Ticket.Tour;
                oBspMasBackOffice.CodNr = oBSP_Ticket.Nr;
                oBspMasBackOffice.Stat = oBSP_Ticket.Rg == BSP_Rg.Doméstico ? "D" : "I";
                oBspMasBackOffice.FopCA = (oBSP_Ticket.Fop == "CA" ? oBSP_Ticket.ValorTransaccion : 0) + oBSP_Ticket.Detalle.Where(x => x.Fop == "CA").Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
                oBspMasBackOffice.FopCC = (oBSP_Ticket.Fop == "CC" ? oBSP_Ticket.ValorTransaccion : 0) + oBSP_Ticket.Detalle.Where(x => x.Fop == "CC").Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
                oBspMasBackOffice.TotalTransaccion = (oBSP_Ticket.Fop == "CC" || oBSP_Ticket.Fop == "CA" ? oBSP_Ticket.ValorTransaccion : 0) + oBSP_Ticket.Detalle.Where(x => x.Fop == "CC" || x.Fop == "CA").Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
                oBspMasBackOffice.ValorTarifa = oBSP_Ticket.ValorTarifa + oBSP_Ticket.Detalle.Select(x => x.ValorTarifa).DefaultIfEmpty(0).Sum();
                oBspMasBackOffice.Imp = oBSP_Ticket.ImpuestoValor + oBSP_Ticket.Detalle.Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
                oBspMasBackOffice.TyC = oBSP_Ticket.ImpuestoTyCValor + oBSP_Ticket.Detalle.Select(x => x.ImpuestoTyCValor).DefaultIfEmpty(0).Sum();
                oBspMasBackOffice.IVATarifa = (oBSP_Ticket.ImpuestoCodigo == "DL" ? oBSP_Ticket.ImpuestoValor : 0) +
                                      oBSP_Ticket.Detalle.Where(x => x.ImpuestoCodigo == "DL")
                                          .Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
                oBspMasBackOffice.Penalidad = oBSP_Ticket.ImpuestoPenValor + oBSP_Ticket.Detalle.Select(x => x.ImpuestoPenValor).DefaultIfEmpty(0).Sum();
                oBspMasBackOffice.Cobl = oBSP_Ticket.ImpuestoCobl + oBSP_Ticket.Detalle.Select(x => x.ImpuestoCobl).DefaultIfEmpty(0).Sum();
                oBspMasBackOffice.ComStdValor = oBSP_Ticket.ComisionStdValor + oBSP_Ticket.Detalle.Select(x => x.ComisionStdValor).DefaultIfEmpty(0).Sum();
                oBspMasBackOffice.ComSuppValor = oBSP_Ticket.ComisionSuppValor + oBSP_Ticket.Detalle.Select(x => x.ComisionSuppValor).DefaultIfEmpty(0).Sum();
                oBspMasBackOffice.IVASinComision = oBSP_Ticket.ImpuestoSinComision + oBSP_Ticket.Detalle.Select(x => x.ImpuestoSinComision).DefaultIfEmpty(0).Sum();
                oBspMasBackOffice.NetoAPagar = oBSP_Ticket.NetoAPagar + oBSP_Ticket.Detalle.Select(x => x.NetoAPagar).DefaultIfEmpty(0).Sum();

                if (bo_ticket != null)
                {
                    oBspMasBackOffice.Operacion = bo_ticket.OperacionNro;
                    oBspMasBackOffice.Factura = bo_ticket.FacturaNro;
                    oBspMasBackOffice.Pasajero = bo_ticket.Pax;
                }

                lstBSPNroOP.Add(oBspMasBackOffice);
            }
            return lstBSPNroOP;
        }

        
    }
}