using System.ComponentModel.DataAnnotations;

namespace Auditur.Negocio.Reportes
{
    public class Reembolso
    {
        [Display(Name = "Cia")]
        public string Cia { get; set; }
        [Display(Name = "RFND Nro")]
        public string RfndNro { get; set; }
        [Display(Name = "RTDN Nro")]
        public string RtdnNro { get; set; }
        [Display(Name = "Fecha emisión")]
        public string Fecha { get; set; }
        [Display(Name = "$/D")]
        public string Moneda { get; set; }
        [Display(Name = "FOP CA")]
        public string FopCA;
        [Display(Name = "FOP CC")]
        public string FopCC;
        [Display(Name = "Total Transaccion")]
        public decimal TotalTransaccion { get; set; }
        [Display(Name = "Valor Tarifa")]
        public decimal ValorTarifa { get; set; }
        [Display(Name = "Imp")]
        public decimal Imp { get; set; }
        [Display(Name = "T&C")]
        public decimal TyC { get; set; }
        [Display(Name = "IVA Tarifa")]
        public decimal IVATarifa { get; set; }
        [Display(Name = "Penalidad")]
        public decimal Penalidad { get; set; }
        [Display(Name = "Com STD Valor")]
        public decimal ComStdValor { get; set; }
        [Display(Name = "Com SUPP Valor")]
        public decimal ComSuppValor { get; set; }
        [Display(Name = "IVA S/Com")]
        public decimal IVASinComision { get; set; }
        [Display(Name = "Neto A Pagar")]
        public decimal NetoAPagar { get; set; }
        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }
    }
}