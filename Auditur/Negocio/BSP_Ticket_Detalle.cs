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
    }
}
