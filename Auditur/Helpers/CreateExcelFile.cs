using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Helpers
{
    public class CreateExcelFile
    {
        public static bool CreateExcelDocument<T>(List<T> list, string Title, string xlsxFilePath, string[] header, string footer)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(ListToDataTable(list, Title));

                return CreateExcelDocument(ds, xlsxFilePath, header, footer);
            }
            catch (Exception ex)
            {
                TextToFile.Errores(TextToFile.Error(ex));
                return false;
            }
        }

        #region HELPER_FUNCTIONS

        public static DataTable ListToDataTable<T>(List<T> list, string Title)
        {
            DataTable dt = new DataTable(Title);
            try
            {
                foreach (PropertyInfo info in typeof(T).GetProperties())
                {
                    var dc = new DataColumn(info.Name, GetNullableType(info.PropertyType));
                    dc.Caption = GetName(info);
                    dt.Columns.Add(dc);
                }

                if (list.Any())
                {
                    foreach (T t in list)
                    {
                        DataRow row = dt.NewRow();
                        foreach (PropertyInfo info in typeof(T).GetProperties())
                        {
                            if (!IsNullableType(info.PropertyType))
                                row[info.Name] = info.GetValue(t, null);
                            else
                                row[info.Name] = (info.GetValue(t, null) ?? DBNull.Value);
                        }
                        dt.Rows.Add(row);
                    }
                }
                else
                {
                    dt.Rows.Add(dt.NewRow());
                }
            }
            catch (Exception ex)
            {
                TextToFile.Errores(TextToFile.Error(ex));
                return null;
            }
            return dt;
        }

        private static string GetName(PropertyInfo info)
        {
            return info.IsDefined(typeof(DisplayAttribute), false) ? info.GetCustomAttributes(typeof(DisplayAttribute), false).Cast<DisplayAttribute>().Single().Name : info.Name;
        }

        private static Type GetNullableType(Type t)
        {
            Type returnType = t;
            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                returnType = Nullable.GetUnderlyingType(t);
            }
            return returnType;
        }

        private static bool IsNullableType(Type type)
        {
            return (type == typeof(string) ||
                    type.IsArray ||
                    (type.IsGenericType &&
                     type.GetGenericTypeDefinition().Equals(typeof(Nullable<>))));
        }

        public static bool CreateExcelDocument(DataTable dt, string xlsxFilePath, string[] header, string footer)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                bool result = CreateExcelDocument(ds, xlsxFilePath, header, footer);
                ds.Tables.Remove(dt);
                return result;
            }
            catch (Exception ex)
            {
                TextToFile.Errores(TextToFile.Error(ex));
                return false;
            }
        }

        #endregion HELPER_FUNCTIONS

        /// <summary>
        /// Create an Excel file, and write it to a file.
        /// </summary>
        /// <param name="ds">DataSet containing the data to be written to the Excel.</param>
        /// <param name="excelFilename">Name of file to be written.</param>
        /// <param name="header">Header of the table.</param>
        /// <returns>True if successful, false if something went wrong.</returns>
        public static bool CreateExcelDocument(DataSet ds, string excelFilename, string[] header, string footer)
        {
            try
            {
                List<string> refsTitles = new List<string>
                {
                    "COBL:",
                    "COM STD:",
                    "COM SUPL:",
                    "FOP:",
                    "PEN:",
                    "RTDN:",
                    "STAT:",
                    "T&C:",
                    "TRNC:"
                };

                List<string> refs = new List<string>
                {
                    "Tarifa  + Impuestos comisionables",
                    "Comisión",
                    "Over",
                    "Forma de pago",
                    "Penalidad",
                    "Documento de referencia",
                    "Ruta (I Internacional - D Doméstico)",
                    "Imp. Tasas y Cargos",
                    "Tipo de documento"
                };

                var azulOscuro = XLColor.FromArgb(100, 54, 96, 146);
                var azulClarito = XLColor.FromArgb(100, 220, 230, 241);
                var azul = XLColor.FromArgb(100, 172, 200, 234);

                XLWorkbook wb = new XLWorkbook();
                foreach (DataTable dt in ds.Tables)
                {
                    var ws = wb.Worksheets.Add(dt.TableName);
                    ws.PageSetup.PageOrientation = XLPageOrientation.Landscape;

                    var rgHeader = ws.Cell(1, 1).InsertData(header);
                    rgHeader.Style.Font.Bold = true;
                    rgHeader.Style.Font.SetFontSize(10);
                    rgHeader.Style.Font.FontName = "Verdana";
                    rgHeader.Style.Font.FontColor = XLColor.White;
                    rgHeader.Row(1).Style.Font.SetFontSize(14);
                    ws.Row(1).Height = 58;
                    ws.Rows(1, 3).Style.Fill.BackgroundColor = azulOscuro;

                    int EspacioHeaderTable = 2;

                    var headerLength = header.Length;

                    /*if (dt.TableName == "DiferenciasIVA")
                    {
                        int filaExtra = header.Length + EspacioHeaderTable;

                        ws.Cell(filaExtra, 1).Value = "Liquidación BSP";
                        ws.Cell(filaExtra, 11).Value = "Liquidación BO";
                        ws.Cell(filaExtra, 20).Value = "Diferencias (BSP - BackOffice) (Solo > 0,50)";

                        ws.Range(filaExtra, 1, filaExtra, 10).Merge();
                        ws.Range(filaExtra, 11, filaExtra, 19).Merge();
                        ws.Range(filaExtra, 20, filaExtra, 23).Merge();
                        ws.Range(filaExtra, 1, filaExtra, 23).Style.Font.Bold = true;
                        ws.Range(filaExtra, 1, filaExtra, 23).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        EspacioHeaderTable++;
                    }
                    else if (dt.TableName == "DiferenciasEmisiones")
                    {
                        int filaExtra = header.Length + EspacioHeaderTable;

                        ws.Cell(filaExtra, 1).Value = "Liquidación BSP";
                        ws.Cell(filaExtra, 10).Value = "Liquidación BO";
                        ws.Cell(filaExtra, 18).Value = "Diferencias (BSP - BackOffice)";

                        ws.Range(filaExtra, 1, filaExtra, 9).Merge();
                        ws.Range(filaExtra, 10, filaExtra, 17).Merge();
                        ws.Range(filaExtra, 18, filaExtra, 23).Merge();
                        ws.Range(filaExtra, 1, filaExtra, 23).Style.Font.Bold = true;
                        ws.Range(filaExtra, 1, filaExtra, 23).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        EspacioHeaderTable++;
                    }
                    else 
                    if (dt.TableName == "Emisiones")
                    {
                        int filaExtra = headerLength + EspacioHeaderTable;

                        ws.Cell(filaExtra, 7).Value = "Fecha";
                        ws.Cell(filaExtra, 9).Value = "Forma de Pago";
                        ws.Cell(filaExtra, 11).Value = "Impuestos";
                        ws.Cell(filaExtra, 13).Value = "IVA";
                        ws.Cell(filaExtra, 14).Value = "Comisión";
                        ws.Cell(filaExtra, 16).Value = "IVA";
                        ws.Cell(filaExtra, 17).Value = "Total";

                        ws.Range(filaExtra, 9, filaExtra, 10).Merge();
                        ws.Range(filaExtra, 9, filaExtra, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range(filaExtra, 11, filaExtra, 12).Merge();
                        ws.Range(filaExtra, 11, filaExtra, 12).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range(filaExtra, 14, filaExtra, 15).Merge();
                        ws.Range(filaExtra, 14, filaExtra, 15).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        ws.Range(filaExtra, 1, filaExtra, 19).Style.Font.Bold = true;

                        EspacioHeaderTable++;
                    }
                    else */
                    if (dt.TableName == "Over")
                    {
                        int filaExtra = headerLength + EspacioHeaderTable;

                        ws.Cell(filaExtra, 6).Value = "OVER PESOS";
                        ws.Cell(filaExtra, 9).Value = "OVER DOLARES";

                        var rangeWithBorders = ws.Range(filaExtra, 6, filaExtra + 1, 11);
                        rangeWithBorders.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                        rangeWithBorders.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        rangeWithBorders.Style.Border.InsideBorderColor = azulOscuro;
                        rangeWithBorders.Style.Border.OutsideBorderColor = azulOscuro;

                        ws.Range(filaExtra, 6, filaExtra, 8).Merge();
                        ws.Range(filaExtra, 9, filaExtra, 11).Merge();
                        ws.Row(filaExtra).Style.Font.Bold = true;
                        ws.Range(filaExtra, 1, filaExtra, 14).Style.Fill.BackgroundColor = azulClarito;
                        ws.Row(filaExtra).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        EspacioHeaderTable++;
                    }/*
                    else if (dt.TableName == "ListadoParaFacturación")
                    {
                        int filaExtra = headerLength + EspacioHeaderTable;

                        ws.Cell(filaExtra, 7).Value = "Fecha";
                        ws.Cell(filaExtra, 11).Value = "IVA";
                        ws.Cell(filaExtra, 14).Value = "Comisión";
                        ws.Cell(filaExtra, 16).Value = "IVA";
                        ws.Cell(filaExtra, 17).Value = "Total";

                        ws.Cell(filaExtra, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell(filaExtra, 16).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range(filaExtra, 14, filaExtra, 15).Merge();
                        ws.Range(filaExtra, 14, filaExtra, 15).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        ws.Range(filaExtra, 1, filaExtra, 18).Style.Font.Bold = true;

                        EspacioHeaderTable++;
                    }*/

                    var table = ws.Cell(headerLength + EspacioHeaderTable, 1).InsertTable(dt);
                    table.ShowAutoFilter = false;
                    table.Theme = XLTableTheme.None;
                    table.Cells(x => x.DataType == XLDataType.Text).Style.Alignment.Horizontal =
                        XLAlignmentHorizontalValues.Center;
                    table.Style.NumberFormat.NumberFormatId = 2;

                    IXLRangeRow row1 = table.Row(1);
                    row1.Style.Font.Bold = true;
                    row1.Style.Fill.BackgroundColor = azulClarito;
                    row1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    row1.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    row1.Style.Border.BottomBorderColor = azulOscuro;

                    int colCount = table.ColumnCount();

                    var rangeHeader = ws.Range(1, 1, headerLength, colCount);
                    for (int i = 1; i <= headerLength; i++)
                        rangeHeader.Range(i, 1, i, 14).Merge();


                    IXLRangeRows rows = null;
                    if (dt.TableName == "Over" || dt.TableName == "Creditos" || dt.TableName == "Debitos" || dt.TableName == "Reembolsos")
                    {
                        rows = table.Rows(x => x.Cell(1).Value.ToString() == "TOTAL");
                    }

                    if (dt.TableName == "DiferenciasEmisiones" || dt.TableName == "DiferenciasIVAyComs")
                    {
                        rows = table.Rows(x => x.Cell(1).Value.ToString() == "DIF");
                    }

                    if (rows != null)
                    {
                        rows.Style.Font.Bold = true;
                        rows.Style.Fill.BackgroundColor = azul;
                        rows.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        rows.Style.Border.TopBorderColor = azulOscuro;
                    }

                    if (dt.TableName == "Creditos" || dt.TableName == "Debitos")
                    {
                        var cellObservaciones = table.Cell(1, colCount);
                        cellObservaciones.Value = "Al hacer doble clic en esta columna, podrá limpiar los datos obtenidos del " + (dt.TableName == "Creditos" ? "ACM" : "ADM") + ", dejando únicamente lo necesario.";
                        cellObservaciones.Style.Font.SetFontSize(9);
                        cellObservaciones.Style.Font.Italic = true;
                        cellObservaciones.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    }

                    ws.Columns().AdjustToContents();
                    ws.Cell(table.LastRow().RowNumber() + 2, 1).Value = footer;
                }

                var wsRefs = wb.Worksheets.Add("Referencias");
                var rgRefsTitles = wsRefs.Cell(1, 1).InsertData(refsTitles);
                var rgRefs = wsRefs.Cell(1, 2).InsertData(refs);
                rgRefsTitles.Style.Font.SetFontSize(12);
                rgRefsTitles.Style.Font.Bold = true;
                rgRefs.Style.Font.SetFontSize(12);
                wsRefs.Columns().AdjustToContents();

                wb.SaveAs(excelFilename);

                return true;
            }
            catch (Exception ex)
            {
                TextToFile.Errores(TextToFile.Error(ex));
                return false;
            }
        }
    }
}