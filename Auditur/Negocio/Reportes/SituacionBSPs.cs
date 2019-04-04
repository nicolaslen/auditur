using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helpers;
using System.Data;

namespace Auditur.Negocio.Reportes
{
    public class SituacionBSPs : IReport<SituacionBSP>
    {
        public List<SituacionBSP> Generar(Semana oSemana)
        {
            List<SituacionBSP> lstSituacionBSP = new List<SituacionBSP>();

            List<BSP_Ticket> lstTickets = oSemana.TicketsBSP
                .Where(bspT => bspT.Concepto.Tipo == 'B' &&
                    bspT.Tipo != "VVVV" &&
                    !oSemana.TicketsBO.Any(boT => boT.Billete == bspT.NroDocumento &&
                        boT.Compania.Codigo == bspT.Compania.Codigo))
                .OrderBy(x => x.Compania.Codigo).ThenBy(x => x.NroDocumento)
                .ToList();

            foreach (BSP_Ticket oBSP_Ticket in lstTickets)
            {
                SituacionBSP oSituacionBSP = new SituacionBSP();

                /*oSituacionBSP.Cia = oBSP_Ticket.Compania.ID.ToString();
                oSituacionBSP.Tipo = oBSP_Ticket.Trnc;
                oSituacionBSP.Ref = oBSP_Ticket.Detalle.Where(x => x.Trnc == "+RTDN").Select(x => x.NroDocumento.ToString()).FirstOrDefault();
                oSituacionBSP.NroDocumento = oBSP_Ticket.NroDocumento;
                oSituacionBSP.FechaEmision = AuditurHelpers.GetDateTimeString(oBSP_Ticket.FechaEmision);
                oSituacionBSP.Moneda = oBSP_Ticket.Moneda == Moneda.Peso ? "$" : "D";
                oSituacionBSP.TourCode = oBSP_Ticket.Tour;
                oSituacionBSP.CodNr = oBSP_Ticket.Nr;
                oSituacionBSP.Stat = oBSP_Ticket.Rg == BSP_Rg.Doméstico ? "D" : "I";
                oSituacionBSP.FopCA = oBSP_Ticket.Fop == "CA" ? "X" : "";
                oSituacionBSP.FopCC = oBSP_Ticket.Fop == "CC" ? "X" : "";
                oSituacionBSP.TotalTransaccion = oBSP_Ticket.ValorTransaccion;
                oSituacionBSP.ValorTarifa = oBSP_Ticket.ValorTarifa;
                oSituacionBSP.Imp = oBSP_Ticket.ImpuestoValor;
                oSituacionBSP.TyC = oBSP_Ticket.ImpuestoTyCValor;
                oSituacionBSP.IVATarifa = (oBSP_Ticket.ImpuestoCodigo == "DL" ? oBSP_Ticket.ImpuestoValor : 0) +
                                oBSP_Ticket.Detalle.Where(x => x.ImpuestoCodigo == "DL")
                                .Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
                oSituacionBSP.Penalidad = oBSP_Ticket.ImpuestoPenValor;
                oSituacionBSP.Cobl = oBSP_Ticket.ImpuestoCobl;
                oSituacionBSP.ComStdValor = oBSP_Ticket.ComisionStdValor;
                oSituacionBSP.ComSuppValor = oBSP_Ticket.ComisionSuppValor;
                oSituacionBSP.IVASinComision = oBSP_Ticket.ImpuestoSinComision;
                oSituacionBSP.NetoAPagar = oBSP_Ticket.NetoAPagar;
                */

                oSituacionBSP.Tarifa = (oBSP_Ticket.TarContado + oBSP_Ticket.TarCredito);
                oSituacionBSP.Impuestos = (oBSP_Ticket.Detalle.Sum(x => x.ImpContado + x.ImpCredito) + oBSP_Ticket.IVA105);
                oSituacionBSP.Comision = (oBSP_Ticket.ComValor + oBSP_Ticket.ComIVA);
                oSituacionBSP.Importe = oBSP_Ticket.Total;

                if (oSituacionBSP.Tarifa != 0 || oSituacionBSP.Impuestos != 0 || oSituacionBSP.Comision != 0 || oSituacionBSP.Importe != 0)
                {
                    oSituacionBSP.Contado = oBSP_Ticket.TarContado;
                    oSituacionBSP.Credito = oBSP_Ticket.TarCredito;
                    oSituacionBSP.NroDocumento = oBSP_Ticket.NroDocumento.ToString();
                    oSituacionBSP.Rg = oBSP_Ticket.Rg == BSP_Rg.Doméstico ? "CT" : "IT";
                    oSituacionBSP.Tr = oBSP_Ticket.Compania.Codigo;
                    oSituacionBSP.Moneda = oBSP_Ticket.Moneda == Moneda.Peso ? "$" : "D";
                    oSituacionBSP.FechaEmision = AuditurHelpers.GetDateTimeString(oBSP_Ticket.FechaEmision);
                    oSituacionBSP.Observaciones = "No figura en su BO";

                    lstSituacionBSP.Add(oSituacionBSP);
                }
            }

            return lstSituacionBSP;
        }
    }
}
