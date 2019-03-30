using Auditur.Negocio;
using Helpers;
using System;
using System.ComponentModel;
using System.Data.SqlServerCe;
using System.Globalization;

namespace Auditur.Presentacion.Classes
{
    public class BSPActions
    {
        private CultureInfo culture;

        public BSPActions(CultureInfo _culture)
        {
            this.culture = _culture;
        }

        public void Guardar(Semana oSemana, BackgroundWorker backgroundWorker1)
        {
            try
            {
                if (oSemana == null)
                    return;

                using (SqlCeConnection conn = AccesoDatos.OpenConn())
                {
                    Semanas semanas = new Semanas(conn);
                    BSP_Tickets bsp_tickets = new BSP_Tickets(conn);
                    if (oSemana.ID > 0)
                    {
                        if (oSemana.TicketsBSP.Count > 0)
                        {
                            bsp_tickets.EliminarPorSemana(oSemana.ID);
                        }
                    }
                    else
                    {
                        semanas.Insertar(oSemana);
                    }

                    BSP_Ticket_Detalles bsp_Ticket_Detalles = new BSP_Ticket_Detalles(conn);
                    int intCont = 0;
                    foreach (BSP_Ticket Ticket in oSemana.TicketsBSP)
                    {
                        Ticket.SemanaID = oSemana.ID;
                        bsp_tickets.Insertar(Ticket);
                        foreach (BSP_Ticket_Detalle TicketDetalle in Ticket.Detalle)
                        {
                            TicketDetalle.TicketID = Ticket.ID;
                            bsp_Ticket_Detalles.Insertar(TicketDetalle);
                        }
                        intCont++;
                        var intPorcentaje = intCont * 100 / oSemana.TicketsBSP.Count;
                        backgroundWorker1.ReportProgress(intPorcentaje, "Guardando BSP... " + intCont + " de " + oSemana.TicketsBSP.Count + " registros guardados.");
                    }
                }
            }
            catch (Exception ex)
            {
                TextToFile.Errores(TextToFile.Error(ex));
            }
        }
    }
}