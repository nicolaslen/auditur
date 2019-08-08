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
                    "REF:",
                    "COBL:",
                    "COM STD:",
                    "COM SUPL:",
                    "FOP:",
                    "PEN:",
                    "RTDN:",
                    "STAT:",
                    "T&C",
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

                XLWorkbook wb = new XLWorkbook();
                foreach (DataTable dt in ds.Tables)
                {
                    var ws = wb.Worksheets.Add(dt.TableName);
                    ws.PageSetup.PageOrientation = XLPageOrientation.Landscape;

                    var rgHeader = ws.Cell(1, 1).InsertData(header);
                    rgHeader.Style.Font.Bold = true;
                    rgHeader.Style.Fill.BackgroundColor = XLColor.Gainsboro;

                    var rgRefsTitles = ws.Cell(1, 10).InsertData(refsTitles);
                    var rgRefs = ws.Cell(2, 11).InsertData(refs);

                    rgRefsTitles.Style.Font.SetFontSize(8);
                    rgRefsTitles.Style.Font.Bold = true;

                    rgRefs.Style.Font.SetFontSize(8);

                    int EspacioHeaderTable = 2;

                    var headerLength = Math.Max(header.Length, refsTitles.Count);

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

                        ws.Range(filaExtra, 6, filaExtra, 8).Merge();
                        ws.Range(filaExtra, 9, filaExtra, 11).Merge();
                        ws.Row(filaExtra).Style.Font.Bold = true;
                        ws.Row(filaExtra).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        EspacioHeaderTable++;
                    }
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
                    }

                    var table = ws.Cell(headerLength + EspacioHeaderTable, 1).InsertTable(dt);
                    table.ShowAutoFilter = false;
                    table.Theme = XLTableTheme.None;

                    table.Row(1).Style.Font.Bold = true;

                    int colCount = table.ColumnCount();

                    var rangeHeader = ws.Range(1, 1, headerLength, colCount);
                    for (int i = 1; i <= headerLength; i++)
                        rangeHeader.Range(i, 1, i, 9).Merge();

                    if (dt.TableName == "Over" || dt.TableName == "Creditos" || dt.TableName == "Debitos" || dt.TableName == "Reembolsos")
                    {
                        table.Rows(x => x.Cell(1).Value.ToString() == "TOTAL").Style.Font.Bold = true;
                        table.Rows(x => x.Cell(1).Value.ToString() == "TOTAL").Style.Fill.BackgroundColor = XLColor.Gainsboro;
                    }

                    ws.Columns().AdjustToContents();
                    ws.Cell(table.LastRow().RowNumber() + 2, 1).Value = footer;
                }

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