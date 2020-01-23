using System.ComponentModel.DataAnnotations;

namespace Auditur.Negocio.Reportes
{
    public class Over
    {
        [Display(Name = "Cia")]
        public string Cia { get; set; }

        [Display(Name = "Nro. Docto")]
        public string BoletoNro { get; set; }

        [Display(Name = "Fecha Emis.")]
        public string FechaEmision { get; set; }

        [Display(Name = "$/D")]
        public string Moneda { get; set; }

        [Display(Name = "TOUR CODE")]
        public string TourCode { get; set; }

        [Display(Name = "Over Ped.")]
        public decimal OverPed { get; set; }

        [Display(Name = "Over Rec.")]
        public decimal OverRec { get; set; }

        [Display(Name = "(Rec. - Ped.)")]
        public decimal Diferencias { get; set; }

        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }

        [Display(Name = "Operación N°")]
        public string Operacion { get; set; }

        [Display(Name = "Factura N°")]
        public string Factura { get; set; }

        [Display(Name = "Pax")]
        public string Pasajero { get; set; }
    }
}