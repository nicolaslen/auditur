using Auditur.Negocio;
using Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlServerCe;
using System.Globalization;

namespace Auditur.Presentacion.Classes
{
    public class BOActions
    {
        public BO_Ticket GetDetalles(ref string Linea, int iLinea, List<Compania> lstCompanias)
        {
            BO_Ticket oBO_Detalle = null;
            try
            {
                //TODO: sacar culture, unificar convert.todecimal como en bsp, y corregir errores de lectura

                Compania oCompaniaActual = null;

                string[] Columnas = Linea.Split(new char[] { ',', ';' });

                oBO_Detalle = new BO_Ticket();
                int colNumber = 0;

                oBO_Detalle.IATA = Convert.ToInt64(GetColumn(Columnas, colNumber++, true));
                string companiaCod = GetColumn(Columnas, colNumber++, false);
                oCompaniaActual = lstCompanias.Find(x => x.Codigo == companiaCod);
                if (oCompaniaActual != null)
                    oBO_Detalle.Compania = oCompaniaActual;
                else
                    oBO_Detalle.Compania = new Compania { Codigo = companiaCod };
                oBO_Detalle.Billete = -Convert.ToInt64(GetColumn(Columnas, colNumber++, true));
                string strFecha = GetColumn(Columnas, colNumber++, false);
                if (DateTime.TryParse(strFecha, out var dtFecha))
                    oBO_Detalle.Fecha = dtFecha;
                oBO_Detalle.Moneda = GetColumn(Columnas, colNumber++, false) == "D" ? Moneda.Dolar : Moneda.Peso;
                oBO_Detalle.CA = GetColumn(Columnas, colNumber++, true).ToDecimal();
                oBO_Detalle.CC = GetColumn(Columnas, colNumber++, true).ToDecimal();
                oBO_Detalle.TotalTransaccion = GetColumn(Columnas, colNumber++, true).ToDecimal();
                oBO_Detalle.ValorTarifa = GetColumn(Columnas, colNumber++, true).ToDecimal();
                oBO_Detalle.Impuestos = GetColumn(Columnas, colNumber++, true).ToDecimal();
                oBO_Detalle.TasasCargos = GetColumn(Columnas, colNumber++, true).ToDecimal();
                oBO_Detalle.IVATarifa = GetColumn(Columnas, colNumber++, true).ToDecimal();
                oBO_Detalle.ComStd = GetColumn(Columnas, colNumber++, true).ToDecimal();
                oBO_Detalle.ComSupl = GetColumn(Columnas, colNumber++, true).ToDecimal();
                oBO_Detalle.IVACom = GetColumn(Columnas, colNumber++, true).ToDecimal();
                oBO_Detalle.Neto = GetColumn(Columnas, colNumber++, true).ToDecimal();
                colNumber++;
                oBO_Detalle.OperacionNro = GetColumn(Columnas, colNumber++, true);
                oBO_Detalle.FacturaNro = GetColumn(Columnas, colNumber++, true);
                oBO_Detalle.Pax = GetColumn(Columnas, colNumber++, true);
            }
            catch (Exception ex)
            {
                TextToFile.Errores(TextToFile.Error(ex));
            }
            return oBO_Detalle;
        }

        private string GetColumn(string[] Columnas, int index, bool isNumber)
        {
            return (Columnas.Length > index && !string.IsNullOrWhiteSpace(Columnas[index]) ? (isNumber ? Columnas[index].Replace('.', ',') : Columnas[index]) : (isNumber ? "0" : "")).Trim();
        }

        public void Guardar(Semana oSemana, BackgroundWorker backgroundWorker1)
        {
            try
            {
                using (SqlCeConnection conn = AccesoDatos.OpenConn())
                {
                    if (oSemana == null)
                        return;

                    Semanas semanas = new Semanas(conn);
                    BO_Tickets bo_tickets = new BO_Tickets(conn);
                    if (oSemana.ID > 0)
                    {
                        if (oSemana.TicketsBO.Count > 0)
                        {
                            bo_tickets.EliminarPorSemana(oSemana.ID);
                        }
                    }
                    else
                    {
                        semanas.Insertar(oSemana);
                    }


                    int intCont = 0;
                    int intPorcentaje = 0;

                    foreach (BO_Ticket Ticket in oSemana.TicketsBO)
                    {
                        Ticket.SemanaID = oSemana.ID;
                        bo_tickets.Insertar(Ticket);
                        intCont++;
                        intPorcentaje = intCont * 100 / oSemana.TicketsBO.Count;
                        backgroundWorker1.ReportProgress(intPorcentaje, "Guardando BO... " + intCont + " de " + oSemana.TicketsBO.Count + " registros guardados.");
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
