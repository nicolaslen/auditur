using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helpers;

namespace Auditur.Negocio.Reportes
{
    public class Debitos : IReport<Debito>
    {
        public List<Debito> Generar(Semana oSemana)
        {
            List<Debito> lstDebito = new List<Debito>();
            
            List<BSP_Ticket> lstTickets = oSemana.TicketsBSP.Where(x => x.Concepto.Tipo == 'D').OrderBy(x => x.Compania.Codigo).ThenBy(x => x.Moneda).ThenBy(x => x.NroDocumento).ToList();

            List<Debito> lstDebitoPesos = new List<Debito>();
            lstTickets.Where(x => x.Moneda == Moneda.Peso).ToList().ForEach(x => lstDebitoPesos.Add(GetDebito(x)));
            if (lstDebitoPesos.Count > 0)
            {
                lstDebitoPesos.Add(new Debito { Nro = "TOTAL", Tarifa = lstDebitoPesos.Sum(x => x.Tarifa), Contado = lstDebitoPesos.Sum(x => x.Contado), Credito = lstDebitoPesos.Sum(x => x.Credito), Importe = lstDebitoPesos.Sum(x => x.Importe), Comision = lstDebitoPesos.Sum(x => x.Comision), Moneda = "$", IVA = lstDebitoPesos.Sum(x => x.IVA), Impuestos = lstDebitoPesos.Sum(x => x.Impuestos) });
                lstDebito.AddRange(lstDebitoPesos);
            }

            List<Debito> lstDebitoDolares = new List<Debito>();
            lstTickets.Where(x => x.Moneda == Moneda.Dolar).ToList().ForEach(x => lstDebitoDolares.Add(GetDebito(x)));
            if (lstDebitoDolares.Count > 0)
            {
                lstDebitoDolares.Add(new Debito { Nro = "TOTAL", Tarifa = lstDebitoDolares.Sum(x => x.Tarifa), Contado = lstDebitoDolares.Sum(x => x.Contado), Credito = lstDebitoDolares.Sum(x => x.Credito), Importe = lstDebitoDolares.Sum(x => x.Importe), Comision = lstDebitoDolares.Sum(x => x.Comision), Moneda = "D", IVA = lstDebitoDolares.Sum(x => x.IVA), Impuestos = lstDebitoDolares.Sum(x => x.Impuestos) });
                lstDebito.AddRange(lstDebitoDolares);
            }

            return lstDebito;
        }

        private Debito GetDebito(BSP_Ticket oBSP_Ticket)
        {
            Debito oDebito = new Debito();

            /*oDebito.Nro = oBSP_Ticket.NroDocumento.ToString();
            oDebito.Rg = oBSP_Ticket.Rg == BSP_Rg.Internacional ? "ID" : "CD";
            oDebito.Tr = oBSP_Ticket.Compania.Codigo;
            oDebito.Tarifa = oBSP_Ticket.TarContado + oBSP_Ticket.TarCredito;
            oDebito.Contado = oBSP_Ticket.TarContado;
            oDebito.Credito = oBSP_Ticket.TarCredito;
            oDebito.Impuestos = oBSP_Ticket.Detalle.Sum(x => x.ImpContado + x.ImpCredito);
            oDebito.IVA = oBSP_Ticket.IVA105;
            oDebito.Comision = (oBSP_Ticket.ComValor + oBSP_Ticket.ComOver);
            oDebito.Fecha = AuditurHelpers.GetDateTimeString(oBSP_Ticket.FechaEmision);
            oDebito.Importe = oBSP_Ticket.Total;
            oDebito.Moneda = oBSP_Ticket.Moneda == Moneda.Peso ? "$" : "D";
            oDebito.Observaciones = oBSP_Ticket.Detalle[0].Observaciones.Replace("|", "\n");*/

            return oDebito;
        }
    }
}
