using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Auditur.Negocio.Reportes
{
    public class BSPNroOP
    {
        [Display(Name = "Operación")]
        public string Operacion { get; set; }
        [Display(Name = "Cia")]
        public string Cia { get; set; }
        [Display(Name = "Rg")]
        public string Rg { get; set; }
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }
        [Display(Name = "Boleto Nro")]
        public string BoletoNro { get; set; }
        [Display(Name = "Moneda")]
        public string Moneda { get; set; }
        [Display(Name = "Emisión")]
        public string FechaEmision { get; set; }
        [Display(Name = "Tarifa")]
        public decimal Tarifa { get; set; }
        [Display(Name = "Contado")]
        public decimal Contado { get; set; }
        [Display(Name = "Crédito")]
        public decimal Credito { get; set; }
        [Display(Name = "Contado ")]
        public decimal ImpContado { get; set; }
        [Display(Name = "Crédito ")]
        public decimal ImpCredito { get; set; }
        [Display(Name = "10,50%")]
        public decimal IVA10 { get; set; }
        [Display(Name = "Normal")]
        public decimal ComNormal { get; set; }
        [Display(Name = "Over")]
        public decimal ComOver { get; set; }
        [Display(Name = "Comisiones")]
        public decimal IVAComisiones { get; set; }
        [Display(Name = "Final")]
        public decimal TotalFinal { get; set; }
        [Display(Name = "Factura")]
        public string Factura { get; set; }
        [Display(Name = "Pasajero")]
        public string Pasajero { get; set; }
    }
}
