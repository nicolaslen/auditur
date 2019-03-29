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
        public bool EsAnalisisDeVenta(ref string[] arrLineas)
        {
            return arrLineas.Length > 2 && arrLineas[2].IndexOf("ANALISIS DE VENTAS") > -1;
        }

        public bool EsParteDeLaTabla(string Linea)
        {
            return Linea.Length == 165;// && (EsNuevoTicket(Linea) || Linea.Trim() == "");
        }

        public bool EsNuevoTicket(string Linea)
        {
            return Validators.IsLong(Linea.Substring(0, 10));
        }

        public bool EsAerolinea(string Linea)
        {
            return Linea.Length == 34;
        }

        public BSP_Ticket GetTicket(string Linea, int Year)
        {
            BSP_Ticket oBSP_Ticket = null;
            try
            {
                oBSP_Ticket = new BSP_Ticket();
                oBSP_Ticket.NroDocumento = Convert.ToInt64(Linea.Substring(0, 10).Trim());
                oBSP_Ticket.Tipo = Linea.Substring(13, 4).ToUpper();
                string FechaVenta = Linea.Substring(19, 5).Trim() + "/" + Year.ToString();
                if (Validators.IsDateTime(FechaVenta))
                    oBSP_Ticket.FechaEmision = Convert.ToDateTime(FechaVenta);
                oBSP_Ticket.TarContado = GetTicket_ObtenerDecimal(GetStringByIndex(Linea, 25, 10));
                oBSP_Ticket.TarCredito = GetTicket_ObtenerDecimal(GetStringByIndex(Linea, 35, 12));

                oBSP_Ticket.IVA105 = GetTicket_ObtenerDecimal(GetStringByIndex(Linea, 72, 11));
                oBSP_Ticket.ComPorcentaje = GetTicket_ObtenerDecimal(GetStringByIndex(Linea, 94, 7));
                oBSP_Ticket.ComValor = GetTicket_ObtenerDecimal(GetStringByIndex(Linea, 101, 9));
                oBSP_Ticket.ComOver = GetTicket_ObtenerDecimal(GetStringByIndex(Linea, 111, 11));
                oBSP_Ticket.ComIVA = GetTicket_ObtenerDecimal(GetStringByIndex(Linea, 121, 12));
                oBSP_Ticket.Total = GetTicket_ObtenerDecimal(GetStringByIndex(Linea, 133, 11));
            }
            catch (Exception ex)
            {
                TextToFile.Errores(TextToFile.Error(ex));
            }
            return oBSP_Ticket;
        }

        private string GetStringByIndex(string Linea, int index, int Max)
        {
            int First = 0;
            int Length = 0;

            if (Linea.Length > index)
            {
                while (Linea[index] == ' ')
                {
                    index++;
                    Max--;
                    if (Max == 0)
                        return "";
                }
                //First = Linea.Substring(0, index + 1).LastIndexOf(' ') + 1;
                First = index;
                Length = Linea.Substring(First).IndexOf(' ') + 1;
            }
            return Length > 0 ? Linea.Substring(First, Length) : Linea.Substring(First);
        }

        public BSP_Ticket_Detalle GetTicketDetalle(string Linea)
        {
            BSP_Ticket_Detalle oBSP_Ticket_Detalle = new BSP_Ticket_Detalle();
            try
            {
                oBSP_Ticket_Detalle.ISO = Linea.Substring(49, 2).Trim();
                oBSP_Ticket_Detalle.ImpContado = GetTicket_ObtenerDecimal(GetStringByIndex(Linea, 52, 9));
                oBSP_Ticket_Detalle.ImpCredito = GetTicket_ObtenerDecimal(GetStringByIndex(Linea, 61, 11));
                oBSP_Ticket_Detalle.IVA21 = GetTicket_ObtenerDecimal(GetStringByIndex(Linea, 83, 12));
                oBSP_Ticket_Detalle.Observaciones = GetStringByIndex(Linea, 144, 20).Trim();
            }
            catch (Exception ex)
            {
                TextToFile.Errores(TextToFile.Error(ex));
            }
            return oBSP_Ticket_Detalle;
        }

        private decimal GetTicket_ObtenerDecimal(string valor)
        {
            valor = valor.Trim();
            if (valor == "")
                return 0;
            else if (valor.Substring(valor.Length - 1) == "-")
                return -Convert.ToDecimal(valor.Substring(0, valor.Length - 1), culture);
            else
                return Convert.ToDecimal(valor, culture);
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
                    int intPorcentaje = 0;
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
                        intPorcentaje = intCont * 100 / oSemana.TicketsBSP.Count;
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
