﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Auditur.Negocio;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

namespace Auditur.Presentacion.Classes
{
    public static class BSPExtensions
    {
        public static List<PageChunks> ExtractChunks(this PdfPage page)
        {
            var textEventListener = new LocationTextExtractionStrategy();
            PdfTextExtractor.GetTextFromPage(page, textEventListener);

            IList<TextChunk> locationalResult = (IList<TextChunk>)locationalResultField.GetValue(textEventListener);
            List<PageChunks> pageChunks = new List<PageChunks>();

            foreach (TextChunk chunk in locationalResult)
            {
                ITextChunkLocation location = chunk.GetLocation();
                Vector start = location.GetStartLocation();
                Vector end = location.GetEndLocation();

                PageChunks ro = new PageChunks()
                {
                    Text = chunk.GetText().Trim(),
                    StartX = start.Get(Vector.I1),
                    Y = start.Get(Vector.I2),
                    EndX = end.Get(Vector.I1),
                    EndY = end.Get(Vector.I2)
                };
                pageChunks.Add(ro);
            }

            pageChunks = pageChunks.OrderByDescending(x => x.Y).ThenBy(x => x.StartX).ToList();
            if (pageChunks.Any())
            {
                var chunkToCompare = pageChunks.First();
                foreach (var chunk in pageChunks)
                {
                    if (Math.Abs(chunk.Y - chunkToCompare.Y) <= 4)
                    {
                        chunk.Y = chunkToCompare.Y;
                    }
                    else
                    {
                        chunkToCompare = chunk;
                    }
                }
                pageChunks = pageChunks.OrderByDescending(x => x.Y).ThenBy(x => x.StartX).ToList();
            }

            return pageChunks;
        }

        public static string GetChunkTextBetween(this List<PageChunks> pageChunks, int start, int end)
        {
            return pageChunks.Where(x => start <= x.StartX && x.EndX <= end).Select(x => x.Text).FirstOrDefault();
        }

        public static decimal ToDecimal(this string value)
        {
            return Convert.ToDecimal(value?.Replace("*", ""));
        }

        public static BSP_Ticket ObtenerBSP_Ticket(this List<PageChunks> orderedLine, Compania compania, Concepto concepto)
        {
            var oBSP_Ticket = new BSP_Ticket();

            oBSP_Ticket.Compania = compania;
            oBSP_Ticket.Concepto = concepto;
            oBSP_Ticket.TRNC = orderedLine.GetChunkTextBetween(37, 60);
            oBSP_Ticket.Billete = Convert.ToInt64(orderedLine.GetChunkTextBetween(63, 98));
            oBSP_Ticket.FechaEmision =
                DateTime.TryParse(orderedLine.GetChunkTextBetween(110, 140), out var fechaVenta)
                    ? (DateTime?)fechaVenta
                    : null;
            oBSP_Ticket.CPN = orderedLine.GetChunkTextBetween(143, 168);
            oBSP_Ticket.NR = orderedLine.GetChunkTextBetween(169, 192);
            oBSP_Ticket.Stat = orderedLine.GetChunkTextBetween(200, 211);
            oBSP_Ticket.Fop = orderedLine.GetChunkTextBetween(218, 235);
            oBSP_Ticket.ValorTransaccion = orderedLine.GetChunkTextBetween(242, 292).ToDecimal();
            oBSP_Ticket.ValorTarifa = orderedLine.GetChunkTextBetween(306, 346).ToDecimal();
            oBSP_Ticket.ImpuestoValor = orderedLine.GetChunkTextBetween(355, 388).ToDecimal();
            oBSP_Ticket.ImpuestoCodigo = orderedLine.GetChunkTextBetween(388, 400);
            oBSP_Ticket.ImpuestoTyCValor = orderedLine.GetChunkTextBetween(406, 442).ToDecimal();
            oBSP_Ticket.ImpuestoTyCCodigo = orderedLine.GetChunkTextBetween(442, 453);
            oBSP_Ticket.ImpuestoPenValor = orderedLine.GetChunkTextBetween(461, 496).ToDecimal();
            oBSP_Ticket.ImpuestoPenCodigo = orderedLine.GetChunkTextBetween(496, 507);
            oBSP_Ticket.ImpuestoCobl = orderedLine.GetChunkTextBetween(520, 562).ToDecimal();
            oBSP_Ticket.ComisionStdPorcentaje =
                orderedLine.GetChunkTextBetween(562, 585).ToDecimal();
            oBSP_Ticket.ComisionStdValor = orderedLine.GetChunkTextBetween(594, 639).ToDecimal();
            oBSP_Ticket.ComisionSuppPorcentaje =
                orderedLine.GetChunkTextBetween(638, 657).ToDecimal();
            oBSP_Ticket.ComisionSuppValor = orderedLine.GetChunkTextBetween(670, 711).ToDecimal();
            oBSP_Ticket.ImpuestoSinComision = orderedLine.GetChunkTextBetween(730, 764).ToDecimal();
            oBSP_Ticket.NetoAPagar = orderedLine.GetChunkTextBetween(780, 818).ToDecimal();
            return oBSP_Ticket;
        }

        public static BSP_Ticket_Detalle ObtenerBSP_Ticket_Detalle(this List<PageChunks> orderedLine)
        {
            var detalle = new BSP_Ticket_Detalle();
            detalle.TRNC = orderedLine.GetChunkTextBetween(37, 60);
            detalle.Billete = Convert.ToInt64(orderedLine.GetChunkTextBetween(63, 98));
            detalle.FechaVenta = DateTime.TryParse(orderedLine.GetChunkTextBetween(110, 140),
                out var fechaVentaDetalle)
                ? (DateTime?)fechaVentaDetalle
                : null;
            detalle.CPN = orderedLine.GetChunkTextBetween(143, 168);
            detalle.NR = orderedLine.GetChunkTextBetween(169, 192);
            detalle.Stat = orderedLine.GetChunkTextBetween(200, 211);
            detalle.Fop = orderedLine.GetChunkTextBetween(218, 235);
            detalle.ValorTransaccion = orderedLine.GetChunkTextBetween(242, 292).ToDecimal();
            detalle.ValorTarifa = orderedLine.GetChunkTextBetween(306, 346).ToDecimal();
            detalle.ImpuestoValor = orderedLine.GetChunkTextBetween(355, 388).ToDecimal();
            detalle.ImpuestoCodigo = orderedLine.GetChunkTextBetween(388, 400);
            detalle.ImpuestoTyCValor = orderedLine.GetChunkTextBetween(406, 442).ToDecimal();
            detalle.ImpuestoTyCCodigo = orderedLine.GetChunkTextBetween(442, 453);
            detalle.ImpuestoPenValor = orderedLine.GetChunkTextBetween(461, 496).ToDecimal();
            detalle.ImpuestoPenCodigo = orderedLine.GetChunkTextBetween(496, 507);
            detalle.ImpuestoCobl = orderedLine.GetChunkTextBetween(520, 562).ToDecimal();
            detalle.ComisionStdPorcentaje = orderedLine.GetChunkTextBetween(562, 585).ToDecimal();
            detalle.ComisionStdValor = orderedLine.GetChunkTextBetween(594, 639).ToDecimal();
            detalle.ComisionSuppPorcentaje = orderedLine.GetChunkTextBetween(638, 657).ToDecimal();
            detalle.ComisionSuppValor = orderedLine.GetChunkTextBetween(670, 711).ToDecimal();
            detalle.ImpuestoSinComision = orderedLine.GetChunkTextBetween(730, 764).ToDecimal();
            detalle.NetoAPagar = orderedLine.GetChunkTextBetween(780, 818).ToDecimal();
            return detalle;
        }

        private static FieldInfo locationalResultField = typeof(LocationTextExtractionStrategy).GetField("locationalResult", BindingFlags.NonPublic | BindingFlags.Instance);
    }
}