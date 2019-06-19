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

            List<BSP_Ticket> lstTicketsBSP = oSemana.TicketsBSP.Where(x => x.Concepto.Nombre == "ISSUES" && x.Trnc == "TKTT").OrderBy(x => x.Compania.Codigo).ThenBy(x => x.NroDocumento).ToList();
            foreach (Compania compania in companias.OrderBy(x => x.Codigo))
            {
                lstOverCompania = new List<Over>();

                foreach (BSP_Ticket oBSP_Ticket in lstTicketsBSP.Where(x => x.Compania.ID == compania.ID))
                {
                    BO_Ticket oBO_Ticket = oSemana.TicketsBO.Find(x => x.Billete == oBSP_Ticket.NroDocumento && x.Compania.Codigo == oBSP_Ticket.Compania.Codigo);

                    var totalComisionSuppValor = oBSP_Ticket.ComisionSuppValor +
                                                 oBSP_Ticket.Detalle.Select(x => x.ComisionSuppValor).DefaultIfEmpty()
                                                     .Sum();
                    if (totalComisionSuppValor != 0 || (oBO_Ticket != null && oBO_Ticket.ComSupl != 0)) //TODO: Que va aca en vez de comsupl?
                    {
                        lstOverCompania.Add(GetOver(oBSP_Ticket, oBO_Ticket));
                    }
                }

                foreach (BO_Ticket bo_ticketFaltante in oSemana.TicketsBO.Where(x => x.Compania.ID == compania.ID && !lstTicketsBSP.Any(y => y.NroDocumento == x.Billete && y.Compania.Codigo == compania.Codigo) && x.ComSupl != 0).OrderBy(x => x.Billete)) //TODO: Que va acá en vez de ComSupl?
                {
                    lstOverCompania.Add(GetOver(null, bo_ticketFaltante));
                }

                if (lstOverCompania.Count > 0)
                {
                    lstOver.AddRange(lstOverCompania);

                    var oOverTotal = new Over();
                    oOverTotal.BoletoNro = "TOTAL";
                    oOverTotal.OverRecPesos = lstOverCompania.Select(x => x.OverRecPesos).Sum();
                    oOverTotal.OverPedPesos = lstOverCompania.Select(x => x.OverPedPesos).Sum();
                    oOverTotal.OverRecDolares = lstOverCompania.Select(x => x.OverRecDolares).Sum();
                    oOverTotal.OverPedDolares = lstOverCompania.Select(x => x.OverPedDolares).Sum();
                    oOverTotal.DiferenciasPesos = lstOverCompania.Select(x => x.DiferenciasPesos).Sum();
                    oOverTotal.DiferenciasDolares = lstOverCompania.Select(x => x.DiferenciasDolares).Sum();

                    lstOver.Add(oOverTotal);
                }
            }

            return lstOver;
        }

        private Over GetOver(BSP_Ticket oBSP_Ticket, BO_Ticket oBO_Ticket)
        {
            Over oOver = new Over();

            oOver.Cia = oBSP_Ticket.Compania.Codigo;
            oOver.BoletoNro = oBSP_Ticket.NroDocumento.ToString();
            oOver.FechaEmision = AuditurHelpers.GetDateTimeString(oBSP_Ticket != null ? oBSP_Ticket.FechaEmision : oBO_Ticket.Fecha);
            oOver.NetRemit = oBSP_Ticket.Nr;
            oOver.TourCode = oBSP_Ticket.Tour;
            oOver.Observaciones = "";

            if (oBSP_Ticket != null)
            {
                var totalComisionSuppValor = oBSP_Ticket.ComisionSuppValor +
                                              oBSP_Ticket.Detalle.Select(x => x.ComisionSuppValor).DefaultIfEmpty(0)
                                                  .Sum();
                if (oBSP_Ticket.Moneda == Moneda.Peso)
                    oOver.OverRecPesos = -totalComisionSuppValor;
                else
                    oOver.OverRecDolares = -totalComisionSuppValor;
            }

            if (oBO_Ticket != null)
            {
                if (oBO_Ticket.Moneda == Moneda.Peso)
                    oOver.OverPedPesos = oBO_Ticket.ComSupl; //oBO_Ticket.; TODO: Que va aca?
                else if (oBO_Ticket.Moneda == Moneda.Dolar)
                    oOver.OverPedDolares = oBO_Ticket.ComSupl; //oBO_Ticket.FacturaNro; TODO: Que va aca?
                oOver.Factura = oBO_Ticket.FacturaNro;
                oOver.Pasajero = oBO_Ticket.Pax;
                oOver.Operacion = oBO_Ticket.OperacionNro;
            }

            oOver.DiferenciasPesos = oOver.OverRecPesos - oOver.OverPedPesos;
            oOver.DiferenciasDolares = oOver.OverRecDolares - oOver.OverPedDolares;

            return oOver;
        }
    }
}