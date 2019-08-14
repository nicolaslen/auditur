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
                    .Where(bo => !oSemana.TicketsBSP.Any(bsp => bo.Billete == bsp.NroDocumento &&
                                bo.Compania.Codigo == bsp.Compania.Codigo))
                                .OrderBy(x => x.Compania.Codigo).ThenBy(x => x.Billete)
                                .ToList();

            foreach (BO_Ticket oBO_Ticket in lstTickets)
            {
                SituacionBO oSituacionBO = new SituacionBO();
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
                oSituacionBO.IVAComision = oBO_Ticket.IVACom;
                oSituacionBO.NetoAPagar = oBO_Ticket.Neto;
                oSituacionBO.OperacionNro = oBO_Ticket.OperacionNro;
                oSituacionBO.Factura = oBO_Ticket.FacturaNro;
                oSituacionBO.Pasajero = oBO_Ticket.Pax;
                oSituacionBO.Observaciones = "No figura en su BSP";

                if (oSituacionBO.FopCA != 0 ||
                   oSituacionBO.FopCC != 0 ||
                   oSituacionBO.TotalTransaccion != 0 ||
                   oSituacionBO.ValorTarifa != 0 ||
                   oSituacionBO.Imp != 0 ||
                   oSituacionBO.TyC != 0 ||
                   oSituacionBO.IVATarifa != 0 ||
                   oSituacionBO.ComStdValor != 0 ||
                   oSituacionBO.ComSuppValor != 0 ||
                   oSituacionBO.IVAComision != 0 ||
                   oSituacionBO.NetoAPagar != 0)
                    lstSituacionBO.Add(oSituacionBO);
            }

            return lstSituacionBO;
        }
    }
}
