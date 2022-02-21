using Syncfusion.Pdf;
using Syncfusion.XlsIO;
using Syncfusion.XlsIORenderer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NTS.Document.Excel
{
    public class ExcelService : IExcelService
    {
        public MemoryStream ExportExcel<T>(List<T> datas, string teamplatePath, int columns, Dictionary<string, string> paramDic = null)
        {
            // Khỏi tạo bảng excel
            ExcelEngine excelEngine = new ExcelEngine();

            IApplication application = excelEngine.Excel;
            IWorkbook workbook = ExportData(application, datas, teamplatePath, false, columns, paramDic);

            MemoryStream stream = new MemoryStream();
            workbook.SaveAs(stream);

            workbook.Close();
            excelEngine.Dispose();

            return stream;
        }

        public string ExportExcelBase64<T>(List<T> datas, string teamplatePath, int columns, Dictionary<string, string> paramDic = null)
        {
            // Khỏi tạo bảng excel
            ExcelEngine excelEngine = new ExcelEngine();

            IApplication application = excelEngine.Excel;
            IWorkbook workbook = ExportData(application, datas, teamplatePath, false, columns, paramDic);

            string base64String = string.Empty;
            using (MemoryStream stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                base64String = Convert.ToBase64String(stream.ToArray());
                stream.Dispose();
            }

            workbook.Close();
            excelEngine.Dispose();

            return base64String;
        }

        public MemoryStream ExportPdf<T>(List<T> datas, string teamplatePath, int columns, Dictionary<string, string> paramDic = null)
        {
            // Khỏi tạo bảng excel
            ExcelEngine excelEngine = new ExcelEngine();

            IApplication application = excelEngine.Excel;
            IWorkbook workbook = ExportData(application, datas, teamplatePath, true, columns, paramDic);

            XlsIORenderer render = new XlsIORenderer();
            PdfDocument pdfDocument = render.ConvertToPDF(workbook.Worksheets[0]);

            MemoryStream stream = new MemoryStream();
            pdfDocument.Save(stream);

            pdfDocument.Close(true);

            workbook.Close();
            excelEngine.Dispose();

            return stream;
        }

        public string ExportPdfBase64<T>(List<T> datas, string teamplatePath, int columns, Dictionary<string, string> paramDic = null)
        {
            // Khỏi tạo bảng excel
            ExcelEngine excelEngine = new ExcelEngine();

            IApplication application = excelEngine.Excel;
            IWorkbook workbook = ExportData(application, datas, teamplatePath, true, columns, paramDic);

            XlsIORenderer render = new XlsIORenderer();
            PdfDocument pdfDocument = render.ConvertToPDF(workbook.Worksheets[0]);

            string base64String = string.Empty;
            using (MemoryStream stream = new MemoryStream())
            {
                pdfDocument.Save(stream);
                base64String = Convert.ToBase64String(stream.ToArray());
                stream.Dispose();
            }

            pdfDocument.Close(true);

            workbook.Close();
            excelEngine.Dispose();

            return base64String;
        }

        private IWorkbook ExportData<T>(IApplication application, List<T> datas, string teamplatePath, bool autofitRows, int columns, Dictionary<string, string> paramDic = null)
        {
            IWorkbook workbook = application.Workbooks.Open(File.OpenRead(Path.Combine(Directory.GetCurrentDirectory(), teamplatePath)));
            IWorksheet sheet = workbook.Worksheets[0];

            if (paramDic != null)
            {
                IRange range;
                foreach (KeyValuePair<string, string> item in paramDic)
                {
                    range = sheet.FindFirst(item.Key, ExcelFindType.Text, ExcelFindOptions.MatchCase);
                    range.Replace(item.Key, item.Value.ToString());
                }
            }

            int total = datas.Count;

            IRange iRangeData = sheet.FindFirst("<Data>", ExcelFindType.Text, ExcelFindOptions.MatchCase);
            iRangeData.Text = iRangeData.Text.Replace("<Data>", string.Empty);

            int rowData = iRangeData.Row;

            sheet.ImportData(datas, rowData, 1, false);

            // Autofit dòng đầu tiên của Data khi import, thường khi xuất Pdf thì dòng đầu tiên đang không autofit
            string columnName = GetExcelColumnName(columns);
            if (autofitRows)
            {
                sheet.Range["A" + rowData + ":" + columnName + (rowData + total)].AutofitRows();
            }

            IStyle bodyStyle = workbook.Styles.Add("BodyStyle");

            bodyStyle.BeginUpdate();
            bodyStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
            bodyStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
            bodyStyle.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Thin;
            bodyStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

            bodyStyle.EndUpdate();

            sheet.Range["A" + rowData + ":" + columnName + (rowData + total)].CellStyleName = "BodyStyle";

            return workbook;
        }

        private string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

    }
}
