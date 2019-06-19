using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helpers;

namespace Auditur.Negocio.Reportes
{
    public class Diferencias : IReport<Diferencia>
    {
        private const decimal DiferenciaMinima = 1;

        public List<Diferencia> Generar(Semana oSemana)
        {
            List<Diferencia> lstDiferencia = new List<Diferencia>();
            List<BSP_Ticket> lstTickets = oSemana.TicketsBSP.Where(x => x.Concepto.Tipo == 'B').OrderBy(x => x.Compania.Codigo).ThenBy(x => x.NroDocumento).ToList();

            foreach (BSP_Ticket oBSP_Ticket in lstTickets)
            {
                BO_Ticket bo_ticket = oSemana.TicketsBO.Find(x => x.Billete == oBSP_Ticket.NroDocumento && x.Compania.Codigo == oBSP_Ticket.Compania.Codigo);
                if (bo_ticket != null)
                {
                    //TODO: Qué tickets pongo en Diferencias? Los campos se llenan igual que en Créditos?
                    /*decimal ImpuestosBSP = Math.Round(oBSP_Ticket.Detalle.Sum(x => x.ImpContado + x.ImpCredito) + oBSP_Ticket.IVA105, 2);

                    decimal TarifaDif = Math.Round((oBSP_Ticket.TarContado + oBSP_Ticket.TarCredito) - (bo_ticket.Tarifa), 2);
                    decimal ContadoDif = Math.Round((oBSP_Ticket.TarContado) - (bo_ticket.TarContado), 2);
                    decimal CreditoDif = Math.Round((oBSP_Ticket.TarCredito) - (bo_ticket.TarCredito), 2);
                    decimal ImpuestosDif = Math.Round((ImpuestosBSP) - (bo_ticket.Impuestos), 2);
                    decimal ComisionDif = Math.Round(-(oBSP_Ticket.ComValor + oBSP_Ticket.ComIVA) - (bo_ticket.Comision), 2);

                    if (Math.Abs(TarifaDif) >= DiferenciaMinima || Math.Abs(ContadoDif) >= DiferenciaMinima || Math.Abs(CreditoDif) >= DiferenciaMinima || Math.Abs(ImpuestosDif) >= DiferenciaMinima || Math.Abs(ComisionDif) >= DiferenciaMinima)
                    {*/
                        var oDiferencia = new Diferencia();
                        
                        oDiferencia.Cia = oBSP_Ticket.Compania.Codigo;
                        oDiferencia.Tipo = oBSP_Ticket.Trnc;
                        oDiferencia.RTDN = Validators.ConcatNumbers(oBSP_Ticket.Detalle.Where(x => x.Trnc == "+RTDN:").Select(x => x.NroDocumento.ToString()).FirstOrDefault(), oBSP_Ticket.Detalle.Where(x => x.Trnc == "+RTDN:").Select(x => x.NroDocumento.ToString()).Skip(1).ToList());
                        if (oBSP_Ticket.Detalle.Any(x => x.Trnc == "+RTDN:" && x.Fop == "EX"))
                            oDiferencia.RTDN += " (EX)";

                        oDiferencia.BoletoNro = Validators.ConcatNumbers(oBSP_Ticket.NroDocumento.ToString(), oBSP_Ticket.Detalle.Where(x => x.Trnc == "+TKTT").Select(x => x.NroDocumento.ToString()).ToList());
                        oDiferencia.FechaEmision = AuditurHelpers.GetDateTimeString(oBSP_Ticket.FechaEmision);
                        oDiferencia.Moneda = oBSP_Ticket.Moneda == Moneda.Peso ? "$" : "D";
                        oDiferencia.TourCode = oBSP_Ticket.Tour;
                        oDiferencia.CodNr = oBSP_Ticket.Nr;
                        oDiferencia.Stat = oBSP_Ticket.Rg == BSP_Rg.Doméstico ? "D" : "I";
                        oDiferencia.FopCA = (oBSP_Ticket.Fop == "CA" ? oBSP_Ticket.ValorTransaccion : 0) + oBSP_Ticket.Detalle.Where(x => x.Fop == "CA").Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
                        oDiferencia.FopCC = (oBSP_Ticket.Fop == "CC" ? oBSP_Ticket.ValorTransaccion : 0) + oBSP_Ticket.Detalle.Where(x => x.Fop == "CC").Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
                        oDiferencia.TotalTransaccion = (oBSP_Ticket.Fop == "CC" || oBSP_Ticket.Fop == "CA" ? oBSP_Ticket.ValorTransaccion : 0) + oBSP_Ticket.Detalle.Where(x => x.Fop == "CC" || x.Fop == "CA").Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
                        oDiferencia.ValorTarifa = oBSP_Ticket.ValorTarifa + oBSP_Ticket.Detalle.Select(x => x.ValorTarifa).DefaultIfEmpty(0).Sum();
                        oDiferencia.Imp = oBSP_Ticket.ImpuestoValor + oBSP_Ticket.Detalle.Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
                        oDiferencia.TyC = oBSP_Ticket.ImpuestoTyCValor + oBSP_Ticket.Detalle.Select(x => x.ImpuestoTyCValor).DefaultIfEmpty(0).Sum();
                        oDiferencia.IVATarifa = (oBSP_Ticket.ImpuestoCodigo == "DL" ? oBSP_Ticket.ImpuestoValor : 0) +
                                              oBSP_Ticket.Detalle.Where(x => x.ImpuestoCodigo == "DL")
                                                  .Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
                        oDiferencia.Penalidad = oBSP_Ticket.ImpuestoPenValor + oBSP_Ticket.Detalle.Select(x => x.ImpuestoPenValor).DefaultIfEmpty(0).Sum();
                        oDiferencia.Cobl = oBSP_Ticket.ImpuestoCobl + oBSP_Ticket.Detalle.Select(x => x.ImpuestoCobl).DefaultIfEmpty(0).Sum();
                        oDiferencia.ComStdValor = oBSP_Ticket.ComisionStdValor + oBSP_Ticket.Detalle.Select(x => x.ComisionStdValor).DefaultIfEmpty(0).Sum();
                        oDiferencia.ComSuppValor = oBSP_Ticket.ComisionSuppValor + oBSP_Ticket.Detalle.Select(x => x.ComisionSuppValor).DefaultIfEmpty(0).Sum();
                        oDiferencia.IVASinComision = oBSP_Ticket.ImpuestoSinComision + oBSP_Ticket.Detalle.Select(x => x.ImpuestoSinComision).DefaultIfEmpty(0).Sum();
                        oDiferencia.NetoAPagar = oBSP_Ticket.NetoAPagar + oBSP_Ticket.Detalle.Select(x => x.NetoAPagar).DefaultIfEmpty(0).Sum();


                        lstDiferencia.Add(oDiferencia);
                    //}
                }
            }

            return lstDiferencia;
        }
    }
}
