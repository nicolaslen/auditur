using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helpers;

namespace Auditur.Negocio.Reportes
{
    public class Creditos : IReport<CreditoObj>
    {
        public List<CreditoObj> Generar(Semana oSemana)
        {
            List<CreditoObj> lstCredito = new List<CreditoObj>();

            List<BSP_Ticket> lstTickets = oSemana.TicketsBSP.Where(x => x.Concepto.Tipo == 'C').OrderBy(x => x.Compania.Codigo).ThenBy(x => x.Moneda).ThenBy(x => x.NroDocumento).ToList();

            List<CreditoObj> lstCreditoPesos = new List<CreditoObj>();
            lstTickets.Where(x => x.Moneda == Moneda.Peso).ToList().ForEach(x => lstCreditoPesos.Add(GetCredito(x)));
            if (lstCreditoPesos.Count > 0)
            {
                lstCreditoPesos.Add(new CreditoObj { Nro = "TOTAL", Tarifa = lstCreditoPesos.Sum(x => x.Tarifa), Contado = lstCreditoPesos.Sum(x => x.Contado), Credito = lstCreditoPesos.Sum(x => x.Credito), Importe = lstCreditoPesos.Sum(x => x.Importe), Comision = lstCreditoPesos.Sum(x => x.Comision), Moneda = "$", IVA = lstCreditoPesos.Sum(x => x.IVA), Impuestos = lstCreditoPesos.Sum(x => x.Impuestos) });
                lstCredito.AddRange(lstCreditoPesos);
            }

            List<CreditoObj> lstCreditoDolares = new List<CreditoObj>();
            lstTickets.Where(x => x.Moneda == Moneda.Dolar).ToList().ForEach(x => lstCreditoDolares.Add(GetCredito(x)));
            if (lstCreditoDolares.Count > 0)
            {
                lstCreditoDolares.Add(new CreditoObj { Nro = "TOTAL", Tarifa = lstCreditoDolares.Sum(x => x.Tarifa), Contado = lstCreditoDolares.Sum(x => x.Contado), Credito = lstCreditoDolares.Sum(x => x.Credito), Importe = lstCreditoDolares.Sum(x => x.Importe), Comision = lstCreditoDolares.Sum(x => x.Comision), Moneda = "D", IVA = lstCreditoDolares.Sum(x => x.IVA), Impuestos = lstCreditoDolares.Sum(x => x.Impuestos) });
                lstCredito.AddRange(lstCreditoDolares);
            }

            return lstCredito;
        }

        private CreditoObj GetCredito(BSP_Ticket oBSP_Ticket)
        {
            CreditoObj oCredito = new CreditoObj();

            oCredito.Nro = oBSP_Ticket.NroDocumento.ToString();
            oCredito.Rg = oBSP_Ticket.Rg == BSP_Rg.Internacional ? "IC" : "CC";
            oCredito.Tr = oBSP_Ticket.Compania.Codigo;
            oCredito.Tarifa = oBSP_Ticket.TarContado + oBSP_Ticket.TarCredito;
            oCredito.Contado = oBSP_Ticket.TarContado;
            oCredito.Credito = oBSP_Ticket.TarCredito;
            oCredito.Impuestos = oBSP_Ticket.Detalle.Sum(x => x.ImpContado + x.ImpCredito);
            oCredito.IVA = oBSP_Ticket.IVA105;
            oCredito.Comision = oBSP_Ticket.ComValor + oBSP_Ticket.ComOver;
            oCredito.Fecha = AuditurHelpers.GetDateTimeString(oBSP_Ticket.FechaEmision);
            oCredito.Importe = oBSP_Ticket.Total;
            oCredito.Moneda = oBSP_Ticket.Moneda == Moneda.Peso ? "$" : "D";
            oCredito.Observaciones = oBSP_Ticket.Detalle[0].Observaciones.Replace("|", "\n");

            return oCredito;
        }
    }
}
