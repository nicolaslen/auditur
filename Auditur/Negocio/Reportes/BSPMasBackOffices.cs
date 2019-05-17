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

            List<BSP_Ticket> lstTickets = oSemana.TicketsBSP.Where(x => x.Concepto.Nombre == "ISSUES" && (x.Trnc == "TKTT" || x.Trnc == "CANX" || x.Trnc == "EMDA")).OrderBy(x => x.Compania.Codigo).ThenBy(x => x.NroDocumento).ToList();

            foreach (BSP_Ticket oBSP_Ticket in lstTickets)
            {
                BO_Ticket bo_ticket = oSemana.TicketsBO.Find(x => x.Billete == oBSP_Ticket.NroDocumento);

                BSPMasBackOffice oBspMasBackOffice = new BSPMasBackOffice();

                oBspMasBackOffice.Cia = oBSP_Ticket.Compania.Codigo;
                oBspMasBackOffice.Tipo = oBSP_Ticket.Trnc;
                oBspMasBackOffice.RTDN = ConcatNumbers(oBSP_Ticket.Detalle.Where(x => x.Trnc == "+RTDN:").Select(x => x.NroDocumento.ToString()).FirstOrDefault(), oBSP_Ticket.Detalle.Where(x => x.Trnc == "+RTDN:").Select(x => x.NroDocumento.ToString()).Skip(1).ToList());
                if (oBSP_Ticket.Detalle.Any(x => x.Trnc == "+RTDN:" && x.Fop == "EX"))
                    oBspMasBackOffice.RTDN += " (EX)";
                
                oBspMasBackOffice.BoletoNro = ConcatNumbers(oBSP_Ticket.NroDocumento.ToString(), oBSP_Ticket.Detalle.Where(x => x.Trnc == "+TKTT").Select(x => x.NroDocumento.ToString()).ToList());
                oBspMasBackOffice.FechaEmision = AuditurHelpers.GetDateTimeString(oBSP_Ticket.FechaEmision);
                oBspMasBackOffice.Moneda = oBSP_Ticket.Moneda == Moneda.Peso ? "$" : "D";
                oBspMasBackOffice.TourCode = oBSP_Ticket.Tour;
                oBspMasBackOffice.CodNr = oBSP_Ticket.Nr;
                oBspMasBackOffice.Stat = oBSP_Ticket.Rg == BSP_Rg.Doméstico ? "D" : "I";
                oBspMasBackOffice.FopCA = (oBSP_Ticket.Fop == "CA" ? oBSP_Ticket.ValorTransaccion : 0) + oBSP_Ticket.Detalle.Where(x => x.Fop == "CA").Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
                oBspMasBackOffice.FopCC = (oBSP_Ticket.Fop == "CC" ? oBSP_Ticket.ValorTransaccion : 0) + oBSP_Ticket.Detalle.Where(x => x.Fop == "CC").Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
                oBspMasBackOffice.TotalTransaccion = (oBSP_Ticket.Fop == "CC" || oBSP_Ticket.Fop == "CA" ? oBSP_Ticket.ValorTransaccion : 0) + oBSP_Ticket.Detalle.Where(x => x.Fop == "CC" || x.Fop == "CA").Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
                oBspMasBackOffice.ValorTarifa = oBSP_Ticket.ValorTarifa + oBSP_Ticket.Detalle.Select(x => x.ValorTarifa).DefaultIfEmpty(0).Sum();
                oBspMasBackOffice.Imp = oBSP_Ticket.ImpuestoValor + oBSP_Ticket.Detalle.Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
                oBspMasBackOffice.TyC = oBSP_Ticket.ImpuestoTyCValor + oBSP_Ticket.Detalle.Select(x => x.ImpuestoTyCValor).DefaultIfEmpty(0).Sum();
                oBspMasBackOffice.IVATarifa = (oBSP_Ticket.ImpuestoCodigo == "DL" ? oBSP_Ticket.ImpuestoValor : 0) +
                                      oBSP_Ticket.Detalle.Where(x => x.ImpuestoCodigo == "DL")
                                          .Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
                oBspMasBackOffice.Penalidad = oBSP_Ticket.ImpuestoPenValor + oBSP_Ticket.Detalle.Select(x => x.ImpuestoPenValor).DefaultIfEmpty(0).Sum();
                oBspMasBackOffice.Cobl = oBSP_Ticket.ImpuestoCobl + oBSP_Ticket.Detalle.Select(x => x.ImpuestoCobl).DefaultIfEmpty(0).Sum();
                oBspMasBackOffice.ComStdValor = oBSP_Ticket.ComisionStdValor + oBSP_Ticket.Detalle.Select(x => x.ComisionStdValor).DefaultIfEmpty(0).Sum();
                oBspMasBackOffice.ComSuppValor = oBSP_Ticket.ComisionSuppValor + oBSP_Ticket.Detalle.Select(x => x.ComisionSuppValor).DefaultIfEmpty(0).Sum();
                oBspMasBackOffice.IVASinComision = oBSP_Ticket.ImpuestoSinComision + oBSP_Ticket.Detalle.Select(x => x.ImpuestoSinComision).DefaultIfEmpty(0).Sum();
                oBspMasBackOffice.NetoAPagar = oBSP_Ticket.NetoAPagar + oBSP_Ticket.Detalle.Select(x => x.NetoAPagar).DefaultIfEmpty(0).Sum();

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

        private static string ConcatNumbers(string initValue, List<string> nextValues)
        {
            if (!string.IsNullOrWhiteSpace(initValue) && nextValues.Any())
            {
                var currentValue = initValue;
                foreach (var value in nextValues)
                {
                    int i = 1;
                    while (currentValue[currentValue.Length - i] == '9')
                    {
                        i++;
                    }

                    initValue += "/" + value.Substring(value.Length - i, i);
                    currentValue = value;
                }
            }

            return initValue;
        }
    }
}