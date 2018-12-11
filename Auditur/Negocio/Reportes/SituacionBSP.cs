using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Auditur.Negocio.Reportes
{
    public class SituacionBSP
    {
        [Display(Name = "Boleto Nro")]
        public string BoletoNro { get; set; }
        [Display(Name = "Rg")]
        public string Rg { get; set; }
        [Display(Name = "Tr")]
        public string Tr { get; set; }
        [Display(Name = "Tarifa")]
        public decimal Tarifa { get; set; }
        [Display(Name = "Contado")]
        public decimal Contado { get; set; }
        [Display(Name = "Crédito")]
        public decimal Credito { get; set; }
        [Display(Name = "Impuestos")]
        public decimal Impuestos { get; set; }
        [Display(Name = "Comisión")]
        public decimal Comision { get; set; }
        [Display(Name = "Importe")]
        public decimal Importe { get; set; }
        [Display(Name = "$/D")]
        public string Moneda { get; set; }
        [Display(Name = "Fecha")]
        public string Fecha { get; set; }
        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }
    }
}
