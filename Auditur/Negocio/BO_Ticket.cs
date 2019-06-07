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
        public bool Offline { get; set; }
        public Compania Compania { get; set; }
        public decimal Tarifa { get; set; }
        public decimal Cash { get; set; }
        public decimal Tarjeta { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal Over { get; set; }
        public string File { get; set; }
        public decimal IVA { get; set; }
        public decimal IVAComision { get; set; }
        public decimal Neto { get; set; }
        public decimal Subtotal { get; set; }
        public string Factura { get; set; }
        public string Pasajero { get; set; }
        public string Vendedor { get; set; }
        public long SemanaID { get; set; }
        public Moneda? Moneda { get; set; }
        public decimal APagar { get; set; }
        public string TourCode { get; set; }
        public string Marco { get; set; }
        public string NroTarjeta { get; set; }
    }
}
