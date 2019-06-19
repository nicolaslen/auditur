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
            //TODO: Qué tickets pongo en Diferencias IVA? Los campos se llenan igual que en Créditos?

            List<char> lstTipoConceptoPermitidos = new List<char>();
            lstTipoConceptoPermitidos.Add('B'); //Billetes
            lstTipoConceptoPermitidos.Add('R'); //Reembolsos
            
            DiferenciasIVA oDiferencia = null;

            //List<BO_Ticket> lstTicketsCargados = new List<BO_Ticket>();

            List<BSP_Ticket> lstTickets = oSemana.TicketsBSP.Where(x => lstTipoConceptoPermitidos.Contains(x.Concepto.Tipo) && x.Rg == BSP_Rg.Doméstico).OrderBy(x => x.Compania.Codigo).ThenBy(x => x.NroDocumento).ToList();
            foreach (BSP_Ticket oBSP_Ticket in lstTickets)
            {
                BO_Ticket bo_ticket = oSemana.TicketsBO.Find(x => x.Billete == oBSP_Ticket.NroDocumento && x.Compania.Codigo == oBSP_Ticket.Compania.Codigo);

                oDiferencia = new DiferenciasIVA();

                /*oDiferencia.TarifaBSP = Math.Abs(oBSP_Ticket.TarContado + oBSP_Ticket.TarCredito);
                oDiferencia.IVATarifaBSP = Math.Abs(oBSP_Ticket.IVA105);
                oDiferencia.ComisionBSP = Math.Abs(oBSP_Ticket.ComValor + oBSP_Ticket.ComOver);
                oDiferencia.IVAComisionBSP = Math.Abs(oBSP_Ticket.ComIVA);

                if (bo_ticket != null)
                {
                    oDiferencia.TarifaBO = Math.Abs(bo_ticket.Tarifa);
                    oDiferencia.IVATarifaBO = Math.Abs(bo_ticket.IVA105);
                    oDiferencia.ComisionBO = Math.Abs(bo_ticket.ComValor + bo_ticket.ComOver);
                    oDiferencia.IVAComisionBO = Math.Abs(bo_ticket.IVAComision);
                }

                oDiferencia.TarifaDif = oDiferencia.TarifaBSP - oDiferencia.TarifaBO;
                oDiferencia.IVATarifaDif = oDiferencia.IVATarifaBSP - oDiferencia.IVATarifaBO;
                oDiferencia.ComisionDif = oDiferencia.ComisionBSP - oDiferencia.ComisionBO;
                oDiferencia.IVAComisionDif = oDiferencia.IVAComisionBSP - oDiferencia.IVAComisionBO;

                if (Math.Abs(oDiferencia.TarifaDif) > DiferenciaMinima || Math.Abs(oDiferencia.IVATarifaDif) > DiferenciaMinima || Math.Abs(oDiferencia.ComisionDif) > DiferenciaMinima || Math.Abs(oDiferencia.IVAComisionDif) > DiferenciaMinima)
                {*/
                    oDiferencia.Cia = oBSP_Ticket.Compania.Codigo;
                    oDiferencia.BoletoNro = Validators.ConcatNumbers(oBSP_Ticket.NroDocumento.ToString(), oBSP_Ticket.Detalle.Where(x => x.Trnc == "+TKTT").Select(x => x.NroDocumento.ToString()).ToList());
                    oDiferencia.FechaEmision = AuditurHelpers.GetDateTimeString(oBSP_Ticket.FechaEmision);
                    oDiferencia.Moneda = oBSP_Ticket.Moneda == Moneda.Peso ? "$" : "D";
                    oDiferencia.Stat = oBSP_Ticket.Rg == BSP_Rg.Doméstico ? "D" : "I";
                    oDiferencia.ValorTarifa = oBSP_Ticket.ValorTarifa + oBSP_Ticket.Detalle.Select(x => x.ValorTarifa).DefaultIfEmpty(0).Sum();
                    oDiferencia.IVATarifa = (oBSP_Ticket.ImpuestoCodigo == "DL" ? oBSP_Ticket.ImpuestoValor : 0) +
                                            oBSP_Ticket.Detalle.Where(x => x.ImpuestoCodigo == "DL")
                                                .Select(x => x.ImpuestoValor).DefaultIfEmpty(0).Sum();
                    oDiferencia.ComStdValor = oBSP_Ticket.ComisionStdValor + oBSP_Ticket.Detalle.Select(x => x.ComisionStdValor).DefaultIfEmpty(0).Sum();
                    oDiferencia.ComSuppValor = oBSP_Ticket.ComisionSuppValor + oBSP_Ticket.Detalle.Select(x => x.ComisionSuppValor).DefaultIfEmpty(0).Sum();
                    oDiferencia.IVAComision = oBSP_Ticket.ImpuestoSinComision + oBSP_Ticket.Detalle.Select(x => x.ImpuestoSinComision).DefaultIfEmpty(0).Sum();

                    if (bo_ticket != null)
                    {
                        oDiferencia.OperacionNro = bo_ticket.OperacionNro;
                        oDiferencia.Factura = bo_ticket.FacturaNro;
                        oDiferencia.Pasajero = bo_ticket.Pax;
                    }

                    lstDiferenciasIVA.Add(oDiferencia);
                //}
            }

            return lstDiferenciasIVA;
        }
    }
}
