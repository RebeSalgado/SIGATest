using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPOI;
using System.Data;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;

namespace GardiSoft.Models
{
    public class Excel2
    {
        public IWorkbook Workbook { get; set; }

        public Excel2()
        {
            Workbook = new XSSFWorkbook();
        }



        public string ExportarSp(DataTable exportData, string ruta)
        {

            // IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet1 = Workbook.CreateSheet("hoja 1");

                      
            for (int i = 0; i < exportData.Rows.Count; i++)
            {
                var row = sheet1.CreateRow(i);
                for (var colIndex = 0; colIndex < exportData.Columns.Count; colIndex++)
                {

                    var cell = row.CreateCell(colIndex);
                    if (i == 0)
                    {
                        cell.SetCellValue(exportData.Columns[colIndex].ColumnName);
                    }
                    else
                    {
                        cell.SetCellValue(exportData.Rows[i][colIndex].ToString());
                    }
                }


            }

            return Guardar(ruta);
        }


        protected string Guardar(string ruta)
        {
            var archivo = Guid.NewGuid().ToString() + ".xlsx";
            ruta = ruta + archivo;
            FileStream sw = File.Create(ruta);
            Workbook.Write(sw);
            sw.Close();
            return archivo;
        }



    }
}