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
        private CultureInfo culture;
        public BOActions(CultureInfo _culture)
        {
            this.culture = _culture;
        }
        public BO_Ticket GetDetalles(ref string Linea, int iLinea, List<Compania> lstCompanias)
        {
            BO_Ticket oBO_Detalle = null;
            try
            {
                Compania oCompaniaActual = null;

                string[] Columnas = Linea.Split(new char[] { ',', ';' });

                oBO_Detalle = new BO_Ticket();
                oBO_Detalle.IATA = Convert.ToInt64(GetColumn(Columnas, 0, true));
                oBO_Detalle.Billete = Convert.ToInt64(GetColumn(Columnas, 1, true));

                DateTime dtFecha;
                string strFecha = GetColumn(Columnas, 2, false);
                /*if (!DateTime.TryParse(strFecha, out dtFecha))
                    dtFecha = new DateTime(DateTime.Today.Year, strFecha.Substring(strFecha.IndexOf("-") + 1)
                oBO_Detalle.Fecha = dtFecha;*/
                if (DateTime.TryParse(strFecha, out dtFecha))
                    oBO_Detalle.Fecha = dtFecha;

                oBO_Detalle.Void = GetColumn(Columnas, 3, false) == "1";

                string CompaniaCod = GetColumn(Columnas, 4, false);
                oCompaniaActual = lstCompanias.Find(x => x.Codigo == CompaniaCod);
                if (oCompaniaActual != null)
                    oBO_Detalle.Compania = oCompaniaActual;
                else
                    oBO_Detalle.Compania = new Compania { Codigo = CompaniaCod };
  
                oBO_Detalle.Tarifa = Convert.ToDecimal(GetColumn(Columnas, 5, true), culture);
                oBO_Detalle.TarContado = Convert.ToDecimal(GetColumn(Columnas, 6, true), culture);
                oBO_Detalle.TarCredito = Convert.ToDecimal(GetColumn(Columnas, 7, true), culture);
                oBO_Detalle.Impuestos = Convert.ToDecimal(GetColumn(Columnas, 8, true), culture);
                //oBO_Detalle.IVA21 = Convert.ToDecimal(GetColumn(Columnas, 9), culture);
                oBO_Detalle.Comision = Convert.ToDecimal(GetColumn(Columnas, 10, true), culture);
                oBO_Detalle.Total = Convert.ToDecimal(GetColumn(Columnas, 11, true), culture);
                //oBO_Detalle.Final = Convert.ToDecimal(GetColumn(Columnas, 12), culture);
                oBO_Detalle.ComOver = Convert.ToDecimal(GetColumn(Columnas, 13, true), culture);
                oBO_Detalle.Expediente = GetColumn(Columnas, 14, true);
                oBO_Detalle.IVA105 = Convert.ToDecimal(GetColumn(Columnas, 15, true), culture);
                oBO_Detalle.IVAComision = Convert.ToDecimal(GetColumn(Columnas, 16, true), culture);
                oBO_Detalle.ComValor = Convert.ToDecimal(GetColumn(Columnas, 17, true), culture);
                oBO_Detalle.Factura = GetColumn(Columnas, 18, true);
                oBO_Detalle.Pasajero = GetColumn(Columnas, 19, false);
                oBO_Detalle.Vendedor = GetColumn(Columnas, 20, false);
                oBO_Detalle.Moneda = Moneda.Peso;
            }
            catch (Exception ex)
            {
                TextToFile.Errores(TextToFile.Error(ex));
            }
            return oBO_Detalle;
        }

        private string GetColumn(string[] Columnas, int index, bool isNumber)
        {
            return (Columnas.Length > index ? (isNumber ? Columnas[index].Replace('.', ',') : Columnas[index]) : (isNumber ? "0" : "")).Trim();
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
