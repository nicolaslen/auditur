using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helpers;

namespace Auditur.Negocio.Reportes
{
    public class Overs : IReport<Over>
    {
        public List<Over> Generar(Semana oSemana)
        {
            List<Over> lstOver = new List<Over>();
            List<Over> lstOverCompania = new List<Over>();
            Over oOverTotal = null;

            Companias Companias = new Companias();
            List<Compania> companias = Companias.GetAll();
            Companias.CloseConnection();

            List<BSP_Ticket> lstTicketsBSP = oSemana.TicketsBSP.Where(x => x.Concepto.Tipo == 'B').OrderBy(x => x.Compania.Codigo).ThenBy(x => x.NroDocumento).ToList();
            foreach (Compania compania in companias.OrderBy(x => x.Codigo))
            {
                lstOverCompania = new List<Over>();

                foreach (BSP_Ticket oBSP_Ticket in lstTicketsBSP.Where(x => x.Compania.ID == compania.ID))
                {
                    BO_Ticket oBO_Ticket = oSemana.TicketsBO.Find(x => x.Billete == oBSP_Ticket.NroDocumento && x.Compania.Codigo == oBSP_Ticket.Compania.Codigo);

                    if (oBSP_Ticket.ComOver != 0 || (oBO_Ticket != null && oBO_Ticket.ComOver != 0))
                    {
                        lstOverCompania.Add(GetOver(oBSP_Ticket, oBO_Ticket));
                    }
                }

                foreach (BO_Ticket bo_ticketFaltante in oSemana.TicketsBO.Where(x => x.Compania.ID == compania.ID && !lstTicketsBSP.Any(y => y.NroDocumento == x.Billete && y.Compania.Codigo == compania.Codigo) && x.ComOver != 0).OrderBy(x => x.Billete))
                {
                    lstOverCompania.Add(GetOver(null, bo_ticketFaltante));
                }

                if (lstOverCompania.Count > 0)
                {
                    lstOver.AddRange(lstOverCompania);

                    oOverTotal = new Over();
                    oOverTotal.Boleto = "TOTAL";
                    oOverTotal.Tr = compania.Codigo;
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

            oOver.Boleto = oBSP_Ticket != null ? oBSP_Ticket.NroDocumento.ToString() : oBO_Ticket.Billete.ToString();
            oOver.Fecha = AuditurHelpers.GetDateTimeString(oBSP_Ticket != null ? oBSP_Ticket.FechaEmision : oBO_Ticket.Fecha);
            oOver.Tr = oBSP_Ticket != null ? oBSP_Ticket.Compania.Codigo : oBO_Ticket.Compania.Codigo;
            oOver.Observaciones = "";

            if (oBSP_Ticket != null)
            {
                if (oBSP_Ticket.Moneda == Moneda.Peso)
                    oOver.OverRecPesos = -oBSP_Ticket.ComOver;
                else
                    oOver.OverRecDolares = -oBSP_Ticket.ComOver;
            }

            if (oBO_Ticket != null)
            {
                if (oBO_Ticket.Moneda == Moneda.Peso)
                    oOver.OverPedPesos = oBO_Ticket.ComOver;
                else if (oBO_Ticket.Moneda == Moneda.Dolar)
                    oOver.OverPedDolares = oBO_Ticket.ComOver;
                oOver.Factura = oBO_Ticket.Factura;
                oOver.Pasajero = oBO_Ticket.Pasajero;
                oOver.Operacion = oBO_Ticket.Expediente;
            }

            oOver.DiferenciasPesos = oOver.OverRecPesos - oOver.OverPedPesos;
            oOver.DiferenciasDolares = oOver.OverRecDolares - oOver.OverPedDolares;

            return oOver;
        }
    }
}
