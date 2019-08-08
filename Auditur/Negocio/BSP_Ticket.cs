using System;
using System.Collections.Generic;

namespace Auditur.Negocio
{
    public class BSP_Ticket
    {
        public BSP_Ticket()
        {
            Detalle = new List<BSP_Ticket_Detalle>();
        }

        public Concepto Concepto { get; set; }
        public long SemanaID { get; set; }
        public Moneda Moneda { get; set; }
        public BSP_Rg Rg { get; set; }

        public long ID { get; set; }
        public Compania Compania { get; set; }
        public string Trnc { get; set; }
        public long NroDocumento { get; set; }
        public DateTime? FechaEmision { get; set; }
        public string Cpn { get; set; }
        public string Nr { get; set; }
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
        public string Tour { get; set; }
        public string Esac { get; set; }
        public string Observaciones { get; set; }
        public List<BSP_Ticket_Detalle> Detalle { get; set; }   
    }
}