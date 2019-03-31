using Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Auditur.Negocio.Reportes
{
    public class BSPMasBackOffices : IReport<BSPMasBackOffice>
    {
        public List<BSPMasBackOffice> Generar(Semana oSemana)
        {
            List<BSPMasBackOffice> lstBSPNroOP = new List<BSPMasBackOffice>();
            List<char> lstTipoConceptoPermitidos = new List<char>();
            lstTipoConceptoPermitidos.Add('B'); //Billetes
            lstTipoConceptoPermitidos.Add('R'); //Reembolsos

            List<BSP_Ticket> lstTickets = oSemana.TicketsBSP.Where(x => lstTipoConceptoPermitidos.Contains(x.Concepto.Tipo)).OrderBy(x => x.Compania.Codigo).ThenBy(x => x.NroDocumento).ToList();

            foreach (BSP_Ticket oBSP_Ticket in lstTickets)
            {
                BO_Ticket bo_ticket = oSemana.TicketsBO.Find(x => x.Billete == oBSP_Ticket.NroDocumento);

                BSPMasBackOffice oBspMasBackOffice = new BSPMasBackOffice();

                oBspMasBackOffice.Cia = oBSP_Ticket.Compania.ID.ToString();
                oBspMasBackOffice.Tipo = oBSP_Ticket.Trnc;
                oBspMasBackOffice.Ref = oBSP_Ticket.Detalle.Where(x => x.Trnc == "+RTDN").Select(x => x.NroDocumento.ToString()).FirstOrDefault();
                oBspMasBackOffice.BoletoNro = oBSP_Ticket.NroDocumento.ToString();
                oBspMasBackOffice.FechaEmision = AuditurHelpers.GetDateTimeString(oBSP_Ticket.FechaEmision);
                oBspMasBackOffice.Moneda = oBSP_Ticket.Moneda == Moneda.Peso ? "$" : "D";
                oBspMasBackOffice.TourCode = oBSP_Ticket.Tour;
                oBspMasBackOffice.CodNr = oBSP_Ticket.Nr;
                oBspMasBackOffice.Stat = oBSP_Ticket.Rg == BSP_Rg.Doméstico ? "D" : "I";
                oBspMasBackOffice.FopCA = oBSP_Ticket.Fop == "CA" ? "X" : "";
                oBspMasBackOffice.FopCC = oBSP_Ticket.Fop == "CC" ? "X" : "";
                oBspMasBackOffice.TotalTransac = oBSP_Ticket.ValorTransaccion;
                oBspMasBackOffice.ValorTarifa = oBSP_Ticket.ValorTarifa;
                oBspMasBackOffice.Imp = oBSP_Ticket.ImpuestoValor;
                oBspMasBackOffice.TyC = oBSP_Ticket.ImpuestoTyCValor;
                oBspMasBackOffice.IVATarifa = (oBSP_Ticket.ImpuestoCodigo == "DL" ? oBSP_Ticket.ImpuestoValor : 0) +
                                      oBSP_Ticket.Detalle.Where(x => x.ImpuestoCodigo == "DL")
                                          .Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
                oBspMasBackOffice.Pen = oBSP_Ticket.ImpuestoPenValor;
                oBspMasBackOffice.Cobl = oBSP_Ticket.ImpuestoCobl;
                oBspMasBackOffice.ComStd = oBSP_Ticket.ComisionStdValor;
                oBspMasBackOffice.ComSupl = oBSP_Ticket.ComisionSuppValor;
                oBspMasBackOffice.IVAComisiones = oBSP_Ticket.ImpuestoSinComision;
                oBspMasBackOffice.NetoAPagar = oBSP_Ticket.NetoAPagar;

                if (bo_ticket != null)
                {
                    oBspMasBackOffice.Operacion = bo_ticket.Expediente;
                    oBspMasBackOffice.Factura = bo_ticket.Factura;
                    oBspMasBackOffice.Pasajero = bo_ticket.Pasajero;
                }

                lstBSPNroOP.Add(oBspMasBackOffice);
            }
            return lstBSPNroOP;
        }
    }
}