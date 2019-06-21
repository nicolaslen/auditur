using System.ComponentModel.DataAnnotations;

namespace Auditur.Negocio.Reportes
{
    public class Reembolso
    {
        [Display(Name = "Cia")]
        public string Cia { get; set; }

        [Display(Name = "TRNC")]
        public string Tipo { get; set; }

        [Display(Name = "RFND N°")]
        public string BoletoNro { get; set; }

        [Display(Name = "RTDN N°")]
        public string RTDN { get; set; }
        
        [Display(Name = "Fecha Emis.")]
        public string FechaEmision { get; set; }

        [Display(Name = "$/D")]
        public string Moneda { get; set; }

        [Display(Name = "Fop CA")]
        public decimal FopCA { get; set; }

        [Display(Name = "Fop CC")]
        public decimal FopCC { get; set; }

        [Display(Name = "Total Transacción")]
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
        
        [Display(Name = "Com Std Valor")]
        public decimal ComStdValor { get; set; }

        [Display(Name = "Com Supp Valor")]
        public decimal ComSuppValor { get; set; }

        [Display(Name = "IVA S/Com")]
        public decimal IVASinComision { get; set; }

        [Display(Name = "Neto A Pagar")]
        public decimal NetoAPagar { get; set; }

        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }
    }
}