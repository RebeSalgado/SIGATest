using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office;
using Microsoft.Office.Interop;
using System.IO;
using System.Data;
using System.Runtime.InteropServices;


namespace GardiSoft.Models 
{
    public class ExcelWriter : IDisposable
    {
        /// libro de excel que se abrirá
        /// </summary>
        private Excel.Application excelApp;
        /// <summary>
        /// libro dentro del excel.
        /// </summary>
        private Excel.Workbook libro;
        /// <summary>
        /// planilla dentro del libro.
        /// </summary>
        private Excel.Worksheet planilla;

        public class ConfiguracionHoja
        {
            public int NumeroHoja { get; set; }
            public int Columna { get; set; }
            public string Formato { get; set; }

        }


        //public string TablaToExcel(string rutaPlantilla, string nombreArchivo, string rutaProduccion, DataTable tabla, params ConfiguracionHoja[] configuracion)
        //{
        //    AbrirExcel(rutaPlantilla);
        //    nombreArchivo = nombreArchivo + Guid.NewGuid() + ".xlsx";
        //    if (tabla.Rows.Count > 0 && tabla.Columns.Count > 0)
        //    {
        //        AsignarValoresPoRango(1, 2, 1, tabla.Rows.Count, tabla.Columns.Count, tabla, false);
        //    }

        //    if (configuracion != null && configuracion.Length > 0)
        //        foreach (var item in configuracion)
        //        {
        //            CambiarFormatoColumna(item.NumeroHoja, item.Columna, item.Formato);
        //        }
        //    guardarComo(rutaProduccion + nombreArchivo);

        //    CerrarExcel();

        //    return nombreArchivo;
        //}

        public string TablaToExcel(string nombreArchivo, string rutaProduccion, DataTable tabla, params ConfiguracionHoja[] configuracion)
        {
            NuevoExcel();
            nombreArchivo = nombreArchivo + Guid.NewGuid() + ".xlsx";
            if (tabla.Rows.Count > 0 && tabla.Columns.Count > 0)
            {
                AsignarValoresPoRango(1, 2, 1, tabla.Rows.Count, tabla.Columns.Count, tabla, true);
            }

            if (configuracion != null && configuracion.Length > 0)
                foreach (var item in configuracion)
                {
                    CambiarFormatoColumna(item.NumeroHoja, item.Columna, item.Formato);
                }
            guardarComo(rutaProduccion + nombreArchivo);


            CerrarExcel();
            return nombreArchivo;
        }

        public ExcelWriter()
        {

        }


        public ExcelWriter(string ruta)
        {
            AbrirExcel(ruta);
        }

        public void AbrirExcel(string ruta)
        {
            excelApp = new Excel.Application();
            excelApp.DisplayAlerts = false;
            //  aca solo la ruta es relevante
            libro = excelApp.Workbooks.Open(ruta, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
            Type.Missing, Type.Missing, Type.Missing, Type.Missing,
            Type.Missing, Type.Missing, Type.Missing, Type.Missing,
            Type.Missing, Type.Missing);
            // planilla = (Excel.Worksheet)libro.Sheets[1];

        }

        public void NuevoExcel()
        {
            excelApp = new Excel.Application();
            excelApp.DisplayAlerts = false;
            //  aca solo la ruta es relevante
            libro = excelApp.Workbooks.Add(Type.Missing);
            // planilla = (Excel.Worksheet)libro.Sheets[1];

        }


        public void AgregarFila(int numeroPagina, int NumeroDeFila)
        {
            var hoja = ObtenerHoja(numeroPagina);
            var startCell = (Microsoft.Office.Interop.Excel.Range)hoja.Cells[NumeroDeFila, 1];
            startCell.EntireRow.Insert();

        }


        public void EliminarFila(int numeroPagina, int NumeroDeFila)
        {
            var hoja = ObtenerHoja(numeroPagina);
            var startCell = (Microsoft.Office.Interop.Excel.Range)hoja.Cells[NumeroDeFila, 1];
            startCell.EntireRow.Delete();

        }


        /// <summary>
        /// retorna la hoja con el índice seleccionado
        /// </summary>
        /// <param name="numPagina">numero de página min value: 1</param>
        /// <returns>jota de excel</returns>
        private Excel.Worksheet ObtenerHoja(int numPagina)
        {
            return (Excel.Worksheet)libro.Sheets[numPagina];

        }

        private Excel.Worksheet ObtenerHoja(string nombre)
        {
            return (Excel.Worksheet)libro.Sheets[nombre];

        }

        /// <summary>
        /// cambia el formato de toda la columna en la que se encuentra la celda
        /// </summary>
        /// <param name="numeroPagina">página donde se enuentra la columna</param>     
        /// <param name="columna">columna de la cenlda</param>
        public void CambiarFormatoColumna(int numeroPagina, int columna, string formato)
        {
            var hoja = ObtenerHoja(numeroPagina);
            var startCell = (Microsoft.Office.Interop.Excel.Range)hoja.Cells[1, columna];
            startCell.EntireColumn.NumberFormat = formato;


        }




        /// <summary>
        /// cambia el color de fondo  de toda la fila en la que se encuentra la celda
        /// </summary>
        /// <param name="numeroPagina">página donde se enuentra la columna</param>     
        /// <param name="columna">columna de la cenlda</param>
        public void CambiarFormatoColor(int numeroPagina, int fila, int colorIndex)
        {
            var hoja = ObtenerHoja(numeroPagina);
            var startCell = (Microsoft.Office.Interop.Excel.Range)hoja.Cells[fila, 1];
            // startCell.EntireRow.Interior.ColorIndex = colorIndex;
            startCell.EntireRow.Interior.ColorIndex = colorIndex;



        }

        public void AgregarVinculo(int paginaVinculo, int FilaCeldaVinculo, int ColumnaCeldaVinculo, string nombrePaginaDestino)
        {
            var hoja = ObtenerHoja(paginaVinculo);
            Excel.Range rangeToHoldHyperlink = (Microsoft.Office.Interop.Excel.Range)hoja.Cells[FilaCeldaVinculo, ColumnaCeldaVinculo];
            string hyperlinkTargetAddress = nombrePaginaDestino + "!A1";
            hoja.Hyperlinks.Add(rangeToHoldHyperlink, string.Empty, hyperlinkTargetAddress, "Screen Tip Text", "ver lista");

        }


        /// <summary>
        /// cambia el color de fondo  de toda la fila en la que se encuentra la celda
        /// </summary>
        /// <param name="numeroPagina">página donde se enuentra la columna</param>     
        /// <param name="columna">columna de la cenlda</param>
        public void CambiarFormatoColorColumna(int numeroPagina, int columna, int colorIndex)
        {
            var hoja = ObtenerHoja(numeroPagina);
            var startCell = (Microsoft.Office.Interop.Excel.Range)hoja.Cells[1, columna];
            // startCell.EntireRow.Interior.ColorIndex = colorIndex;
            startCell.EntireColumn.Interior.ColorIndex = colorIndex;



        }

        /// <summary>
        /// cambia el color de fondo  de toda la fila en la que se encuentra la celda
        /// </summary>
        /// <param name="numeroPagina">página donde se enuentra la columna</param>     
        /// <param name="columna">columna de la cenlda</param>
        public void CambiarFormatoColor(int numeroPagina, int fila, int rojo, int verde, int azul)
        {
            var hoja = ObtenerHoja(numeroPagina);
            var startCell = (Microsoft.Office.Interop.Excel.Range)hoja.Cells[fila, 1];
            // startCell.EntireRow.Interior.ColorIndex = colorIndex;
            startCell.EntireRow.Interior.Color = System.Drawing.Color.FromArgb(rojo, verde, azul);



        }

        /// <summary>
        /// cambia el color de fondo  de toda la fila en la que se encuentra la celda
        /// </summary>
        /// <param name="numeroPagina">página donde se enuentra la columna</param>     
        /// <param name="columna">columna de la cenlda</param>
        public void CambiarFormatoColor(int numeroPagina, int fila, System.Drawing.Color color)
        {
            var hoja = ObtenerHoja(numeroPagina);
            var startCell = (Microsoft.Office.Interop.Excel.Range)hoja.Cells[fila, 1];
            // startCell.EntireRow.Interior.ColorIndex = colorIndex;
            startCell.EntireRow.Interior.Color = color;

        }

        /// <summary>
        /// cambia el color de fondo  de  la celda
        /// </summary>
        /// <param name="numeroPagina">página donde se enuentra la columna</param>     
        /// <param name="columna">columna de la cenlda</param>
        public void CambiarFormatoColorCelda(int numeroPagina, int fila, int columna, System.Drawing.Color color)
        {
            var hoja = ObtenerHoja(numeroPagina);
            var startCell = (Microsoft.Office.Interop.Excel.Range)hoja.Cells[fila, columna];
            // startCell.EntireRow.Interior.ColorIndex = colorIndex;
            startCell.Interior.Color = color;

        }


        public void AjustarColumnaATexto(int numPagina, int numColumna)
        {
            var hoja = ObtenerHoja(numPagina);
            var startCell = (Microsoft.Office.Interop.Excel.Range)hoja.Cells[1, numColumna];
            startCell.EntireColumn.AutoFit();

        }

        public void NegritaACelda(int numPagina, int fila, int numColumna)
        {
            var hoja = ObtenerHoja(numPagina);
            var startCell = (Microsoft.Office.Interop.Excel.Range)hoja.Cells[fila, numColumna];
            startCell.Font.Bold = true;

        }



        public void AlinearTextoDerecha(int numPagina, int fila, int numColumna)
        {
            var hoja = ObtenerHoja(numPagina);
            var startCell = (Microsoft.Office.Interop.Excel.Range)hoja.Cells[fila, numColumna];
            startCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

        }







        /// <summary>
        /// cierra la app de excel
        /// </summary>
        public void CerrarExcel()
        {
            try
            {
                // Garbage collecting
                GC.Collect();
                GC.WaitForPendingFinalizers();
                // Clean up references to all COM objects
                // As per above, you're just using a Workbook and Excel Application instance, so release them:
                // excelApp.Workbooks.Close(false, Type.Missing, Type.Missing);
                libro.Close(false, Type.Missing, Type.Missing);
                Marshal.FinalReleaseComObject(libro);
                Marshal.FinalReleaseComObject(excelApp);
            }
            catch
            { }




        }


        /// <summary>
        /// Mata excel, lo hice por que no se como cerrarlo (#!"#$"&%$%#"/&()#"#) edit: ahora ya cierra, este método es incecesario, pero queda para el recuerdo.
        /// </summary>
        private void killExcel()
        {

            //TODO: no supe como cerrar excel, lo tuve que matar
            System.Diagnostics.Process[] PROC = System.Diagnostics.Process.GetProcessesByName("EXCEL");
            foreach (System.Diagnostics.Process PK in PROC)
            {
                if (PK.MainWindowTitle.Length == 0)
                {
                    PK.Kill();
                }
            }
        }


        /// <summary>
        /// obtiene el valor de la celda según el nombre de la celda
        /// </summary>
        /// <param name="celda">A1, A2 A3.. etc</param>
        /// <returns></returns>
        public string Valor(string celda)
        {

            return planilla.get_Range(celda).Value;
        }

        /// <summary>
        /// obtiene el valor de la celda según los indice de la celda
        /// </summary>
        /// <param name="pagina">indice de pagina</param>
        /// <param name="fila">indice de fila</param>
        /// <param name="columna">indice de columna</param>
        /// <returns></returns>
        public dynamic Valor(int pagina, int fila, int columna)
        {

            var hoja = ObtenerHoja(pagina);
            var celda = (Microsoft.Office.Interop.Excel.Range)hoja.Cells[fila, columna];
            return celda.Value;
        }


        /// <summary>
        /// Permite leer por rango una parte del archivo de excel.
        /// </summary>
        /// <param name="pagina">numero de la página que se desee leer</param>
        /// <param name="filaInicio">indice de fila en la que se encuentra la celda inicial del rango</param>
        /// <param name="ColumnaInicio">indice de columna en la que se encuentra la celda inicial del rango</param>
        /// <param name="FilaTermino">indice de fila en la que se encuentra la celda final del rango</param>
        /// <param name="ColumnaTermino">indice de columna en la que se encuentra la celda final del rango</param>
        /// <returns>retorna una matriz de objetos con todos los valores</returns>
        public dynamic LeerValorPorRango(int pagina, int filaInicio, int ColumnaInicio, int FilaTermino, int ColumnaTermino)
        {
            var hoja = ObtenerHoja(pagina);
            var startCell = (Microsoft.Office.Interop.Excel.Range)hoja.Cells[filaInicio, ColumnaInicio];
            var endCell = (Microsoft.Office.Interop.Excel.Range)hoja.Cells[FilaTermino, ColumnaTermino];
            var readRange = hoja.Range[startCell, endCell];
            return readRange.Value;
        }


        /// <summary>
        /// Permite leer de la hoja toda la matriz donde el usuario ha cambiado datos
        /// </summary>
        /// <param name="pagina">numero de la página que se desee leer</param>
        /// <param name="filaInicio">indice de fila en la que se encuentra la celda inicial del rango</param>
        /// <param name="ColumnaInicio">indice de columna en la que se encuentra la celda inicial del rango</param>
        /// <param name="FilaTermino">indice de fila en la que se encuentra la celda final del rango</param>
        /// <param name="ColumnaTermino">indice de columna en la que se encuentra la celda final del rango</param>
        /// <returns>retorna una matriz de objetos con todos los valores</returns>
        public dynamic LeerValorPorRangoUsado(int pagina)
        {
            var hoja = ObtenerHoja(pagina);
            Excel.Range range = hoja.UsedRange;
            var startCell = (Microsoft.Office.Interop.Excel.Range)hoja.Cells[1, 1];
            var endCell = range;
            var readRange = hoja.Range[startCell, endCell];
            return readRange.Value;
        }





        /// <summary>
        /// Obtiene un arreglo con todos los valores de la fila
        /// </summary>
        /// <param name="numeroPagina">numero de la hoja de excel</param>
        /// <param name="numFila">numero de la fila que se desea leer</param>
        /// <returns>un arreglo con todos los valores de la fila</returns>
        public dynamic ValoresPorFila(int numeroPagina, int numFila)
        {
            var hoja = ObtenerHoja(numeroPagina);
            var startCell = (Microsoft.Office.Interop.Excel.Range)hoja.Cells[numFila, 1];
            return startCell.EntireRow.Value2;
        }

        /// <summary>
        /// Obtiene un arreglo con todos los valores de la columna
        /// </summary>
        /// <param name="numeroPagina">numero de la hoja de excel</param>
        /// <param name="numFila">numero de la columna que se desea leer</param>
        /// <returns>un arreglo con todos los valores de la columna</returns>
        public dynamic ValoresPorColumna(int numeroPagina, int numColumna)
        {
            var hoja = ObtenerHoja(numeroPagina);
            var startCell = (Microsoft.Office.Interop.Excel.Range)hoja.Cells[1, numColumna];
            return startCell.EntireColumn.Value2;
        }




        public void AgregaHoja(string nombreHoja)
        {

            dynamic hoja = libro.Sheets.Add();
            hoja.Name = nombreHoja;

        }


        /// <summary>
        /// asigna un valor a la celda en la página especifica
        /// </summary>
        /// <param name="pagina">numero de página</param>
        /// <param name="celda">formato de celda en formato "A1", "B1", etc</param>
        /// <param name="value">valor</param>
        public void AsignarValor(int pagina, string celda, string value)
        {

            var hoja = ObtenerHoja(pagina);
            var cell = (Microsoft.Office.Interop.Excel.Range)hoja.get_Range(celda);
            cell.Value2 = value;

        }

        public void AsignarValor(int pagina, int fila, int columna, string value)
        {
            var hoja = ObtenerHoja(pagina);
            var celda = (Microsoft.Office.Interop.Excel.Range)hoja.Cells[fila, columna];
            celda.Value = value;
        }

        public void CombinarCeldas(int pagina, int filaCelda1, int columnaCelda1, int filaCelda2, int columnaCelda2)
        {
            var hoja = ObtenerHoja(pagina);
            var celda = (Microsoft.Office.Interop.Excel.Range)hoja.Cells[filaCelda1, columnaCelda1];
            var celda2 = (Microsoft.Office.Interop.Excel.Range)hoja.Cells[filaCelda2, columnaCelda2];
            var rango = hoja.get_Range(celda, celda2);
            rango.Merge(true);

        }


        /// <summary>
        /// permite enviar una matriz, la que se escribirá entre la celda de inicio y término ubicadas en als coordenadas enviadas como parámetro.
        /// </summary>
        /// <param name="pagina">num de página</param>
        /// <param name="filaInicio">fila de la celda de inicio</param>
        /// <param name="ColumnaInicio">columna de la celda de inicio</param>
        /// <param name="FilaTermino">fila de la celda de término</param>
        /// <param name="ColumnaTermino">columna de la celda de termino</param>
        /// <param name="datos">un arreglo de objetos cque contiene los datos que se desean guardar</param>
        public void AsignarValoresPoRango(int pagina, int filaInicio, int ColumnaInicio, int FilaTermino, int ColumnaTermino, object[,] datos)
        {
            var hoja = ObtenerHoja(pagina);
            var startCell = (Microsoft.Office.Interop.Excel.Range)hoja.Cells[filaInicio, ColumnaInicio];
            var endCell = (Microsoft.Office.Interop.Excel.Range)hoja.Cells[FilaTermino + 1, ColumnaTermino];
            var writeRange = hoja.Range[startCell, endCell];
            writeRange.Value2 = datos;

        }

        public void EstablecerHojaActiva(int pagina)
        {
            ObtenerHoja(pagina).Select(Type.Missing);


        }





        /// <summary>
        /// permite enviar una matriz, la que se escribirá entre la celda de inicio y término ubicadas en als coordenadas enviadas como parámetro.
        /// </summary>
        /// <param name="pagina">num de página</param>
        /// <param name="filaInicio">fila de la celda de inicio</param>
        /// <param name="ColumnaInicio">columna de la celda de inicio</param>
        /// <param name="FilaTermino">fila de la celda de término</param>
        /// <param name="ColumnaTermino">columna de la celda de termino</param>
        /// <param name="datos">una tabla que contiene los datos que se desean guardar</param>
        /// <param name="encabezados">indica si se desean escribir los encabezados de la tabla, esto se escriben siempre una fila más arriba</param>
        public void AsignarValoresPoRango(int pagina, int filaInicio, int ColumnaInicio, int FilaTermino, int ColumnaTermino, DataTable tabla, bool encabezados)
        {
            if (encabezados)
            {
                object[,] encanbezados = new object[1, tabla.Columns.Count];

                for (int i = 0; i < tabla.Columns.Count; i++)
                {
                    encanbezados[0, i] = tabla.Columns[i].ColumnName;
                }

                AsignarValoresPoRango(pagina, filaInicio - 1, ColumnaInicio, FilaTermino - 1, encanbezados.Length, encanbezados);
            }

            object[,] arr = new object[tabla.Rows.Count + 1, tabla.Columns.Count];

            for (int r = 0; r < tabla.Rows.Count; r++)
            {
                DataRow dr = tabla.Rows[r];
                for (int c = 0; c < tabla.Columns.Count; c++)
                {
                    arr[r, c] = dr[c];
                }
            }
            AsignarValoresPoRango(pagina, filaInicio, ColumnaInicio, FilaTermino, ColumnaTermino, arr);




        }





        /// <summary>
        /// Cambia el nombre de la joha especificada por su índice
        /// </summary>
        /// <param name="numPagina">numero de página</param>
        /// <param name="NombreHoja">el nuevo nombre de la página</param>
        public void CambiarNombreHoja(int numPagina, string NombreHoja)
        {
            var hoja = ObtenerHoja(numPagina);
            hoja.Name = NombreHoja;

        }




        /// <summary>
        /// refresca la data de la tabla ubicada en la número de página especificado
        /// </summary>
        /// <param name="pagina"></param>
        public void RefrescarTablaDinamica(int pagina)
        {
            var hoja = ObtenerHoja(pagina);
            hoja.PivotTables(1).RefreshTable();

        }






        /// <summary>
        /// Permite guardar la planilla en meoria en la ruta seleccionada
        /// </summary>
        /// <param name="ruta"></param>
        public void guardarComo(string ruta)
        {
            if (File.Exists(ruta))
            {
                File.Delete(ruta);
            }
            libro.SaveAs(ruta);

        }

        /// <summary>
        /// quita el proceso excel.exe de la memoria.
        /// </summary>
        public void Dispose()
        {
            excelApp.Workbooks.Close();
            Marshal.ReleaseComObject(libro);

            excelApp.Quit();
            Marshal.ReleaseComObject(excelApp);
            excelApp.Quit();

            Marshal.ReleaseComObject(excelApp);
        }
    }
}
