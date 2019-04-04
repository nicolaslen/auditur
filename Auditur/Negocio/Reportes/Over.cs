using System.ComponentModel.DataAnnotations;

namespace Auditur.Negocio.Reportes
{
    public class Over
    {
        public string NroDocumento { get; set; }
        public string Fecha { get; set; }
        public string Tr { get; set; }

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

        public string Factura { get; set; }
        public string Pasajero { get; set; }
        public string Observaciones { get; set; }

        [Display(Name = "Op. Nº")]
        public string Operacion { get; set; }
    }
}