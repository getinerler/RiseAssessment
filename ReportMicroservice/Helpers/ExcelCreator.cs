using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ReportMicroservice.Dtos;
using System;
using System.Collections.Generic;
using System.IO;

namespace ReportMicroservice.Helpers
{
    public class ExcelCreator
    {
        private List<PhoneBookItem> _phoneBookItems;
        private readonly IWorkbook workbook;
        private readonly ISheet sheet;
        private readonly ICellStyle headerStyle;
        private int rowNum = 0;

        public ExcelCreator(List<PhoneBookItem> phoneBookItems)
        {
            _phoneBookItems = phoneBookItems;

            this.workbook = new XSSFWorkbook();
            this.sheet = workbook.CreateSheet();
            headerStyle = GetHeaderStyle();
        }

        public byte[] GetFile()
        {
            CreateHeader();
            Fill();
            ResizeColumns();

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms, false);
                return ms.ToArray();
            }
        }

        private void CreateHeader()
        {
            int columnCounter = 0;

            IRow headerRow = sheet.CreateRow(rowNum++);

            CreateCell(headerRow, columnCounter++, "Country", headerStyle);
            CreateCell(headerRow, columnCounter++, "City", headerStyle);
            CreateCell(headerRow, columnCounter++, "Count", headerStyle);
        }

        private void Fill()
        {
            int columnCounter;

            foreach (PhoneBookItem val in _phoneBookItems)
            {
                columnCounter = 0;

                IRow row = sheet.CreateRow(rowNum++);

                CreateCell(row, columnCounter++, val.Country ?? string.Empty);
                CreateCell(row, columnCounter++, val.City ?? string.Empty);
                CreateCell(row, columnCounter++, val.Count.ToString() ?? string.Empty);
            }
        }

        private void ResizeColumns()
        {
            int columnCount = 8;
            for (int i = 0; i <= columnCount; i++)
            {
                sheet.AutoSizeColumn(i);
                GC.Collect();
            }
        }

        private void CreateCell(IRow row, int index, string value, ICellStyle style = null)
        {
            ICell Cell = row.CreateCell(index);
            Cell.SetCellValue(value);
            if (style != null)
            {
                Cell.CellStyle = style;
            }
        }

        private void CreateCell(IRow row, int index, decimal value, ICellStyle style = null)
        {
            ICell Cell = row.CreateCell(index);
            Cell.SetCellType(CellType.Numeric);
            Cell.SetCellValue(Convert.ToDouble(value));
            if (style != null)
            {
                Cell.CellStyle = style;
            }
        }

        private ICellStyle GetHeaderStyle()
        {
            ICellStyle style = workbook.CreateCellStyle();

            IFont boldFont = workbook.CreateFont();
            boldFont.Boldweight = (short)FontBoldWeight.Bold;
            style.SetFont(boldFont);

            style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            style.FillPattern = FillPattern.SolidForeground;
            return style;
        }
    }
}
