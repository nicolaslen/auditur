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

            List<BSP_Ticket> lstTickets = oSemana.TicketsBSP.Where(x => x.Concepto.Tipo.Equals('R')).OrderBy(x => x.Compania.Codigo).ThenBy(x => x.Moneda).ThenBy(x => x.Billete).ToList();

            List<Reembolso> lstReembolsosPesos = new List<Reembolso>();
            lstTickets.Where(x => x.Moneda == Moneda.Peso).ToList().ForEach(x => lstReembolsosPesos.Add(GetReembolso(x)));
            if (lstReembolsosPesos.Count > 0)
            {
                lstReembolsosPesos.Add(new Reembolso { BoletoNro = "TOTAL", Tarifa = lstReembolsosPesos.Sum(x => x.Tarifa), Contado = lstReembolsosPesos.Sum(x => x.Contado), Credito = lstReembolsosPesos.Sum(x => x.Credito), ImpContado = lstReembolsosPesos.Sum(x => x.ImpContado), ImpCredito = lstReembolsosPesos.Sum(x => x.ImpCredito), CC = lstReembolsosPesos.Sum(x => x.CC), Importe = lstReembolsosPesos.Sum(x => x.Importe), Comision = lstReembolsosPesos.Sum(x => x.Comision), Moneda = "$", IVA = lstReembolsosPesos.Sum(x => x.IVA) });
                lstReembolsos.AddRange(lstReembolsosPesos);
            }

            List<Reembolso> lstReembolsosDolares = new List<Reembolso>();
            lstTickets.Where(x => x.Moneda == Moneda.Dolar).ToList().ForEach(x => lstReembolsosDolares.Add(GetReembolso(x)));
            if (lstReembolsosDolares.Count > 0)
            {
                lstReembolsosDolares.Add(new Reembolso { BoletoNro = "TOTAL", Tarifa = lstReembolsosDolares.Sum(x => x.Tarifa), Contado = lstReembolsosDolares.Sum(x => x.Contado), Credito = lstReembolsosDolares.Sum(x => x.Credito), ImpContado = lstReembolsosDolares.Sum(x => x.ImpContado), ImpCredito = lstReembolsosDolares.Sum(x => x.ImpCredito), CC = lstReembolsosDolares.Sum(x => x.CC), Importe = lstReembolsosDolares.Sum(x => x.Importe), Comision = lstReembolsosDolares.Sum(x => x.Comision), Moneda = "D", IVA = lstReembolsosDolares.Sum(x => x.IVA) });
                lstReembolsos.AddRange(lstReembolsosDolares);
            }

            return lstReembolsos;
        }

        private Reembolso GetReembolso(BSP_Ticket oBSP_Ticket)
        {
            Reembolso oReembolso = new Reembolso();
            oReembolso.BoletoNro = oBSP_Ticket.Detalle.Find(x => x.Observaciones.Substring(0, 2) == "RF").Observaciones.Substring(5, 10);
            oReembolso.Rg = oBSP_Ticket.Rg == BSP_Rg.Internacional ? "IR" : "CR";
            oReembolso.Tr = oBSP_Ticket.Compania.Codigo;
            oReembolso.Fecha = AuditurHelpers.GetDateTimeString(oBSP_Ticket.FechaVenta);
            oReembolso.Tarifa = (oBSP_Ticket.TarContado + oBSP_Ticket.TarCredito);
            oReembolso.Contado = oBSP_Ticket.TarContado;
            oReembolso.Credito = oBSP_Ticket.TarCredito;
            oReembolso.ImpContado = oBSP_Ticket.Detalle.Where(x => !x.ISO.Equals("CP")).Sum(x => x.ImpContado);
            oReembolso.ImpCredito = oBSP_Ticket.Detalle.Where(x => !x.ISO.Equals("CP")).Sum(x => x.ImpCredito);
            oReembolso.IVA = oBSP_Ticket.IVA105;
            oReembolso.CC = oBSP_Ticket.Detalle.Where(x => x.ISO.Equals("CP")).Sum(x => x.ImpContado + x.ImpCredito + x.IVA21);
            oReembolso.Comision = (oBSP_Ticket.ComValor + oBSP_Ticket.ComIVA);
            oReembolso.Importe = oBSP_Ticket.Total;
            oReembolso.Moneda = oBSP_Ticket.Moneda == Moneda.Peso ? "$" : "D";
            oReembolso.ReembolsoNro = oBSP_Ticket.Billete.ToString();
            return oReembolso;
        }
    }
}
