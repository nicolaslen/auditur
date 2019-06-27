using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Auditur.Negocio.Reportes
{
    public class SituacionBSP
    {
        [Display(Name = "Cia")]
        public string Cia { get; set; }

        [Display(Name = "TRNC")]
        public string Tipo { get; set; }

        [Display(Name = "Nro. Docto")]
        public string BoletoNro { get; set; }

        [Display(Name = "RTDN")]
        public string Ref { get; set; }

        [Display(Name = "Fecha Emis.")]
        public string FechaEmision { get; set; }

        [Display(Name = "$/D")]
        public string Moneda { get; set; }

        [Display(Name = "Tour Code")]
        public string TourCode { get; set; }

        [Display(Name = "Net Remit")]
        public string CodNr { get; set; }

        [Display(Name = "Destino I/D")]
        public string Stat { get; set; }

        [Display(Name = "Fop CA")]
        public decimal FopCA { get; set; }

        [Display(Name = "Fop CC")]
        public decimal FopCC { get; set; }

        [Display(Name = "Total Transac")]
        public decimal TotalTransaccion { get; set; }

        [Display(Name = "Valor Tarifa")]
        public decimal ValorTarifa { get; set; }

        [Display(Name = "Imp")]
        public decimal Imp { get; set; }

        [Display(Name = "T&C")]
        public decimal TyC { get; set; }

        [Display(Name = "IVA Tarifa")]
        public decimal IVATarifa { get; set; }

        [Display(Name = "Pen")]
        public decimal Penalidad { get; set; }

        [Display(Name = "Cobl")]
        public decimal Cobl { get; set; }

        [Display(Name = "ComStdValor")]
        public decimal ComStdValor { get; set; }

        [Display(Name = "ComSuplValor")]
        public decimal ComSuppValor { get; set; }

        [Display(Name = "IVA Com")]
        public decimal IVASinComision { get; set; }

        [Display(Name = "NetoAPagar")]
        public decimal NetoAPagar { get; set; }

        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }
    }
}
