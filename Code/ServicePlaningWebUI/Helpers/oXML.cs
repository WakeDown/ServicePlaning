using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ServicePlaningWebUI.Helpers
{
    public class OXml
    {


        public static Dictionary<string, string> GetMarkupValues(Stream stream, params string[] markupArr)
        {
            var result = new Dictionary<string, string>();

            string sheetName = "Заявка";
            string addressName = "et_name";

            var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheets.Worksheet(sheetName);

            var range = workbook.Range(addressName);

            var cells = range.Cells();

            foreach (var xlCell in cells)
            {
                result.Add("KEY", xlCell.Value.ToString());
            }

            

            //SpreadsheetDocument document = SpreadsheetDocument.Open(stream, false);

            //WorkbookPart wbPart = document.WorkbookPart;

            

            //Sheet theSheet = wbPart.Workbook.Descendants<Sheet>().
            //  Where(s => s.Name == sheetName).FirstOrDefault();

            //// Throw an exception if there is no sheet.
            //if (theSheet == null)
            //{
            //    throw new ArgumentException("sheetName");
            //}

            //WorksheetPart wsPart = (WorksheetPart)(wbPart.GetPartById(theSheet.Id));

            

            //Cell theCell = wsPart.Worksheet.Descendants<Cell>().Where(c => c.LocalName == addressName).FirstOrDefault();

            //if (theCell != null)
            //{
            //    result.Add("KEY", theCell.InnerText);
            //    // Code removed here…
            //}

            return result;
        }

    }
}