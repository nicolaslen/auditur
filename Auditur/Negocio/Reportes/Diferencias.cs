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
            Diferencia oDiferencia = null;

            foreach (BSP_Ticket oBSP_Ticket in lstTickets)
            {
                BO_Ticket bo_ticket = oSemana.TicketsBO.Find(x => x.Billete == oBSP_Ticket.NroDocumento && x.Compania.Codigo == oBSP_Ticket.Compania.Codigo);
                if (bo_ticket != null)
                {
                    decimal ImpuestosBSP = Math.Round(oBSP_Ticket.Detalle.Sum(x => x.ImpContado + x.ImpCredito) + oBSP_Ticket.IVA105, 2);

                    decimal TarifaDif = Math.Round((oBSP_Ticket.TarContado + oBSP_Ticket.TarCredito) - (bo_ticket.Tarifa), 2);
                    decimal ContadoDif = Math.Round((oBSP_Ticket.TarContado) - (bo_ticket.TarContado), 2);
                    decimal CreditoDif = Math.Round((oBSP_Ticket.TarCredito) - (bo_ticket.TarCredito), 2);
                    decimal ImpuestosDif = Math.Round((ImpuestosBSP) - (bo_ticket.Impuestos), 2);
                    decimal ComisionDif = Math.Round(-(oBSP_Ticket.ComValor + oBSP_Ticket.ComIVA) - (bo_ticket.Comision), 2);

                    if (Math.Abs(TarifaDif) >= DiferenciaMinima || Math.Abs(ContadoDif) >= DiferenciaMinima || Math.Abs(CreditoDif) >= DiferenciaMinima || Math.Abs(ImpuestosDif) >= DiferenciaMinima || Math.Abs(ComisionDif) >= DiferenciaMinima)
                    {
                        oDiferencia = new Diferencia();

                        oDiferencia.BoletoNroBSP = oBSP_Ticket.NroDocumento.ToString();
                        oDiferencia.RgBSP = oBSP_Ticket.Rg == BSP_Rg.Doméstico ? "C" : "I";
                        oDiferencia.TrBSP = oBSP_Ticket.Compania.Codigo;
                        oDiferencia.TarifaBSP = (oBSP_Ticket.TarContado + oBSP_Ticket.TarCredito);
                        oDiferencia.ContadoBSP = oBSP_Ticket.TarContado;
                        oDiferencia.CreditoBSP = oBSP_Ticket.TarCredito;

                        oDiferencia.ImpuestosBSP = ImpuestosBSP;
                        oDiferencia.ComisionBSP = (oBSP_Ticket.ComValor + oBSP_Ticket.ComIVA);
                        oDiferencia.MonedaBSP = oBSP_Ticket.Moneda == Moneda.Peso ? "$" : "D";

                        oDiferencia.TarifaBO = bo_ticket.Tarifa;
                        oDiferencia.ContadoBO = bo_ticket.TarContado;
                        oDiferencia.CreditoBO = bo_ticket.TarCredito;
                        oDiferencia.ImpuestosBO = bo_ticket.Impuestos;
                        oDiferencia.ComisionBO = bo_ticket.Comision;
                        oDiferencia.Factura = bo_ticket.Factura;
                        oDiferencia.Pasajero = bo_ticket.Pasajero;
                        oDiferencia.MonedaBO = bo_ticket.Moneda == Moneda.Peso ? "$" : "D";

                        oDiferencia.Operacion = bo_ticket.Expediente;

                        oDiferencia.TarifaDif = TarifaDif.ToString();
                        oDiferencia.ContadoDif = ContadoDif;
                        oDiferencia.CreditoDif = CreditoDif;
                        oDiferencia.ImpuestosDif = ImpuestosDif;
                        oDiferencia.ComisionDif = ComisionDif;

                        lstDiferencia.Add(oDiferencia);
                    }
                }
            }

            return lstDiferencia;
        }
    }
}
