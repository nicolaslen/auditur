using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Auditur.Negocio.Reportes
{
    public class Facturacion
    {
        [Display(Name = "Cia")]
        public string Cia { get; set; }

        [Display(Name = "TRNC")]
        public string Tipo { get; set; }

        [Display(Name = "Nro. Docto")]
        public string BoletoNro { get; set; }

        [Display(Name = "RTDN")]
        public string RTDN { get; set; }

        [Display(Name = "Fecha Emis.")]
        public string FechaEmision { get; set; }

        [Display(Name = "Stat")]
        public string Stat { get; set; }

        [Display(Name = "Valor Tarifa")]
        public decimal ValorTarifa { get; set; }

        [Display(Name = "QN")]
        public decimal QN { get; set; }

        [Display(Name = "Imp")]
        public decimal Impuestos { get; set; }

        [Display(Name = "T&C")]
        public decimal TyC { get; set; }
        
        [Display(Name = "IVA Tarifa")]
        public decimal IVATarifa { get; set; }

        [Display(Name = "Com Std")]
        public decimal ComStdValor { get; set; }

        [Display(Name = "Com Supl")]
        public decimal ComSuppValor { get; set; }

        [Display(Name = "IVA Com")]
        public decimal IVASinComision { get; set; }
    }
}
