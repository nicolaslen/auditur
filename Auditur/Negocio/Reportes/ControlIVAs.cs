using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helpers;

namespace Auditur.Negocio.Reportes
{
    public class ControlIVAs : IReport<ControlIVA>
    {
        private const decimal DiferenciaMinima = 0.5M;

        public List<ControlIVA> Generar(Semana oSemana)
        {
            List<ControlIVA> lstControlIVA = new List<ControlIVA>();
            
            List<char> lstTipoConceptoPermitidos = new List<char>();
            lstTipoConceptoPermitidos.Add('B'); //Billetes
            lstTipoConceptoPermitidos.Add('R'); //Reembolsos
            
            ControlIVA oControlIVA = null;

            //List<BO_Ticket> lstTicketsCargados = new List<BO_Ticket>();

            List<BSP_Ticket> lstTickets = oSemana.TicketsBSP.Where(x => lstTipoConceptoPermitidos.Contains(x.Concepto.Tipo) && x.Rg == BSP_Rg.Doméstico).OrderBy(x => x.Compania.Codigo).ThenBy(x => x.NroDocumento).ToList();
            foreach (BSP_Ticket oBSP_Ticket in lstTickets)
            {
                BO_Ticket bo_ticket = oSemana.TicketsBO.Find(x => x.Billete == oBSP_Ticket.NroDocumento && x.Compania.Codigo == oBSP_Ticket.Compania.Codigo);

                oControlIVA = new ControlIVA();

                oControlIVA.TarifaBSP = Math.Abs(oBSP_Ticket.TarContado + oBSP_Ticket.TarCredito);
                oControlIVA.IVATarifaBSP = Math.Abs(oBSP_Ticket.IVA105);
                oControlIVA.ComisionBSP = Math.Abs(oBSP_Ticket.ComValor + oBSP_Ticket.ComOver);
                oControlIVA.IVAComisionBSP = Math.Abs(oBSP_Ticket.ComIVA);

                if (bo_ticket != null)
                {
                    oControlIVA.TarifaBO = Math.Abs(bo_ticket.Tarifa);
                    oControlIVA.IVATarifaBO = Math.Abs(bo_ticket.IVA105);
                    oControlIVA.ComisionBO = Math.Abs(bo_ticket.ComValor + bo_ticket.ComOver);
                    oControlIVA.IVAComisionBO = Math.Abs(bo_ticket.IVAComision);
                }

                oControlIVA.TarifaDif = oControlIVA.TarifaBSP - oControlIVA.TarifaBO;
                oControlIVA.IVATarifaDif = oControlIVA.IVATarifaBSP - oControlIVA.IVATarifaBO;
                oControlIVA.ComisionDif = oControlIVA.ComisionBSP - oControlIVA.ComisionBO;
                oControlIVA.IVAComisionDif = oControlIVA.IVAComisionBSP - oControlIVA.IVAComisionBO;

                if (Math.Abs(oControlIVA.TarifaDif) > DiferenciaMinima || Math.Abs(oControlIVA.IVATarifaDif) > DiferenciaMinima || Math.Abs(oControlIVA.ComisionDif) > DiferenciaMinima || Math.Abs(oControlIVA.IVAComisionDif) > DiferenciaMinima)
                {
                    oControlIVA.BoletoNroBSP = oBSP_Ticket.NroDocumento.ToString();
                    oControlIVA.RgBSP = "C";
                    oControlIVA.TrBSP = oBSP_Ticket.Compania.Codigo;
                    oControlIVA.Tr2BSP = (oBSP_Ticket.Concepto.Tipo.Equals('R') ? "R" : (oBSP_Ticket.Tipo.Contains('F') && !oBSP_Ticket.Detalle.Any(x => x.Observaciones.Trim() == "CNJ") ? "B" : "V"));
                    oControlIVA.MonedaBSP = oBSP_Ticket.Moneda == Moneda.Peso ? "$" : "D";
                    oControlIVA.FechaBSP = AuditurHelpers.GetDateTimeString(oBSP_Ticket.FechaEmision);

                    if (bo_ticket != null)
                    {
                        oControlIVA.TrBO = bo_ticket.Compania != null ? bo_ticket.Compania.Codigo : "";
                        oControlIVA.NroTicketBO = bo_ticket.Billete.ToString();
                        oControlIVA.Referencia = bo_ticket.Expediente;
                        oControlIVA.Factura = bo_ticket.Factura;
                        oControlIVA.Pasajero = bo_ticket.Pasajero.ToString();
                    }

                    lstControlIVA.Add(oControlIVA);
                }
            }

            return lstControlIVA;
        }
    }
}
