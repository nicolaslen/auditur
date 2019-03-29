using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helpers;

namespace Auditur.Negocio.Reportes
{
    public class SituacionBOs : IReport<SituacionBO>
    {
        public List<SituacionBO> Generar(Semana oSemana)
        {
            List<SituacionBO> lstSituacionBO = new List<SituacionBO>();

            List<BO_Ticket> lstTickets = 
                oSemana.TicketsBO
                    .Where(bo => (!oSemana.TicketsBSP.Any(bsp => bo.Billete == bsp.NroDocumento && 
                                bo.Compania.Codigo == bsp.Compania.Codigo))
                                ||
                                (oSemana.TicketsBSP.Any(bsp => bo.Billete == bsp.NroDocumento &&
                                bo.Compania.Codigo == bsp.Compania.Codigo &&
                                bo.Tarifa != bsp.TarContado + bsp.TarCredito &&
                                ((bo.TarContado != 0 && bsp.TarContado == 0) ||
                                (bo.TarCredito != 0 && bsp.TarCredito == 0))
                                ))).OrderBy(x => x.Compania.Codigo).ThenBy(x => x.Billete).ToList();

            foreach (BO_Ticket oBO_Ticket in lstTickets)
            {
                SituacionBO oSituacionBO = new SituacionBO();

                oSituacionBO.Tarifa = oBO_Ticket.Tarifa;
                oSituacionBO.Impuestos = oBO_Ticket.Impuestos;
                oSituacionBO.IVA = oBO_Ticket.IVA105;
                oSituacionBO.Over = oBO_Ticket.ComOver;
                oSituacionBO.Comision = oBO_Ticket.Comision;
                oSituacionBO.Importe = oBO_Ticket.Total;

                if (oSituacionBO.Tarifa != 0 || oSituacionBO.Impuestos != 0 || oSituacionBO.IVA != 0 || oSituacionBO.Over != 0 || oSituacionBO.Comision != 0 || oSituacionBO.Importe != 0)
                {
                    oSituacionBO.BoletoNro = oBO_Ticket.Billete.ToString();
                    oSituacionBO.Tr = oBO_Ticket.Compania != null ? oBO_Ticket.Compania.Codigo : "";
                    oSituacionBO.Contado = oBO_Ticket.TarContado;
                    oSituacionBO.Credito = oBO_Ticket.TarCredito;
                    oSituacionBO.Moneda = oBO_Ticket.Moneda == Moneda.Peso ? "$" : "D";
                    oSituacionBO.Fecha = AuditurHelpers.GetDateTimeString(oBO_Ticket.Fecha);
                    oSituacionBO.Observaciones = "No figura en su BSP";
                    oSituacionBO.Factura = oBO_Ticket.Factura;
                    oSituacionBO.Pasajero = oBO_Ticket.Pasajero;
                    oSituacionBO.Operacion = oBO_Ticket.Expediente;

                    lstSituacionBO.Add(oSituacionBO);
                }
            }

            return lstSituacionBO;
        }
    }
}
