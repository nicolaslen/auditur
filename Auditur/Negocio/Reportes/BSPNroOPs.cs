using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helpers;

namespace Auditur.Negocio.Reportes
{
    public class BSPNroOPs : IReport<BSPNroOP>
    {
        public List<BSPNroOP> Generar(Semana oSemana)
        {
            List<BSPNroOP> lstBSPNroOP = new List<BSPNroOP>();
            List<char> lstTipoConceptoPermitidos = new List<char>();
            lstTipoConceptoPermitidos.Add('B'); //Billetes
            lstTipoConceptoPermitidos.Add('R'); //Reembolsos

            List<BSP_Ticket> lstTickets = oSemana.TicketsBSP.Where(x => lstTipoConceptoPermitidos.Contains(x.Concepto.Tipo)).OrderBy(x => x.Compania.Codigo).ThenBy(x => x.Billete).ToList();
            
            foreach (BSP_Ticket oBSP_Ticket in lstTickets)
            {
                BO_Ticket bo_ticket = oSemana.TicketsBO.Find(x => x.Billete == oBSP_Ticket.Billete);
                
                BSPNroOP oBSPNroOP = new BSPNroOP();
                
                oBSPNroOP.Cia = oBSP_Ticket.Compania.Codigo;
                oBSPNroOP.Rg = oBSP_Ticket.Rg == BSP_Rg.Doméstico ? "C" : "I";
                oBSPNroOP.Tipo = (oBSP_Ticket.Concepto.Tipo.Equals('R') ? "R" : (oBSP_Ticket.Tipo.Contains('F') && !oBSP_Ticket.Detalle.Any(x => x.Observaciones.Trim() == "CNJ") ? "B" : "V"));
                oBSPNroOP.BoletoNro = !oBSP_Ticket.Concepto.Tipo.Equals('R') ? oBSP_Ticket.Billete.ToString() : oBSP_Ticket.Detalle.Find(x => x.Observaciones.Substring(0, 2) == "RF").Observaciones.Substring(5, 10);
                oBSPNroOP.Moneda = oBSP_Ticket.Moneda == Moneda.Peso ? "$" : "D";
                oBSPNroOP.FechaEmision = AuditurHelpers.GetDateTimeString(oBSP_Ticket.FechaEmision);
                oBSPNroOP.Tarifa = oBSP_Ticket.TarContado + oBSP_Ticket.TarCredito;
                oBSPNroOP.Contado = oBSP_Ticket.TarContado;
                oBSPNroOP.Credito = oBSP_Ticket.TarCredito;
                oBSPNroOP.ImpContado = oBSP_Ticket.Detalle.Sum(x => x.ImpContado);
                oBSPNroOP.ImpCredito = oBSP_Ticket.Detalle.Sum(x => x.ImpCredito);
                oBSPNroOP.IVA10 = oBSP_Ticket.IVA105;
                oBSPNroOP.ComNormal = oBSP_Ticket.ComValor;
                oBSPNroOP.ComOver = oBSP_Ticket.ComOver;
                oBSPNroOP.IVAComisiones = oBSP_Ticket.ComIVA;
                oBSPNroOP.TotalFinal = oBSP_Ticket.Total;

                if (bo_ticket != null)
                {
                    oBSPNroOP.Operacion = bo_ticket.Expediente;
                    oBSPNroOP.Factura = bo_ticket.Factura;
                    oBSPNroOP.Pasajero = bo_ticket.Pasajero;
                }
                
                lstBSPNroOP.Add(oBSPNroOP);
            }
            return lstBSPNroOP;
        }
    }
}
