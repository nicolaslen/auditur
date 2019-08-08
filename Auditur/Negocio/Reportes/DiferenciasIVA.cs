using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Auditur.Negocio.Reportes
{
    public class DiferenciasIVA
    {
        [Display(Name = " ")]
        public string Origen { get; set; }

        [Display(Name = "Cia")]
        public string Cia { get; set; }

        [Display(Name = "Nro. Docto")]
        public string BoletoNro { get; set; }

        [Display(Name = "Fecha Emis.")]
        public string FechaEmision { get; set; }

        [Display(Name = "$/D")]
        public string Moneda { get; set; }

        [Display(Name = "Valor Tarifa")]
        public decimal ValorTarifa { get; set; }

        [Display(Name = "IVA Tarifa")]
        public decimal IVATarifa { get; set; }

        [Display(Name = "Com Std")]
        public decimal ComStdValor { get; set; }

        [Display(Name = "Com Supl")]
        public decimal ComSuppValor { get; set; }

        [Display(Name = "IVA Com")]
        public decimal IVAComision { get; set; }

        [Display(Name = "Operación N°")]
        public string OperacionNro { get; set; }

        [Display(Name = "Factura N°")]
        public string Factura { get; set; }

        [Display(Name = "Pax")]
        public string Pasajero { get; set; }
    }
}
