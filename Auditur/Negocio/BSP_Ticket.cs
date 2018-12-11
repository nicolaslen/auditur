using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auditur.Negocio
{
    public class BSP_Ticket
    {
        public BSP_Ticket()
        {
            Detalle = new List<BSP_Ticket_Detalle>();
        }

        public long ID { get; set; }
        public long Billete { get; set; }
        public string Tipo { get; set; }
        public DateTime? FechaVenta { get; set; }
        public decimal TarContado { get; set; }
        public decimal TarCredito { get; set; }
        public decimal IVA105 { get; set; }
        public decimal ComPorcentaje { get; set; }
        public decimal ComValor { get; set; }
        public decimal ComOver { get; set; }
        public decimal ComIVA { get; set; }
        public decimal Total { get; set; }
        public BSP_Rg Rg { get; set; }
        public Concepto Concepto { get; set; }
        public Compania Compania { get; set; }
        public long SemanaID { get; set; }
        public Moneda? Moneda { get; set; }

        public List<BSP_Ticket_Detalle> Detalle { get; set; }
    }
}
