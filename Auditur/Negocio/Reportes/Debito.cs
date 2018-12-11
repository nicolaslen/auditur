using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Auditur.Negocio.Reportes
{
    public class Debito
    {
        [Display(Name = "ADM Nro")]
        public string Nro { get; set; }
        public string Rg { get; set; }
        public string Tr { get; set; }
        public decimal Tarifa { get; set; }
        public decimal Contado { get; set; }
        [Display(Name = "Crédito")]
        public decimal Credito { get; set; }
        public decimal Impuestos { get; set; }
        public decimal IVA { get; set; }
        [Display(Name = "Comisión")]
        public decimal Comision { get; set; }
        public string Fecha { get; set; }
        public decimal Importe { get; set; }
        [Display(Name = "$/D")]
        public string Moneda { get; set; }
        public string Observaciones { get; set; }
    }
}
