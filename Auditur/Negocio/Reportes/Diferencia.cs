using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Auditur.Negocio.Reportes
{
    public class Diferencia
    {
        [Display(Name = "Boleto Nro")]
        public string BoletoNroBSP { get; set; }
        [Display(Name = "Rg")]
        public string RgBSP { get; set; }
        [Display(Name = "Tr")]
        public string TrBSP { get; set; }
        [Display(Name = "Tarifa")]
        public decimal TarifaBSP { get; set; }
        [Display(Name = "Contado")]
        public decimal ContadoBSP { get; set; }
        [Display(Name = "Crédito")]
        public decimal CreditoBSP { get; set; }
        [Display(Name = "Impuestos")]
        public decimal ImpuestosBSP { get; set; }
        [Display(Name = "Comisión")]
        public decimal ComisionBSP { get; set; }
        [Display(Name = "$/D")]
        public string MonedaBSP { get; set; }

        [Display(Name = "Tarifa ")]
        public decimal TarifaBO { get; set; }
        [Display(Name = "Contado ")]
        public decimal ContadoBO { get; set; }
        [Display(Name = "Crédito ")]
        public decimal CreditoBO { get; set; }
        [Display(Name = "Impuestos ")]
        public decimal ImpuestosBO { get; set; }
        [Display(Name = "Comisión ")]
        public decimal ComisionBO { get; set; }
        public string Factura { get; set; }
        public string Pasajero { get; set; }
        [Display(Name = "$/D ")]
        public string MonedaBO { get; set; }
        [Display(Name = "Tarifa  ")]
        public string TarifaDif { get; set; }
        [Display(Name = "Contado  ")]
        public decimal ContadoDif { get; set; }
        [Display(Name = "Crédito  ")]
        public decimal CreditoDif { get; set; }
        [Display(Name = "Impuestos  ")]
        public decimal ImpuestosDif { get; set; }
        [Display(Name = "Comisión  ")]
        public decimal ComisionDif { get; set; }
        [Display(Name = "Op. Nº")]
        public string Operacion { get; set; }
    }
}
