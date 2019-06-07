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
            List<char> lstTipoConceptoPermitidos = new List<char>();
            lstTipoConceptoPermitidos.Add('B'); //Billetes
            lstTipoConceptoPermitidos.Add('R'); //Reembolsos

            List<BSP_Ticket> lstTickets = oSemana.TicketsBSP.Where(x => lstTipoConceptoPermitidos.Contains(x.Concepto.Tipo)).OrderBy(x => x.Compania.Codigo).ThenBy(x => x.NroDocumento).ToList();

            foreach (BSP_Ticket oBSP_Ticket in lstTickets)
            {
                BO_Ticket bo_ticket = oSemana.TicketsBO.Find(x => x.Billete == oBSP_Ticket.NroDocumento);

                Facturacion oFacturacion = new Facturacion();

                /*oFacturacion.Cia = oBSP_Ticket.Compania.Codigo;
                oFacturacion.Rg = oBSP_Ticket.Rg == BSP_Rg.Doméstico ? "C" : "I";
                oFacturacion.Tipo = (oBSP_Ticket.Concepto.Tipo.Equals('R') ? "R" : (oBSP_Ticket.Tipo.Contains('F') && !oBSP_Ticket.Detalle.Any(x => x.Observaciones.Trim() == "CNJ") ? "B" : "V"));
                oFacturacion.BoletoNro = !oBSP_Ticket.Concepto.Tipo.Equals('R') ? oBSP_Ticket.NroDocumento.ToString() : oBSP_Ticket.Detalle.Find(x => x.Observaciones.Substring(0, 2) == "RF").Observaciones.Substring(5, 10);
                oFacturacion.Moneda = oBSP_Ticket.Moneda == Moneda.Peso ? "$" : "D";
                oFacturacion.FechaEmision = AuditurHelpers.GetDateTimeString(oBSP_Ticket.FechaEmision);
                oFacturacion.Tarifa = oBSP_Ticket.TarContado + oBSP_Ticket.TarCredito;
                oFacturacion.QN = oBSP_Ticket.Detalle.Where(x => x.ISO == "QN").Sum(x => x.ImpContado + x.ImpCredito);
                oFacturacion.Impuestos = oBSP_Ticket.Detalle.Where(x => x.ISO != "QN").Sum(x => x.ImpContado + x.ImpCredito);
                oFacturacion.IVA10 = oBSP_Ticket.IVA105;
                oFacturacion.Gravado = Math.Round((oBSP_Ticket.IVA105 / 10.5M) * 100, 2);
                oFacturacion.Exento = Math.Round(oFacturacion.Tarifa + oFacturacion.QN + oFacturacion.Impuestos - oFacturacion.Gravado, 2);
                oFacturacion.ComNormal = oBSP_Ticket.ComValor;
                oFacturacion.ComOver = oBSP_Ticket.ComOver;
                oFacturacion.IVAComisiones = oBSP_Ticket.ComIVA;
                oFacturacion.TotalFinal = oBSP_Ticket.Total;
                oBSP_Ticket.Detalle.ForEach(x => oFacturacion.Observaciones += x.Observaciones + " ");
                oFacturacion.Observaciones = oFacturacion.Observaciones.Trim();*/

                if (bo_ticket != null)
                {
                    oFacturacion.Operacion = bo_ticket.File;
                    oFacturacion.Factura = bo_ticket.Factura;
                    oFacturacion.Pasajero = bo_ticket.Pasajero;
                }

                lstFacturacion.Add(oFacturacion);
            }
            return lstFacturacion;
        }
    }
}
