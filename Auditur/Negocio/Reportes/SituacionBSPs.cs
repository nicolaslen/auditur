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

                oSituacionBSP.Tarifa = (oBSP_Ticket.TarContado + oBSP_Ticket.TarCredito);
                oSituacionBSP.Impuestos = (oBSP_Ticket.Detalle.Sum(x => x.ImpContado + x.ImpCredito) + oBSP_Ticket.IVA105);
                oSituacionBSP.Comision = (oBSP_Ticket.ComValor + oBSP_Ticket.ComIVA);
                oSituacionBSP.Importe = oBSP_Ticket.Total;

                if (oSituacionBSP.Tarifa != 0 || oSituacionBSP.Impuestos != 0 || oSituacionBSP.Comision != 0 || oSituacionBSP.Importe != 0)
                { 
                    oSituacionBSP.Contado = oBSP_Ticket.TarContado;
                    oSituacionBSP.Credito = oBSP_Ticket.TarCredito;
                    oSituacionBSP.BoletoNro = oBSP_Ticket.NroDocumento.ToString();
                    oSituacionBSP.Rg = oBSP_Ticket.Rg == BSP_Rg.Doméstico ? "CT" : "IT";
                    oSituacionBSP.Tr = oBSP_Ticket.Compania.Codigo;
                    oSituacionBSP.Moneda = oBSP_Ticket.Moneda == Moneda.Peso ? "$" : "D";
                    oSituacionBSP.Fecha = AuditurHelpers.GetDateTimeString(oBSP_Ticket.FechaEmision);
                    oSituacionBSP.Observaciones = "No figura en su BO";

                    lstSituacionBSP.Add(oSituacionBSP);
                }
            }

            return lstSituacionBSP;
        }
    }
}
