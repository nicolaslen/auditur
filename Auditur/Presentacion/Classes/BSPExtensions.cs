using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Auditur.Negocio;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

namespace Auditur.Presentacion.Classes
{
    public static class BSPExtensions
    {
        private static Dictionary<string, int> months = new Dictionary<string, int>
        {
            {
                "JAN", 1
            },
            {
                "FEB", 2
            },
            {
                "MAR", 3
            },
            {
                "APR", 4
            },
            {
                "MAY", 5
            },
            {
                "JUN", 6
            },
            {
                "JUL", 7
            },
            {
                "AUG", 8
            },
            {
                "SEP", 9
            },
            {
                "OCT", 10
            },
            {
                "NOV", 11
            },
            {
                "DEC", 12
            }
        };

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

        private static DateTime? GetDateTime(string text)
        {
            DateTime? returnDateTime = null;
            try
            {
                int day = int.Parse(text.Substring(0, 2));
                string month = text.Substring(2, 3);
                int year = int.Parse(text.Substring(5, 2));
                returnDateTime = new DateTime(DateTime.Now.Year / 100 * 100 + year, months[month], day);
            }
            catch
            {
                /*
                 DateTime.TryParse(orderedLine.GetChunkTextBetween(110, 140), out var fechaVenta)
                    ? (DateTime?)fechaVenta
                    : null;
                    */
            }
            return returnDateTime;
        }

        public static BSP_Ticket ObtenerBSP_Ticket(this List<PageChunks> orderedLine, Compania compania, Concepto concepto, Moneda moneda, BSP_Rg rg, CultureInfo culture)
        {
            var oBSP_Ticket = new BSP_Ticket();

            oBSP_Ticket.Compania = compania;
            oBSP_Ticket.Concepto = concepto;
            oBSP_Ticket.Moneda = moneda;
            oBSP_Ticket.Rg = rg;
            oBSP_Ticket.Trnc = orderedLine.GetChunkTextBetween(37, 60);
            oBSP_Ticket.NroDocumento = Convert.ToInt64(orderedLine.GetChunkTextBetween(63, 98));
            oBSP_Ticket.FechaEmision = GetDateTime(orderedLine.GetChunkTextBetween(110, 140));
            oBSP_Ticket.Cpn = orderedLine.GetChunkTextBetween(143, 168);
            oBSP_Ticket.Nr = orderedLine.GetChunkTextBetween(169, 192);
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

            var possibleRTDN = orderedLine.GetChunkTextBetween(20, 170);
            if (possibleRTDN != null && possibleRTDN.Length >= 5 && possibleRTDN.Substring(0, 5) == "+RTDN")
            {
                possibleRTDN = Regex.Replace(possibleRTDN, @"\s+", " ");
                var splitedRtdn = possibleRTDN.Split(' ');
                detalle.Trnc = splitedRtdn[0];
                detalle.NroDocumento = Convert.ToInt64(splitedRtdn[1]);
                detalle.Cpn = splitedRtdn[2];
            }
            else
            {
                detalle.Trnc = orderedLine.GetChunkTextBetween(37, 60);
                detalle.NroDocumento = Convert.ToInt64(orderedLine.GetChunkTextBetween(63, 98));
                detalle.Cpn = orderedLine.GetChunkTextBetween(143, 168);
            }

            detalle.FechaEmision = DateTime.TryParse(orderedLine.GetChunkTextBetween(110, 140),
                out var fechaVentaDetalle)
                ? (DateTime?)fechaVentaDetalle
                : null;
            detalle.Nr = orderedLine.GetChunkTextBetween(169, 192);
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