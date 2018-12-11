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
        public long Billete { get; set; }
        public DateTime? Fecha { get; set; }
        public bool Void { get; set; }
        public Compania Compania { get; set; }
        public decimal Tarifa { get; set; }
        public decimal TarContado { get; set; }
        public decimal TarCredito { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Comision { get; set; }
        public decimal ComOver { get; set; }
        public string Expediente { get; set; }
        public decimal IVA105 { get; set; }
        public decimal IVAComision { get; set; }
        public decimal ComValor { get; set; }
        public decimal Total { get; set; }
        public string Factura { get; set; }
        public string Pasajero { get; set; }
        public string Vendedor { get; set; }
        public long SemanaID { get; set; }
        public Moneda? Moneda { get; set; }
    }
}
