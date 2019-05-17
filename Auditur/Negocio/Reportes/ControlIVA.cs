using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Auditur.Negocio.Reportes
{
    public class ControlIVA
    {
        [Display(Name = "NroDocumento Nro")]
        public string BoletoNroBSP { get; set; }
        [Display(Name = "Rg")]
        public string RgBSP { get; set; }
        [Display(Name = "Tr")]
        public string TrBSP { get; set; }
        [Display(Name = "Tr ")]
        public string Tr2BSP { get; set; }
        [Display(Name = "Tarifa")]
        public decimal TarifaBSP { get; set; }
        [Display(Name = "IVA Tarifa")]
        public decimal IVATarifaBSP { get; set; }
        [Display(Name = "Comisión")]
        public decimal ComisionBSP { get; set; }
        [Display(Name = "IVA Comis")]
        public decimal IVAComisionBSP { get; set; }
        [Display(Name = "Moneda")]
        public string MonedaBSP { get; set; }
        [Display(Name = "Fecha")]
        public string FechaBSP { get; set; }

        [Display(Name = "Tr  ")]
        public string TrBO { get; set; }
        [Display(Name = "Nro Ticket")]
        public string NroTicketBO { get; set; }
        [Display(Name = "Tarifa ")]
        public decimal TarifaBO { get; set; }
        [Display(Name = "IVA Tarifa ")]
        public decimal IVATarifaBO { get; set; }
        [Display(Name = "Comisión ")]
        public decimal ComisionBO { get; set; }
        [Display(Name = "IVA Comis ")]
        public decimal IVAComisionBO { get; set; }
        [Display(Name = "RTDN.")]
        public string Referencia { get; set; }
        [Display(Name = "Factura")]
        public string Factura { get; set; }
        [Display(Name = "Pasajero")]
        public string Pasajero { get; set; }

        [Display(Name = "Tarifa  ")]
        public decimal TarifaDif { get; set; }
        [Display(Name = "IVA Tarifa  ")]
        public decimal IVATarifaDif { get; set; }
        [Display(Name = "Comisión  ")]
        public decimal ComisionDif { get; set; }
        [Display(Name = "IVA Comis  ")]
        public decimal IVAComisionDif { get; set; }
    }
}
