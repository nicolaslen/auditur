using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helpers;

namespace Auditur.Negocio.Reportes
{
    public class DiferenciasIVAs : IReport<DiferenciasIVA>
    {
        private const decimal DiferenciaMinima = 0.5M;

        public List<DiferenciasIVA> Generar(Semana oSemana)
        {
            List<DiferenciasIVA> lstDiferenciasIVA = new List<DiferenciasIVA>();

            List<BSP_Ticket> lstTickets = oSemana.TicketsBSP.Where(x => x.Concepto.Nombre == "ISSUES" && x.Rg == BSP_Rg.Doméstico).OrderBy(x => x.Compania.Codigo).ThenBy(x => x.NroDocumento).ToList();
            foreach (BSP_Ticket oBSP_Ticket in lstTickets)
            {
                BO_Ticket bo_ticket = oSemana.TicketsBO.Find(x =>
                    x.Billete == oBSP_Ticket.NroDocumento && x.Compania.Codigo == oBSP_Ticket.Compania.Codigo);

                if (bo_ticket != null)
                {
                    decimal valorTarifaBsp = oBSP_Ticket.ValorTarifa +
                                             oBSP_Ticket.Detalle.Select(x => x.ValorTarifa).DefaultIfEmpty(0).Sum();
                    decimal valorTarifaDif = Math.Round(valorTarifaBsp - bo_ticket.ValorTarifa, 2);

                    decimal ivaTarifaBsp = (oBSP_Ticket.ImpuestoCodigo == "DL" ? oBSP_Ticket.ImpuestoValor : 0) +
                                           oBSP_Ticket.Detalle.Where(x => x.ImpuestoCodigo == "DL")
                                               .Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
                    decimal ivaTarifaDif = Math.Round(ivaTarifaBsp - bo_ticket.IVACom, 2);

                    decimal comStdBsp = oBSP_Ticket.ComisionStdValor +
                                        oBSP_Ticket.Detalle.Select(x => x.ComisionStdValor).DefaultIfEmpty(0).Sum();
                    decimal comStdDif = comStdBsp - bo_ticket.ComStd;
                    decimal comSuplBsp = oBSP_Ticket.ComisionSuppValor +
                                         oBSP_Ticket.Detalle.Select(x => x.ComisionSuppValor).DefaultIfEmpty(0).Sum();
                    decimal comSuplDif = comSuplBsp - bo_ticket.ComSupl;
                    decimal ivaComBsp = oBSP_Ticket.ImpuestoSinComision +
                                        oBSP_Ticket.Detalle.Select(x => x.ImpuestoSinComision).DefaultIfEmpty(0).Sum();
                    decimal ivaComDif = ivaComBsp - bo_ticket.IVACom;

                    if (oBSP_Ticket.Moneda != bo_ticket.Moneda ||
                        Math.Abs(valorTarifaDif) >= DiferenciaMinima ||
                        Math.Abs(ivaTarifaDif) >= DiferenciaMinima ||
                        Math.Abs(comStdDif) >= DiferenciaMinima ||
                        Math.Abs(comSuplDif) >= DiferenciaMinima ||
                        Math.Abs(ivaComDif) >= DiferenciaMinima)
                    {
                        var oDiferenciaBSP = new DiferenciasIVA();

                        oDiferenciaBSP.Cia = oBSP_Ticket.Compania.Codigo;
                        oDiferenciaBSP.BoletoNro = Validators.ConcatNumbers(oBSP_Ticket.NroDocumento.ToString(),
                            oBSP_Ticket.Detalle.Where(x => x.Trnc == "+TKTT").Select(x => x.NroDocumento.ToString())
                                .ToList());
                        oDiferenciaBSP.FechaEmision = AuditurHelpers.GetDateTimeString(oBSP_Ticket.FechaEmision);
                        oDiferenciaBSP.Moneda = oBSP_Ticket.Moneda == Moneda.Peso ? "$" : "D";
                        oDiferenciaBSP.Stat = oBSP_Ticket.Rg == BSP_Rg.Doméstico ? "D" : "I";
                        oDiferenciaBSP.ValorTarifa = valorTarifaBsp;
                        oDiferenciaBSP.IVATarifa = ivaTarifaBsp;
                        oDiferenciaBSP.ComStdValor = comStdBsp;
                        oDiferenciaBSP.ComSuppValor = comSuplBsp;
                        lstDiferenciasIVA.Add(oDiferenciaBSP);

                        var oDiferenciaBO = new DiferenciasIVA();
                        oDiferenciaBO.Cia = bo_ticket.Compania.Codigo;
                        oDiferenciaBO.BoletoNro = bo_ticket.Billete.ToString();
                        oDiferenciaBO.FechaEmision = AuditurHelpers.GetDateTimeString(bo_ticket.Fecha);
                        oDiferenciaBO.Moneda = bo_ticket.Moneda == Moneda.Peso ? "$" : "D";
                        oDiferenciaBO.ValorTarifa = bo_ticket.ValorTarifa;
                        oDiferenciaBO.IVATarifa = bo_ticket.IVATarifa;
                        oDiferenciaBO.ComStdValor = bo_ticket.ComStd;
                        oDiferenciaBO.ComSuppValor = bo_ticket.ComSupl;
                        oDiferenciaBO.OperacionNro = bo_ticket.OperacionNro;
                        oDiferenciaBO.Factura = bo_ticket.FacturaNro;
                        oDiferenciaBO.Pasajero = bo_ticket.Pax;
                        lstDiferenciasIVA.Add(oDiferenciaBO);

                        var oDiferencia = new DiferenciasIVA();
                        oDiferencia.ValorTarifa = valorTarifaDif;
                        oDiferencia.IVATarifa = ivaTarifaDif;
                        oDiferencia.ComStdValor = comStdDif;
                        oDiferencia.ComSuppValor = comSuplDif;
                        lstDiferenciasIVA.Add(oDiferencia);
                    }
                }
            }

            return lstDiferenciasIVA;
        }
    }
}
