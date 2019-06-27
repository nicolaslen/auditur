﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helpers;

namespace Auditur.Negocio.Reportes
{
    public class Debitos : IReport<Debito>
    {
        public List<Debito> Generar(Semana oSemana)
        {
            List<Debito> lstDebito = new List<Debito>();
            
            List<BSP_Ticket> lstTickets = oSemana.TicketsBSP.Where(x => x.Concepto.Nombre == "DEBIT MEMOS").OrderBy(x => x.Compania.Codigo).ThenBy(x => x.Moneda).ThenBy(x => x.NroDocumento).ToList();

            List<Debito> lstDebitoPesos = new List<Debito>();
            lstTickets.Where(x => x.Moneda == Moneda.Peso).ToList().ForEach(x => lstDebitoPesos.Add(GetDebito(x)));
            if (lstDebitoPesos.Count > 0)
            {
                lstDebitoPesos.Add(new Debito { Cia = "TOTAL", NetoAPagar = lstDebitoPesos.Sum(x => x.NetoAPagar) });
                lstDebito.AddRange(lstDebitoPesos);
            }

            List<Debito> lstDebitoDolares = new List<Debito>();
            lstTickets.Where(x => x.Moneda == Moneda.Dolar).ToList().ForEach(x => lstDebitoDolares.Add(GetDebito(x)));
            if (lstDebitoDolares.Count > 0)
            {
                lstDebitoDolares.Add(new Debito { Cia = "TOTAL", NetoAPagar = lstDebitoDolares.Sum(x => x.NetoAPagar) });
                lstDebito.AddRange(lstDebitoDolares);
            }

            return lstDebito;
        }

        private Debito GetDebito(BSP_Ticket oBSP_Ticket)
        {
            Debito oDebito = new Debito();

            oDebito.Cia = oBSP_Ticket.Compania.Codigo;
            oDebito.Tipo = oBSP_Ticket.Trnc;

            oDebito.RTDN = Validators.ConcatNumbers(oBSP_Ticket.Detalle.Where(x => x.Trnc == "+RTDN:").Select(x => x.NroDocumento.ToString()).FirstOrDefault(), oBSP_Ticket.Detalle.Where(x => x.Trnc == "+RTDN:").Select(x => x.NroDocumento.ToString()).Skip(1).ToList());
            if (oBSP_Ticket.Detalle.Any(x => x.Trnc == "+RTDN:" && x.Fop == "EX"))
                oDebito.RTDN += " (EX)";

            oDebito.NroDocumento = oBSP_Ticket.NroDocumento.ToString();
            oDebito.FechaEmision = AuditurHelpers.GetDateTimeString(oBSP_Ticket.FechaEmision);
            oDebito.Moneda = oBSP_Ticket.Moneda == Moneda.Peso ? "$" : "D";
            oDebito.Stat = oBSP_Ticket.Rg == BSP_Rg.Doméstico ? "D" : "I";
            oDebito.FopCA = (oBSP_Ticket.Fop == "CA" ? oBSP_Ticket.ValorTransaccion : 0);
            oDebito.FopCC = (oBSP_Ticket.Fop == "CC" ? oBSP_Ticket.ValorTransaccion : 0);
            oDebito.TotalTransaccion =
                (oBSP_Ticket.Fop == "CC" || oBSP_Ticket.Fop == "CA" ? oBSP_Ticket.ValorTransaccion : 0);
            oDebito.ValorTarifa = oBSP_Ticket.ValorTarifa;
            oDebito.Imp = oBSP_Ticket.ImpuestoValor;
            oDebito.TyC = oBSP_Ticket.ImpuestoTyCValor;
            oDebito.IVATarifa = (oBSP_Ticket.ImpuestoCodigo == "DL" ? oBSP_Ticket.ImpuestoValor : 0);
            oDebito.Penalidad = oBSP_Ticket.ImpuestoPenValor;
            oDebito.Cobl = oBSP_Ticket.ImpuestoCobl;
            oDebito.ComStdValor = oBSP_Ticket.ComisionStdValor;
            oDebito.ComSuppValor = oBSP_Ticket.ComisionSuppValor;
            oDebito.IVAComision = oBSP_Ticket.ImpuestoSinComision;
            oDebito.NetoAPagar = oBSP_Ticket.NetoAPagar;
            return oDebito;
        }
    }
}
