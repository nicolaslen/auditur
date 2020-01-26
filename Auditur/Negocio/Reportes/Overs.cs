using System;
using Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Auditur.Negocio.Reportes
{
    public class Overs : IReport<Over>
    {
        public List<Over> Generar(Semana oSemana)
        {
            List<Over> lstOver = new List<Over>();
            List<Over> lstOverCompania = new List<Over>();

            Companias Companias = new Companias();
            List<Compania> companias = Companias.GetAll();
            Companias.CloseConnection();

            List<string> companiesCodes = new List<string> { "4M", "LA", "QR", "AR", "AC", "AF", "NZ", "EK", "QF", "SA" };

            List<BSP_Ticket> lstTicketsBSP = oSemana.TicketsBSP.Where(x => x.Concepto.Nombre == "ISSUES" && x.Trnc == "TKTT").OrderBy(x => x.Compania.Codigo).ThenBy(x => x.NroDocumento).ToList();
            foreach (Compania compania in companias.OrderBy(x => x.Codigo))
            {
                lstOverCompania = new List<Over>();

                foreach (BSP_Ticket oBSP_Ticket in lstTicketsBSP.Where(x => x.Compania.ID == compania.ID))
                {
                    BO_Ticket oBO_Ticket = oSemana.TicketsBO.Find(x => x.Billete == oBSP_Ticket.NroDocumento && x.Compania.Codigo == oBSP_Ticket.Compania.Codigo);
                    lstOverCompania.Add(GetOver(oBSP_Ticket, oBO_Ticket, companiesCodes));
                }

                foreach (BO_Ticket bo_ticketFaltante in oSemana.TicketsBO.Where(x => x.Compania.ID == compania.ID && !lstTicketsBSP.Any(y => y.NroDocumento == x.Billete && y.Compania.Codigo == compania.Codigo) && x.ComSupl != 0).OrderBy(x => x.Billete))
                {
                    lstOverCompania.Add(GetOver(null, bo_ticketFaltante, companiesCodes));
                }

                lstOverCompania = lstOverCompania.Where(x => x.Diferencias != 0 || (x.OverRec == 0 && companiesCodes.Contains(x.Cia))).OrderBy(x => x.Moneda).ThenByDescending(x => x.Diferencias).ToList();

                if (lstOverCompania.Count > 0)
                {
                    lstOver.AddRange(lstOverCompania);

                    var oOverTotal = new Over();
                    oOverTotal.Cia = "TOTAL";
                    oOverTotal.OverRec = lstOverCompania.Select(x => x.OverRec).Sum();
                    oOverTotal.OverPed = lstOverCompania.Select(x => x.OverPed).Sum();
                    oOverTotal.Diferencias = lstOverCompania.Select(x => x.Diferencias).Sum();
                    lstOver.Add(oOverTotal);
                }
            }


            return lstOver;
        }

        private Over GetOver(BSP_Ticket oBSP_Ticket, BO_Ticket oBO_Ticket, List<string> companiesCodes)
        {
            Over oOver = new Over();
            oOver.Cia = oBSP_Ticket != null ? oBSP_Ticket.Compania.Codigo : oBO_Ticket.Compania.Codigo;
            oOver.BoletoNro = oBSP_Ticket?.NroDocumento.ToString() ?? oBO_Ticket.Billete.ToString();
            oOver.FechaEmision = AuditurHelpers.GetDateTimeString(oBSP_Ticket != null ? oBSP_Ticket.FechaEmision : oBO_Ticket.Fecha);
            if (oBSP_Ticket != null)
                oOver.Moneda = oBSP_Ticket.Moneda == Moneda.Peso ? "$" : "D";
            else
                oOver.Moneda = oBO_Ticket.Moneda != null ? (oBO_Ticket.Moneda == Moneda.Peso ? "$" : "D") : "";
            oOver.TourCode = oBSP_Ticket?.Tour;

            if (oBSP_Ticket != null)
            {
                var totalComisionSuppValor = oBSP_Ticket.ComisionSuppValor +
                                              oBSP_Ticket.Detalle.Select(x => x.ComisionSuppValor).DefaultIfEmpty(0)
                                                  .Sum();
                oOver.OverRec = -totalComisionSuppValor;
            }

            if (oBO_Ticket != null)
            {
                oOver.OverPed = oBO_Ticket.ComSupl;
                oOver.Factura = oBO_Ticket.FacturaNro;
                oOver.Pasajero = oBO_Ticket.Pax;
                oOver.Operacion = oBO_Ticket.OperacionNro;
            }

            oOver.Diferencias = oOver.OverRec + oOver.OverPed;
            oOver.Observaciones = GetObservaciones(oOver, !string.IsNullOrWhiteSpace(oBSP_Ticket?.Tour), companiesCodes);


            
            return oOver;
        }

        private string GetObservaciones(Over oOver, bool hasTour, List<string> companiesCodes)
        {
            if (oOver.OverRec == 0 && oOver.OverPed == 0 && companiesCodes.Contains(oOver.Cia))
                return hasTour ? "ERROR? VER CONTRATO" : "RECLAMAR";


            if (oOver.OverPed > oOver.OverRec)
            {
                if (oOver.OverRec == 0)
                    return "RECLAMAR";
                    
                return "CORREGIR COSTO";
            }

            return "ERROR EMISIÓN/CORREGIR COSTO";
        }
    }
}