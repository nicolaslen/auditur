using Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Auditur.Negocio.Reportes
{
    public class Creditos : IReport<Credito>
    {
        public List<Credito> Generar(Semana oSemana)
        {
            List<Credito> lstCredito = new List<Credito>();

            List<BSP_Ticket> lstTickets = oSemana.TicketsBSP.Where(x => x.Concepto.Nombre == "CREDIT MEMOS").OrderBy(x => x.Compania.Codigo).ThenBy(x => x.Moneda).ThenBy(x => x.NroDocumento).ToList();

            List<Credito> lstCreditoPesos = new List<Credito>();
            lstTickets.Where(x => x.Moneda == Moneda.Peso).ToList().ForEach(x => lstCreditoPesos.Add(GetCredito(x)));
            if (lstCreditoPesos.Count > 0)
            {
                lstCreditoPesos.Add(new Credito { Cia = "TOTAL", FopCA = lstCreditoPesos.Sum(x => x.FopCA), FopCC = lstCreditoPesos.Sum(x => x.FopCC), TotalTransaccion = lstCreditoPesos.Sum(x => x.TotalTransaccion), ValorTarifa = lstCreditoPesos.Sum(x => x.ValorTarifa), Imp = lstCreditoPesos.Sum(x => x.Imp), TyC = lstCreditoPesos.Sum(x => x.TyC), IVATarifa = lstCreditoPesos.Sum(x => x.IVATarifa), Penalidad = lstCreditoPesos.Sum(x => x.Penalidad), ComStdValor = lstCreditoPesos.Sum(x => x.ComStdValor), ComSuppValor = lstCreditoPesos.Sum(x => x.ComStdValor), IVASinComision = lstCreditoPesos.Sum(x => x.IVASinComision), NetoAPagar = lstCreditoPesos.Sum(x => x.NetoAPagar) });
                lstCredito.AddRange(lstCreditoPesos);
            }

            List<Credito> lstCreditoDolares = new List<Credito>();
            lstTickets.Where(x => x.Moneda == Moneda.Dolar).ToList().ForEach(x => lstCreditoDolares.Add(GetCredito(x)));
            if (lstCreditoDolares.Count > 0)
            {
                lstCreditoDolares.Add(new Credito { Cia = "TOTAL", FopCA = lstCreditoDolares.Sum(x => x.FopCA), FopCC = lstCreditoDolares.Sum(x => x.FopCC), TotalTransaccion = lstCreditoDolares.Sum(x => x.TotalTransaccion), ValorTarifa = lstCreditoDolares.Sum(x => x.ValorTarifa), Imp = lstCreditoDolares.Sum(x => x.Imp), TyC = lstCreditoDolares.Sum(x => x.TyC), IVATarifa = lstCreditoDolares.Sum(x => x.IVATarifa), Penalidad = lstCreditoDolares.Sum(x => x.Penalidad), ComStdValor = lstCreditoDolares.Sum(x => x.ComStdValor), ComSuppValor = lstCreditoDolares.Sum(x => x.ComStdValor), IVASinComision = lstCreditoDolares.Sum(x => x.IVASinComision), NetoAPagar = lstCreditoDolares.Sum(x => x.NetoAPagar) });
                lstCredito.AddRange(lstCreditoDolares);
            }

            return lstCredito;
        }

        private Credito GetCredito(BSP_Ticket oBSP_Ticket)
        {
            Credito oCredito = new Credito();

            oCredito.Cia = oBSP_Ticket.Compania.Codigo;
            oCredito.Tipo = oBSP_Ticket.Trnc;

            oCredito.RTDN = Validators.ConcatNumbers(oBSP_Ticket.Detalle.Where(x => x.Trnc == "+RTDN:").Select(x => x.NroDocumento.ToString()).FirstOrDefault(), oBSP_Ticket.Detalle.Where(x => x.Trnc == "+RTDN:").Select(x => x.NroDocumento.ToString()).Skip(1).ToList());
            if (oBSP_Ticket.Detalle.Any(x => x.Trnc == "+RTDN:" && x.Fop == "EX"))
                oCredito.RTDN += " (EX)";

            oCredito.NroDocumento = oBSP_Ticket.NroDocumento.ToString();
            oCredito.FechaEmision = AuditurHelpers.GetDateTimeString(oBSP_Ticket.FechaEmision);
            oCredito.Moneda = oBSP_Ticket.Moneda == Moneda.Peso ? "$" : "D";
            oCredito.FopCA = (oBSP_Ticket.Fop == "CA" ? oBSP_Ticket.ValorTransaccion : 0);
            oCredito.FopCC = (oBSP_Ticket.Fop == "CC" ? oBSP_Ticket.ValorTransaccion : 0);
            oCredito.TotalTransaccion =
                (oBSP_Ticket.Fop == "CC" || oBSP_Ticket.Fop == "CA" ? oBSP_Ticket.ValorTransaccion : 0);
            oCredito.ValorTarifa = oBSP_Ticket.ValorTarifa;
            oCredito.Imp = (oBSP_Ticket.ImpuestoCodigo != "DL" && oBSP_Ticket.ImpuestoCodigo != "O5" ? oBSP_Ticket.ImpuestoValor : 0)
                                 + oBSP_Ticket.Detalle.Where(x => x.ImpuestoCodigo != "DL" && x.ImpuestoCodigo != "O5").Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
            oCredito.Imp30 = (oBSP_Ticket.ImpuestoCodigo == "O5" ? oBSP_Ticket.ImpuestoValor : 0) + oBSP_Ticket.Detalle.Where(x => x.ImpuestoCodigo == "O5").Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
            oCredito.TyC = oBSP_Ticket.ImpuestoTyCValor;
            oCredito.IVATarifa = (oBSP_Ticket.ImpuestoCodigo == "DL" ? oBSP_Ticket.ImpuestoValor : 0)
                                   + oBSP_Ticket.Detalle.Where(x => x.ImpuestoCodigo == "DL").Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
            oCredito.Penalidad = oBSP_Ticket.ImpuestoPenValor;
            oCredito.ComStdValor = oBSP_Ticket.ComisionStdValor;
            oCredito.ComSuppValor = oBSP_Ticket.ComisionSuppValor;
            oCredito.IVASinComision = oBSP_Ticket.ImpuestoSinComision;
            oCredito.NetoAPagar = oBSP_Ticket.NetoAPagar;
            oCredito.Observaciones = oBSP_Ticket.Observaciones?.Replace("|", "\n");
            return oCredito;
        }
    }
}