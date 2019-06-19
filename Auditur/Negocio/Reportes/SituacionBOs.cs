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

            //TODO: Qué tickets van en Situacion BO?
            List<BO_Ticket> lstTickets = 
                oSemana.TicketsBO
                    /*.Where(bo => (!oSemana.TicketsBSP.Any(bsp => bo.Billete == bsp.NroDocumento && 
                                bo.Compania.Codigo == bsp.Compania.Codigo))
                                ||
                                (oSemana.TicketsBSP.Any(bsp => bo.Billete == bsp.NroDocumento &&
                                bo.Compania.Codigo == bsp.Compania.Codigo &&
                                bo.CA != bsp.TarContado + bsp.TarCredito &&
                                ((bo.TotalTransaccion != 0 && bsp.TarContado == 0) ||
                                (bo.ValorTarifa != 0 && bsp.TarCredito == 0))
                                ))).OrderBy(x => x.Compania.Codigo).ThenBy(x => x.Billete)*/
                                .ToList();
            
            foreach (BO_Ticket oBO_Ticket in lstTickets)
            {
                SituacionBO oSituacionBO = new SituacionBO();
                //TODO: Están bien completados los campos?
                oSituacionBO.Cia = oBO_Ticket.Compania.Codigo;
                oSituacionBO.BoletoNro = oBO_Ticket.Billete.ToString();
                oSituacionBO.FechaEmision = AuditurHelpers.GetDateTimeString(oBO_Ticket.Fecha);
                oSituacionBO.Moneda = oBO_Ticket.Moneda == Moneda.Peso ? "$" : "D";
                oSituacionBO.FopCA = oBO_Ticket.CA;
                oSituacionBO.FopCC = oBO_Ticket.CC;
                oSituacionBO.TotalTransaccion = oBO_Ticket.TotalTransaccion;
                oSituacionBO.ValorTarifa = oBO_Ticket.ValorTarifa;
                oSituacionBO.Imp = oBO_Ticket.Impuestos;
                oSituacionBO.TyC = oBO_Ticket.TasasCargos;
                oSituacionBO.IVATarifa = oBO_Ticket.IVATarifa;
                oSituacionBO.ComStdValor = oBO_Ticket.ComStd;
                oSituacionBO.ComSuppValor = oBO_Ticket.ComSupl;
                oSituacionBO.IVAComision = oBO_Ticket.IVACom;;
                oSituacionBO.NetoAPagar = oBO_Ticket.Neto;;

                //TODO: A cuáles se le agregan estos campos?
                /*if (oSituacionBO.Tarifa != 0 || oSituacionBO.Impuestos != 0 || oSituacionBO.IVA != 0 || oSituacionBO.Over != 0 || oSituacionBO.Comision != 0 || oSituacionBO.Importe != 0)
                {*/
                    oSituacionBO.OperacionNro = oBO_Ticket.OperacionNro;
                    oSituacionBO.Factura = oBO_Ticket.FacturaNro;
                    oSituacionBO.Pasajero = oBO_Ticket.Pax;
                    //oSituacionBO.Observaciones = "No figura en su BSP";

                    lstSituacionBO.Add(oSituacionBO);
                //}
            }

            return lstSituacionBO;
        }
    }
}
