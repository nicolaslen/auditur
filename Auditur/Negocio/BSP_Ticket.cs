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
        public DateTime? FechaEmision { get; set; }
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

        public string TRNC { get; set; }
        public string CPN { get; set; }
        public string Stat { get; set; }
        public string Fop { get; set; }
        public decimal ValorTransaccion { get; set; }
        public decimal ValorTarifa { get; set; }
        public decimal ImpuestoValor { get; set; }
        public string ImpuestoCodigo { get; set; }
        public decimal ImpuestoTyCValor { get; set; }
        public string ImpuestoTyCCodigo { get; set; }
        public decimal ImpuestoPenValor { get; set; }
        public string ImpuestoPenCodigo { get; set; }
        public decimal ImpuestoCobl { get; set; }
        public decimal ComisionStdPorcentaje { get; set; }
        public decimal ComisionStdValor { get; set; }
        public decimal ComisionSuppPorcentaje { get; set; }
        public decimal ComisionSuppValor { get; set; }
        public decimal ImpuestoSinComision { get; set; }
        public decimal NetoAPagar { get; set; }
        public string NR { get; set; }
    }
}
