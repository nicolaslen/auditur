using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auditur.Negocio
{
    public class Semana
    {
        public Semana()
        {
            TicketsBSP = new List<BSP_Ticket>();
            TicketsBO = new List<BO_Ticket>();
        }

        public long ID { get; set; }
        public DateTime Periodo { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public Agencia Agencia { get; set; }
        public bool BSPCargado { get; set; }
        public bool BOCargado { get; set; }
        public List<BSP_Ticket> TicketsBSP { get; set; }
        public List<BO_Ticket> TicketsBO { get; set; }

    }
}
