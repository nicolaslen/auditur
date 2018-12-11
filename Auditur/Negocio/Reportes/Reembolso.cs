using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Auditur.Negocio.Reportes
{
    public class Reembolso
    {
        [Display(Name = "Boleto Nro")]
        public string BoletoNro { get; set; }
        public string Rg { get; set; }
        public string Tr { get; set; }
        public string Fecha { get; set; }
        public decimal Tarifa { get; set; }
        public decimal Contado { get; set; }
        [Display(Name = "Crédito")]
        public decimal Credito { get; set; }
        [Display(Name = "Imp.Cont.")]
        public decimal ImpContado { get; set; }
        [Display(Name = "Imp.Tarj.")]
        public decimal ImpCredito { get; set; }
        public decimal IVA { get; set; }
        public decimal CC { get; set; }
        [Display(Name = "Comisión")]
        public decimal Comision { get; set; }
        public decimal Importe { get; set; }
        [Display(Name = "$/D")]
        public string Moneda { get; set; }
        [Display(Name = "Reembolso Nro")]
        public string ReembolsoNro { get; set; }
    }
}
