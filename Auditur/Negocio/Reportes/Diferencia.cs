using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Auditur.Negocio.Reportes
{
    public class Diferencia
    {
        [Display(Name = "Cia")]
        public string Cia { get; set; }

        [Display(Name = "TRNC N°")]
        public string Trnc { get; set; }

        [Display(Name = "Nro. Docto")]
        public string BoletoNro { get; set; }

        [Display(Name = "RTDN N°")]
        public string RTDN { get; set; }

        [Display(Name = "Fecha Emis.")]
        public string FechaEmision { get; set; }

        [Display(Name = "$/D")]
        public string Moneda { get; set; }

        [Display(Name = "Destino I/D")]
        public string Stat { get; set; }

        [Display(Name = "Fop CA")]
        public decimal FopCA { get; set; }

        [Display(Name = "Fop CC")]
        public decimal FopCC { get; set; }

        [Display(Name = "Total Transac")]
        public decimal TotalTransaccion { get; set; }

        [Display(Name = "Valor Tarifa")]
        public decimal ValorTarifa { get; set; }

        [Display(Name = "Imp")]
        public decimal Imp { get; set; }

        [Display(Name = "T&C")]
        public decimal TyC { get; set; }

        [Display(Name = "IVA Tarifa")]
        public decimal IVATarifa { get; set; }

        [Display(Name = "Pen")]
        public decimal Penalidad { get; set; }

        [Display(Name = "Cobl")]
        public decimal Cobl { get; set; }

        [Display(Name = "Com Std")]
        public decimal ComStdValor { get; set; }

        [Display(Name = "Com Supl")]
        public decimal ComSuppValor { get; set; }

        [Display(Name = "IVA Com")]
        public decimal IVASinComision { get; set; }

        [Display(Name = "Neto A Pagar")]
        public decimal NetoAPagar { get; set; }
        
        [Display(Name = "Operación N°")]
        public string OperacionNro { get; set; }

        [Display(Name = "Factura N°")]
        public string Factura { get; set; }

        [Display(Name = "Pax")]
        public string Pasajero { get; set; }
    }
}
