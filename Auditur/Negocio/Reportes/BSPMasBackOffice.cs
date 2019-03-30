using System.ComponentModel.DataAnnotations;

namespace Auditur.Negocio.Reportes
{
    public class BSPMasBackOffice
    {
        [Display(Name = "Cia")]
        public string Cia { get; set; }

        [Display(Name = "Tipo")]
        public string Tipo { get; set; }

        [Display(Name = "Ref")]
        public string Ref { get; set; }

        [Display(Name = "Boleto Nro")]
        public string BoletoNro { get; set; }

        [Display(Name = "Fecha Emisión")]
        public string FechaEmision { get; set; }

        [Display(Name = "Moneda")]
        public string Moneda { get; set; }

        [Display(Name = "Tour Code")]
        public string TourCode { get; set; }

        [Display(Name = "Cod Nr")]
        public string CodNr { get; set; }

        [Display(Name = "Stat")]
        public string Stat { get; set; }

        [Display(Name = "Fop CA")]
        public string FopCA { get; set; }

        [Display(Name = "Fop CC")]
        public string FopCC { get; set; }

        [Display(Name = "Total Transacción")]
        public decimal TotalTransac { get; set; }

        [Display(Name = "Valor Tarifa")]
        public decimal ValorTarifa { get; set; }

        [Display(Name = "Imp")]
        public decimal Imp { get; set; }

        [Display(Name = "T&C")]
        public decimal TyC { get; set; }

        [Display(Name = "IVA Tarifa")]
        public decimal IVATarifa { get; set; }

        [Display(Name = "Pen")]
        public decimal Pen { get; set; }

        [Display(Name = "Cobl")]
        public decimal Cobl { get; set; }

        [Display(Name = "ComStd")]
        public decimal ComStd { get; set; }

        [Display(Name = "ComSupl")]
        public decimal ComSupl { get; set; }

        [Display(Name = "IVA Com")]
        public decimal IVAComisiones { get; set; }

        [Display(Name = "NetoAPagar")]
        public decimal NetoAPagar { get; set; }

        [Display(Name = "Operación")]
        public string Operacion { get; set; }

        [Display(Name = "Factura")]
        public string Factura { get; set; }

        [Display(Name = "Pasajero")]
        public string Pasajero { get; set; }
    }
}