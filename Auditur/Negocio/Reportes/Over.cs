using System.ComponentModel.DataAnnotations;

namespace Auditur.Negocio.Reportes
{
    public class Over
    {
        [Display(Name = "Cia")]
        public string Cia { get; set; }

        [Display(Name = "Nro. Docto")]
        public string BoletoNro { get; set; }

        [Display(Name = "Fecha Emisión")]
        public string FechaEmision { get; set; }

        [Display(Name = "NET REMIT")]
        public string NetRemit { get; set; }

        [Display(Name = "TOUR CODE")]
        public string TourCode { get; set; }

        [Display(Name = "Over Ped. $")]
        public decimal OverPedPesos { get; set; }

        [Display(Name = "Over Rec. $")]
        public decimal OverRecPesos { get; set; }

        [Display(Name = "Diferencias $")]
        public decimal DiferenciasPesos { get; set; }

        [Display(Name = "Over Ped. u$s")]
        public decimal OverPedDolares { get; set; }

        [Display(Name = "Over Rec. u$s")]
        public decimal OverRecDolares { get; set; }

        [Display(Name = "Diferencias u$s")]
        public decimal DiferenciasDolares { get; set; }

        [Display(Name = "Op. Nº")]
        public string Operacion { get; set; }
        [Display(Name = "Factura Nº")]
        public string Factura { get; set; }
        [Display(Name = "Pax")]
        public string Pasajero { get; set; }

        public string Observaciones { get; set; }


    }
}