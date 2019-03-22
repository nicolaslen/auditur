using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auditur.Negocio
{
    public class BSP_Ticket_Detalle
    {
        public long ID { get; set; }
        public string ISO  { get; set; }
        public decimal ImpContado { get; set; }
        public decimal ImpCredito { get; set; }
        public decimal IVA21 { get; set; }
        public string Observaciones { get; set; }
        public long TicketID { get; set; }

        public long Billete { get; set; }
        public DateTime? FechaVenta { get; set; }
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
