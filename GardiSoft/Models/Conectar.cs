using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq.Expressions;

namespace GardiSoft.Models
{
    /// <summary>
    /// permite conectarse a la base de datos usando el enfoque tradicional.
    /// </summary>
    public class Conectar : IDisposable
    {

        public SqlConnection Coneccion { get; set; }
        public SqlTransaction Transaccion { get; set; }


        public Conectar(string db)
        {
            var cadena = ConfigurationManager.ConnectionStrings[db].ConnectionString;

            
            Coneccion = new SqlConnection(cadena);
           
        }

        public Conectar()
        {
            var cadena = ConfigurationManager.ConnectionStrings["GardiSoftConnection"].ConnectionString;
            Coneccion = new SqlConnection(cadena);            
        }

      

        public void Abrir()
        {
            if (Coneccion.State != ConnectionState.Open)
            {
                Coneccion.Open();
            }
        }



        public void Cerrar()
        {
            if (Transaccion == null)
            {
                //cmd.Transaction = Transaccion;
                Coneccion.Close();
            }

        }


        public void Dispose()
        {
            Coneccion.Close();
        }


        public DataTable EjecutarConsultaSelect(string sql, CommandType tipo, params SqlParameter[] parametros)
        {
            var cmd = Coneccion.CreateCommand();

            cmd.CommandTimeout = int.MaxValue;
            if (Transaccion != null)
            {
                cmd.Transaction = Transaccion;
            }
            cmd.CommandText = sql;
            cmd.CommandType = tipo;

            if (parametros != null)
            { 
            cmd.Parameters.AddRange(parametros);
            }


            DataTable tabla = new DataTable("subproyectos");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(tabla);
            this.Cerrar();
          
            return tabla;
        }


        public int EjecutarConsultaNoSelect(string sql)
        {

            int resultado = 0;
            try
            {
                var cmd = Coneccion.CreateCommand();
                if (Transaccion != null)
                {
                    cmd.Transaction = Transaccion;
                }
                cmd.CommandText = sql;
                Abrir();
                resultado = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Cerrar();
                throw ex;

            }

            Cerrar();
            return resultado;


        }

        public string QueryExcel(string sp,string ruta,string nombreArchivo,params SqlParameter[] parametros)
        {
            var tabla = EjecutarConsultaSelect(sp, CommandType.StoredProcedure,parametros);

            var plantilla = new ExcelWriter();
            var r = plantilla.TablaToExcel(nombreArchivo,                
               ruta, 
                tabla);

            plantilla.CerrarExcel();

            return r;
        }

    }
}


