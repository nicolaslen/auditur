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
            List<BSP_Ticket> lstTickets = oSemana.TicketsBSP.Where(x => x.Concepto.Nombre == "ISSUES").OrderBy(x => x.Compania.Codigo).ThenBy(x => x.NroDocumento).ToList();

            foreach (BSP_Ticket oBSP_Ticket in lstTickets)
            {
                BO_Ticket bo_ticket = oSemana.TicketsBO.Find(x => x.Billete == oBSP_Ticket.NroDocumento && x.Compania.Codigo == oBSP_Ticket.Compania.Codigo);
                if (bo_ticket != null)
                {
                    decimal valorTransaccionBSP = (oBSP_Ticket.Fop == "CC" || oBSP_Ticket.Fop == "CA" ? oBSP_Ticket.ValorTransaccion : 0) + oBSP_Ticket.Detalle
                                                      .Where(x => x.Fop == "CA" || x.Fop == "CC")
                                                      .Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
                    decimal valorTransaccionDif = Math.Round(valorTransaccionBSP - bo_ticket.TotalTransaccion, 2);
                    decimal valorTarifaBsp = oBSP_Ticket.ValorTarifa +
                                             oBSP_Ticket.Detalle.Select(x => x.ValorTarifa).DefaultIfEmpty(0).Sum();
                    decimal valorTarifaDif = Math.Round(valorTarifaBsp - bo_ticket.ValorTarifa, 2);
                    decimal impuestosBsp = (oBSP_Ticket.ImpuestoCodigo != "DL" ? oBSP_Ticket.ImpuestoValor : 0) + oBSP_Ticket.Detalle.Where(x => x.ImpuestoCodigo != "DL").Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
                    
                    decimal impuestosDif = Math.Round(impuestosBsp - bo_ticket.Impuestos, 2);
                    decimal ivaTarifaBsp = (oBSP_Ticket.ImpuestoCodigo == "DL" ? oBSP_Ticket.ImpuestoValor : 0) + oBSP_Ticket.Detalle.Where(x => x.ImpuestoCodigo == "DL").Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
                    decimal ivaTarifaDif = Math.Round(ivaTarifaBsp - bo_ticket.IVATarifa, 2);
                    decimal tycBsp = oBSP_Ticket.ImpuestoTyCValor +
                                     oBSP_Ticket.Detalle.Select(x => x.ImpuestoTyCValor).DefaultIfEmpty(0).Sum();
                    
                    decimal comStdBsp = oBSP_Ticket.ComisionStdValor + oBSP_Ticket.Detalle.Select(x => x.ComisionStdValor).DefaultIfEmpty(0).Sum();
                    decimal comStdDif = comStdBsp - bo_ticket.ComStd;
                    decimal comSuplBsp = oBSP_Ticket.ComisionSuppValor + oBSP_Ticket.Detalle.Select(x => x.ComisionSuppValor).DefaultIfEmpty(0).Sum();
                    decimal comSuplDif = comSuplBsp - bo_ticket.ComSupl;
                    decimal ivaComBsp = oBSP_Ticket.ImpuestoSinComision + oBSP_Ticket.Detalle.Select(x => x.ImpuestoSinComision).DefaultIfEmpty(0).Sum();
                    decimal ivaComDif = ivaComBsp - bo_ticket.IVACom;
                    decimal netoBsp = oBSP_Ticket.NetoAPagar + oBSP_Ticket.Detalle.Select(x => x.NetoAPagar).DefaultIfEmpty(0).Sum();
                    decimal netoDif = netoBsp - bo_ticket.Neto;

                    if (oBSP_Ticket.Moneda != bo_ticket.Moneda || 
                        Math.Abs(valorTransaccionDif) > DiferenciaMinima || 
                        Math.Abs(valorTarifaDif) > DiferenciaMinima ||
                        Math.Abs(impuestosDif) > DiferenciaMinima ||
                        Math.Abs(ivaTarifaDif) > DiferenciaMinima ||
                        Math.Abs(comStdDif) > DiferenciaMinima ||
                        Math.Abs(comSuplDif) > DiferenciaMinima ||
                        Math.Abs(ivaComDif) > DiferenciaMinima)
                    {
                        var oDiferenciaBSP = new Diferencia();
                        oDiferenciaBSP.Origen = "BSP";
                        oDiferenciaBSP.Cia = oBSP_Ticket.Compania.Codigo;
                        oDiferenciaBSP.Trnc = oBSP_Ticket.Trnc;
                        oDiferenciaBSP.RTDN = Validators.ConcatNumbers(oBSP_Ticket.Detalle.Where(x => x.Trnc == "+RTDN:").Select(x => x.NroDocumento.ToString()).FirstOrDefault(), oBSP_Ticket.Detalle.Where(x => x.Trnc == "+RTDN:").Select(x => x.NroDocumento.ToString()).Skip(1).ToList());
                        if (oBSP_Ticket.Detalle.Any(x => x.Trnc == "+RTDN:" && x.Fop == "EX"))
                            oDiferenciaBSP.RTDN += " (EX)";

                        oDiferenciaBSP.BoletoNro = Validators.ConcatNumbers(oBSP_Ticket.NroDocumento.ToString(), oBSP_Ticket.Detalle.Where(x => x.Trnc == "+TKTT").Select(x => x.NroDocumento.ToString()).ToList());
                        oDiferenciaBSP.FechaEmision = AuditurHelpers.GetDateTimeString(oBSP_Ticket.FechaEmision);
                        oDiferenciaBSP.Moneda = oBSP_Ticket.Moneda == Moneda.Peso ? "$" : "D";
                        oDiferenciaBSP.Stat = oBSP_Ticket.Rg == BSP_Rg.Doméstico ? "D" : "I";
                        oDiferenciaBSP.FopCA = (oBSP_Ticket.Fop == "CA" ? oBSP_Ticket.ValorTransaccion : 0) + oBSP_Ticket.Detalle.Where(x => x.Fop == "CA").Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
                        oDiferenciaBSP.FopCC = (oBSP_Ticket.Fop == "CC" ? oBSP_Ticket.ValorTransaccion : 0) + oBSP_Ticket.Detalle.Where(x => x.Fop == "CC").Select(x => x.ValorTransaccion).DefaultIfEmpty(0).Sum();
                        oDiferenciaBSP.TotalTransaccion = valorTransaccionBSP;
                        oDiferenciaBSP.ValorTarifa = valorTarifaBsp;
                        oDiferenciaBSP.Imp = impuestosBsp;
                        oDiferenciaBSP.TyC = tycBsp;
                        oDiferenciaBSP.IVATarifa = ivaTarifaBsp;
                        oDiferenciaBSP.Penalidad = oBSP_Ticket.ImpuestoPenValor + oBSP_Ticket.Detalle.Select(x => x.ImpuestoPenValor).DefaultIfEmpty(0).Sum();
                        oDiferenciaBSP.ComStdValor = comStdBsp;
                        oDiferenciaBSP.ComSuppValor = comSuplBsp;
                        oDiferenciaBSP.IVASinComision = ivaComBsp;
                        oDiferenciaBSP.NetoAPagar = netoBsp;
                        lstDiferencia.Add(oDiferenciaBSP);
                        
                        var oDiferenciaBO = new Diferencia();
                        oDiferenciaBO.Origen = "BO";
                        oDiferenciaBO.Cia = bo_ticket.Compania.Codigo;
                        oDiferenciaBO.BoletoNro = bo_ticket.Billete.ToString();
                        oDiferenciaBO.FechaEmision = AuditurHelpers.GetDateTimeString(bo_ticket.Fecha);
                        oDiferenciaBO.Moneda = bo_ticket.Moneda == Moneda.Peso ? "$" : "D";
                        oDiferenciaBO.FopCA = bo_ticket.CA;
                        oDiferenciaBO.FopCC = bo_ticket.CC;
                        oDiferenciaBO.TotalTransaccion = bo_ticket.TotalTransaccion;
                        oDiferenciaBO.ValorTarifa = bo_ticket.ValorTarifa;
                        oDiferenciaBO.Imp = bo_ticket.Impuestos;
                        oDiferenciaBO.TyC = 0;
                        oDiferenciaBO.IVATarifa = bo_ticket.IVATarifa;
                        oDiferenciaBO.ComStdValor = bo_ticket.ComStd;
                        oDiferenciaBO.ComSuppValor = bo_ticket.ComSupl;
                        oDiferenciaBO.IVASinComision = bo_ticket.IVACom;
                        oDiferenciaBO.NetoAPagar = bo_ticket.Neto;
                        oDiferenciaBO.OperacionNro = bo_ticket.OperacionNro;
                        oDiferenciaBO.Factura = bo_ticket.FacturaNro;
                        oDiferenciaBO.Pasajero = bo_ticket.Pax;
                        lstDiferencia.Add(oDiferenciaBO);

                        var oDiferencia = new Diferencia();
                        oDiferencia.Origen = "DIF";
                        oDiferencia.FopCA = oDiferenciaBSP.FopCA - oDiferenciaBO.FopCA;
                        oDiferencia.FopCC = oDiferenciaBSP.FopCC - oDiferenciaBO.FopCC;
                        oDiferencia.TotalTransaccion = valorTransaccionDif;
                        oDiferencia.ValorTarifa = valorTarifaDif;
                        oDiferencia.Imp = impuestosDif;
                        oDiferencia.TyC = tycBsp;
                        oDiferencia.IVATarifa = ivaTarifaDif;
                        oDiferencia.Penalidad = oDiferenciaBSP.Penalidad - oDiferenciaBO.Penalidad;
                        oDiferencia.ComStdValor = comStdDif;
                        oDiferencia.ComSuppValor = comSuplDif;
                        oDiferencia.IVASinComision = ivaComDif;
                        oDiferencia.NetoAPagar = netoDif;
                        lstDiferencia.Add(oDiferencia);
                    }
                }
            }

            return lstDiferencia;
        }
    }
}
