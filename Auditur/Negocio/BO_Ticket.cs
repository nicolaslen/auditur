using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auditur.Negocio
{
    public class BO_Ticket
    {
        public long ID { get; set; }
        public long IATA { get; set; }
        public Compania Compania { get; set; }
        public long Billete { get; set; }
        public DateTime? Fecha { get; set; }
        public Moneda? Moneda { get; set; }
        public decimal CA { get; set; }
        public decimal CC { get; set; }
        public decimal TotalTransaccion { get; set; }
        public decimal ValorTarifa { get; set; }
        public decimal Impuestos { get; set; }
        public decimal TasasCargos { get; set; }
        public decimal IVATarifa { get; set; }
        public decimal ComStd { get; set; }
        public decimal ComSupl { get; set; }
        public decimal IVACom { get; set; }
        public decimal Neto { get; set; }
        public string Pax { get; set; }
        public string OperacionNro { get; set; }
        public string FacturaNro { get; set; }
        public long SemanaID { get; set; }
    }
}
