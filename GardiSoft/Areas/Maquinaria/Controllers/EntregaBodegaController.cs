using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardiSoft.Areas.Maquinaria.Controllers
{
    public class EntregaBodegaController : Controller
    {
        // GET: Maquinaria/EntregaBodega
        Models.GardiSoftContext db = new Models.GardiSoftContext();


        public ActionResult Index(int id= 0)
        {
            if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            ViewBag.Id = id;
            return View();
        }

        public JsonResult VerVales(int idOt = 0)
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_verValesEmitidosPorOt",
              CommandType.StoredProcedure,
              new System.Data.SqlClient.SqlParameter("idOT", idOt));             
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerConsumoVale(int numeroPedido = 0)
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_verValesEmitidosPorNumeroPedido",
              CommandType.StoredProcedure,
              new System.Data.SqlClient.SqlParameter("numeroPedido", numeroPedido));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerEntregasOt(int idOt = 0, int idBodega = 0)
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_entregaMateriales",
              CommandType.StoredProcedure,
              new System.Data.SqlClient.SqlParameter("idOT", idOt),
              new System.Data.SqlClient.SqlParameter("idBodega", idBodega));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerInformacionEntrega(int idOt = 0)
        {
            var ot = db.Ot.FirstOrDefault(x => x.Id == idOt);
            return Json(ot, JsonRequestBehavior.AllowGet);
            //DataTable tabla = new Models.Conectar()
            //  .EjecutarConsultaSelect("sp_uma_mantenimiento_Fin700MaquinaCentroConsumo",
            //  CommandType.StoredProcedure,
            //  new System.Data.SqlClient.SqlParameter("idOT", idOt));
            //string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            //return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerAlternativos(string codigo = "REP-MANITU-3861", int idBodega = 0)
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_RepuestosOriginalesAlternativos",
              CommandType.StoredProcedure,
              new System.Data.SqlClient.SqlParameter("codigo", codigo),
               new System.Data.SqlClient.SqlParameter("idBodega", idBodega));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        #region crearPedidoFin700

        public class ProductoSolicitado
        {
            public int Id { get; set; }
            public int IdAlternativo { get; set; }
            public int Unidad { get; set; }
            public int Cantidad { get; set; }
            public int idDetalle { get; set; }
            public int IdProductoInterno { get; set; }
          


        }


        string lastMessage = "";

        private int ObtenerValorProducto(int idProducto, int idConsumo)
        {
            DataTable tabla = new Models.Conectar()
            .EjecutarConsultaSelect("sp_uma_mantenimiento_valorProducto",
            CommandType.StoredProcedure,
            new System.Data.SqlClient.SqlParameter("productoId", idProducto),
            new System.Data.SqlClient.SqlParameter("@idConsumo", idConsumo));
            if (tabla.Rows.Count > 0)
            {
                return int.Parse(tabla.Rows[0][0].ToString());
            }
            else
                return 0;
           
            
        }


        public JsonResult GuardarConsumoFin700(int idbodega, int pcreId, int cconsumoId, List<ProductoSolicitado> productos, int idOt)
        {
            try
            {
                //Se obtiene le centro de responsabilidad de la bodega
                DataTable tabla = new Models.Conectar()
                .EjecutarConsultaSelect("sp_uma_mantenimiento_CentroResponsabilidadBodega",
                CommandType.StoredProcedure,
                new System.Data.SqlClient.SqlParameter("idBodega", idbodega));

                //reemplaza el centro de responsabilidad de la máquina, por que al hacer una salida de bodega, el centro que menda es el de la bodega de salida
                pcreId = int.Parse(tabla.Rows[0][0].ToString());

                var numeroPedido=  GuardarCabeceraPedido(30, idbodega, productos, "300", "200", pcreId.ToString(), "Pedido realizado por SIGA", cconsumoId);
              //  var numeroPedido = new Random().Next(0,int.MaxValue).ToString();

                var idTipoOt = db.Ot.FirstOrDefault(x => x.Id == idOt).IdTipoOt;

                if (numeroPedido !=  "-1")
                {

                    foreach (var item in productos)
                    {
                        if (idTipoOt != 2) //si es correctiva
                        {
                            db.RepuestoCorrectivosEntregados.Add(new Entidades.Uma.RepuestoCorrectivosEntregados
                            {
                                Cantidad = item.Cantidad,
                                IdDetalleCorrectivo = item.idDetalle,
                                IdRecurso = item.Id,
                                idRecursoOriginal = item.IdProductoInterno,
                                NumeroPedido = int.Parse(numeroPedido),
                                FechaEntrega = DateTime.Now,
                                Valor = ObtenerValorProducto(item.Id, int.Parse(numeroPedido))

                            });
                        }
                        else
                        {
                            db.OtRepuestosEntregados.Add(new Entidades.Uma.OtRepuestosEntregados
                            {
                                Cantidad = item.Cantidad,
                                IdRecurso = item.Id,
                                IdRecursoOriginal = item.IdProductoInterno,
                                NumeroPedido = int.Parse(numeroPedido),
                                FechaEntrega = DateTime.Now,
                                IdDetalleCorrectivo = item.idDetalle,
                                Valor = ObtenerValorProducto(item.Id, int.Parse(numeroPedido))

                            });
                        }
                    }


                    db.SaveChanges();


                    //parche, ejecuta en SP que regula los precios, a veces los valores del fin700 viene en cero, así que busco el último valor de compra



                    return Json(new { Numero = numeroPedido, Fecha = DateTime.Now.ToShortDateString(), CconsumoId = cconsumoId }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Numero = "0", Fecha = DateTime.Now.ToShortDateString(), CconsumoId = 0, MensajeFin = lastMessage }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                return Json(new { Numero = "0", Fecha = DateTime.Now.ToShortDateString(), CconsumoId = 0, MensajeFin = lastMessage }, JsonRequestBehavior.AllowGet);
            }

        }


        private string GuardarCabeceraPedido(int division, int bodega, IEnumerable<ProductoSolicitado> productos, string subProyecto, string numeroProyecto, string pCreId, string glosa, int cconsumo)
        {
            var idPedido = 0;

            //declare @p18 numeric(32,12)
            //set @p18=0
            //exec ExiDsp_ActDespSinPed @EmpId=1.000000000000,@DivCodigo=20,@UniCodigo=1,@pTipoOpeId=7,@FechaProceso='2014-11-25 00:00:00',@ConEstCod=0,@pCConsumoId=36,@pBodegaId=59,
            //@pBodOrigenId=0,@PedidoTipoRecepcion=1,@GlosaExis='ruta3',@PerId=0,@LogOwner='10267               ',
            //@ProNegocioCod=31,@Id_Usuario='ADM',@Id_Funcionalidad='EXIOPEDESPCON',@pConvenioCabId=0,@OpeLogtCabId=@p18 output
            //select @p18


            //obtiene el centro de consumo a partir de la máquina, si no está no puede realizarse la operacion


            // comprueba el stock de todos los productos antes de guardarlos;
            foreach (var item in productos)
            {
                var mensajeStock = Stock(item.IdAlternativo.ToString(), bodega, item.Cantidad);
                if (mensajeStock.Length > 0)
                {
                    //hay mensaje de 'sin stock'                
                    return "-1";
                }

            }


            string numeroPedido = GuardaCabecera(division, cconsumo, bodega, glosa);
            if (numeroPedido == "-1")
            {
                lastMessage = "Error desconocido";
                return "-1";
            }
            string idContable = ObtenerIdContable(numeroPedido);
            idContable = GuardarTipoNumeroProyecto(idContable, numeroPedido, subProyecto, numeroProyecto, pCreId);

            idPedido = int.Parse(numeroPedido);


            int linea = 0;
            foreach (var item in productos)
            {
                linea++;
                AgregarDetalle(numeroPedido, item.IdAlternativo.ToString(), item.Cantidad, item.Unidad.ToString(), bodega, subProyecto, numeroProyecto, linea, pCreId);
            }



            AprobarPedido(numeroPedido);
            return numeroPedido;
            //return "Despacho realizado";






        }

        public object AprobarPedido(string pedido)
        {
            var c = new Models.Conectar("fin700"); ;
            #region comprueba stock, pero el SP siempre retorna cero


            // //exec ExiDes_ValidaSaldoProducto @CodOwner='10267               ',@OpeLogtCabId=48787.000000000000,@Id_Usuario='ADM',@Id_Funcionalidad='EXIOPEDESPCON',@xStatus=@p5 output

            //  try
            // {
            //     c.Abrir();
            //     SqlCommand command = new SqlCommand();
            //     command.CommandType = CommandType.StoredProcedure;
            //     command.CommandText = "ExiDes_ValidaSaldoProducto";
            //     command.Connection = c.Coneccion;
            //     command.Parameters.AddWithValue("CodOwner", 10267);
            //     command.Parameters.AddWithValue("OpeLogtCabId", pedido);
            //     command.Parameters.AddWithValue("Id_Usuario", "ADM");
            //     command.Parameters.AddWithValue("Id_Funcionalidad", "EXIOPEDESPCON");
            //     SqlParameter p = new SqlParameter("xStatus", SqlDbType.Int);
            //     p.Value = 5;
            //     command.Parameters.Add(p);

            //     command.ExecuteNonQuery();
            //     c.Cerrar();



            //    // return "1";
            // }
            // catch (Exception)
            // {
            //     c.Cerrar();
            //     return "-1";
            // }


            #endregion



            //exec ExiDes_ProcesoDespacho01 @CodOwner='10267               ',@TdoId=0,@DlcFolioDocto=48787,@DlcFecDocto='2014-12-02 00:00:00',
            //@Id_Usuario='ADM',@Id_Funcionalidad='EXIOPEDESPCON',@OpeLogtCabId=48787.000000000000


            //lastMessage = "";
            try
            {
                c.Abrir();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ExiDes_ProcesoDespacho01";
                command.Connection = c.Coneccion;
                command.Parameters.AddWithValue("CodOwner", 10267);
                command.Parameters.AddWithValue("TdoId", 0);
                command.Parameters.AddWithValue("DlcFolioDocto", pedido);
                command.Parameters.AddWithValue("DlcFecDocto", System.DateTime.Now);
                command.Parameters.AddWithValue("Id_Usuario", "ADM");
                command.Parameters.AddWithValue("Id_Funcionalidad", "EXIOPEDESPCON");
                command.Parameters.AddWithValue("OpeLogtCabId", pedido);
                //c.Coneccion.InfoMessage += Coneccion_InfoMessage;
                command.ExecuteNonQuery();
                c.Cerrar();
                //if (lastMessage.Length > 0)
                //{
                //    //error de stock
                //    return lastMessage;
                //}
                return new
                {
                    resultado = "Despacho realizado",
                    numero = pedido

                };

            }
            catch (Exception)
            {
                c.Cerrar();
                return new
                {
                    resultado = "Error al generar el despacho",
                    numero = 0

                }; ;
            }




        }



        private string Stock(string idProducto, int bodega, int cantidad)
        {
            var c = new Models.Conectar("fin700");

            try
            {
                c.Abrir();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ExiDsp_ValStockDsp";
                command.Connection = c.Coneccion;
                command.Parameters.AddWithValue("pProductoId", idProducto);
                command.Parameters.AddWithValue("pBodegaId", bodega);
                command.Parameters.AddWithValue("pTipoOpeId", 7);
                command.Parameters.AddWithValue("CantSalidaStock", cantidad);
                c.Coneccion.InfoMessage += Coneccion_InfoMessage;
                command.ExecuteNonQuery();
                c.Cerrar();

                return lastMessage;
                // return "1";
            }
            catch (Exception)
            {
                c.Cerrar();
                return "-1";
            }

        }

        private string GuardaCabecera(int division, int cconsumo, int bodega, string glosa)
        {
            var c = new Models.Conectar("fin700"); ;

            try
            {
                c.Abrir();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ExiDsp_ActDespSinPed";
                command.Connection = c.Coneccion;
                command.Parameters.AddWithValue("EmpId", "1");
                command.Parameters.AddWithValue("DivCodigo", division);
                command.Parameters.AddWithValue("UniCodigo", "1");
                command.Parameters.AddWithValue("pTipoOpeId", "7");
                command.Parameters.AddWithValue("FechaProceso", DateTime.Now.ToString("yyyyMMdd"));
                command.Parameters.AddWithValue("ConEstCod", "0");
                command.Parameters.AddWithValue("pCConsumoId", cconsumo);
                command.Parameters.AddWithValue("pBodegaId", bodega);
                command.Parameters.AddWithValue("pBodOrigenId", "0");
                command.Parameters.AddWithValue("PedidoTipoRecepcion", "1");
                command.Parameters.AddWithValue("GlosaExis", glosa);
                command.Parameters.AddWithValue("PerId", "0");
                command.Parameters.AddWithValue("LogOwner", "10267");
                command.Parameters.AddWithValue("ProNegocioCod", "31");
                command.Parameters.AddWithValue("Id_Usuario", "ADM");
                command.Parameters.AddWithValue("Id_Funcionalidad", "EXIOPEDESPCON");
                command.Parameters.AddWithValue("pConvenioCabId", "0");

                SqlParameter p18 = new SqlParameter("OpeLogtCabId", SqlDbType.Decimal);
                p18.Value = null;
                p18.Direction = ParameterDirection.Output;

                command.Parameters.Add(p18);

                command.ExecuteNonQuery();


                c.Cerrar();
                return p18.Value.ToString();
            }
            catch (Exception ex)
            {
                c.Cerrar();
                return "-1";
            }
        }

        public string ObtenerIdContable(string numPedido)
        {


            var c = new Models.Conectar("fin700"); ;

            try
            {
                c.Abrir();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ExiMas_DevMasLeeOpeCabCont";
                command.Connection = c.Coneccion;

                command.Parameters.AddWithValue("pOpeLogtCabId", numPedido);

                // command.Parameters.AddWithValue("pCreId", 2);
                SqlParameter pCreId = new SqlParameter("pCreId", SqlDbType.Float);
                // pCreId.Value = 2;
                pCreId.Direction = ParameterDirection.Output;
                command.Parameters.Add(pCreId);



                SqlParameter p3 = new SqlParameter("pCdiId", SqlDbType.Float);
                p3.Direction = ParameterDirection.Output;
                command.Parameters.Add(p3);


                SqlParameter p4 = new SqlParameter("pTprId", SqlDbType.Float);
                p4.Direction = ParameterDirection.Output;
                command.Parameters.Add(p4);

                SqlParameter p5 = new SqlParameter("PryNumero", SqlDbType.VarChar, 8000);
                p5.Direction = ParameterDirection.Output;
                command.Parameters.Add(p5);

                SqlParameter p6 = new SqlParameter("pPartidaId", SqlDbType.Float);
                p6.Direction = ParameterDirection.Output;
                command.Parameters.Add(p6);

                SqlParameter p7 = new SqlParameter("OpeLogtCabContId", SqlDbType.Decimal);
                p7.Direction = ParameterDirection.Output;
                command.Parameters.Add(p7);

                SqlParameter p8 = new SqlParameter("TdoId", SqlDbType.Float);
                p8.Direction = ParameterDirection.Output;
                command.Parameters.Add(p8);

                SqlParameter p9 = new SqlParameter("NumeroDocumento", SqlDbType.Float);
                p9.Direction = ParameterDirection.Output;
                command.Parameters.Add(p9);

                SqlParameter p10 = new SqlParameter("FechaDocumento", SqlDbType.DateTime);
                p10.Direction = ParameterDirection.Output;
                command.Parameters.Add(p10);


                command.ExecuteNonQuery();


                c.Cerrar();
                return p7.Value.ToString();
            }
            catch (Exception)
            {
                c.Cerrar();
                return "-1";
            }




        }

        void Coneccion_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            lastMessage = e.Message;
        }

        public string GuardarTipoNumeroProyecto(string idContable, string idPedido, string TipoProyecto, string numeroProyecto, string pcreId)
        {
            //declare @p12 numeric(32,12)
            //set @p12=48789.000000000000
            //exec ExiMas_DevMasGuardaOpeCabC @pOpeLogtCabId=48773,@pCreId=146,@pCdiId=31,@pTprId=300,@PryNumero='200',@pPartidaId=0,@TdoId=0,@NumeroDocumento=0,
            //@FechaDocumento='1900-01-01 00:00:00',@Id_Usuario='ADM',@Id_Funcionalidad='EXIOPEDESPCON',@OpeLogtCabContId=@p12 output
            //select @p12


            var c = new Models.Conectar("fin700"); ;

            try
            {
                c.Abrir();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ExiMas_DevMasGuardaOpeCabC";
                command.Connection = c.Coneccion;
                command.Parameters.AddWithValue("pOpeLogtCabId", idPedido);
                command.Parameters.AddWithValue("pCreId", pcreId);
                command.Parameters.AddWithValue("pCdiId", 31);
                command.Parameters.AddWithValue("pTprId", TipoProyecto);
                command.Parameters.AddWithValue("PryNumero", numeroProyecto);
                command.Parameters.AddWithValue("pPartidaId", 0);
                command.Parameters.AddWithValue("TdoId", 0);
                command.Parameters.AddWithValue("NumeroDocumento", 0);
                command.Parameters.AddWithValue("FechaDocumento", "1900-01-01");
                command.Parameters.AddWithValue("Id_Usuario", "ADM");
                command.Parameters.AddWithValue("Id_Funcionalidad", "EXIOPEDESPCON");
                SqlParameter p12 = new SqlParameter("OpeLogtCabContId", SqlDbType.Decimal);
                p12.Value = Decimal.Parse(idContable);
                p12.Direction = ParameterDirection.InputOutput;
                command.Parameters.Add(p12);
                command.ExecuteNonQuery();
                c.Cerrar();
                return p12.Value.ToString();
            }
            catch (Exception)
            {
                c.Cerrar();
                return "-1";
            }




        }


        public string AgregarDetalle(string idPedido, string idProducto, int cantidadSalida, string tipoUnidad, int bodega, string tipoProyecto, string numeroProyecto, int linea, string pcreid)
        {


            var c = new Models.Conectar("fin700"); ;

            try
            {
                c.Abrir();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ExiMas_DevMasGuardaDetall3";
                command.Connection = c.Coneccion;
                command.Parameters.Add("pOpeLogtCabId", idPedido);
                command.Parameters.Add("pDocOrigenDetId", "0");
                command.Parameters.Add("linea", linea);
                command.Parameters.Add("pProductoId", idProducto);
                command.Parameters.Add("SituacionContCod", "1");
                command.Parameters.Add("IndicaSerie", "1");
                command.Parameters.Add("IndicaLote", "1");
                command.Parameters.Add("pBodDestinoId", bodega);
                command.Parameters.Add("pUbicacionId", "0");
                command.Parameters.Add("pLoteProductoId", "0");
                command.Parameters.Add("IndDescripDoc", "0");
                command.Parameters.Add("CantBodegaOrigen", "0");
                command.Parameters.Add("CantDocOrigen", "0");
                command.Parameters.Add("CantEntrada", "0");
                command.Parameters.Add("CantSalida", cantidadSalida);
                command.Parameters.Add("pUnidadMedOrigenId", tipoUnidad);
                //@CantEntrada2=0,@CantSalida2=0,@pUnidadMed2Id=0,@UnidadMedConversion=0,@UnidadMedMultDivide=0,
                command.Parameters.Add("CantEntrada2", "0");
                command.Parameters.Add("CantSalida2", "0");
                command.Parameters.Add("pUnidadMed2Id", "0");
                command.Parameters.Add("UnidadMedConversion", "0");
                command.Parameters.Add("UnidadMedMultDivide", "0");
                //@pUnidadMedStockId=104,@Cantidad=0,@CostoMoneda=0,@CostoMonedaAlt=0,@ValorUniOpeMoneda=0,@ValorUniOpeMonedaAlt=0,@ValorUniOpeMonedaOrigen=0,
                command.Parameters.Add("pUnidadMedStockId", tipoUnidad);
                command.Parameters.Add("Cantidad", "0");
                command.Parameters.Add("CostoMoneda", "0");
                command.Parameters.Add("CostoMonedaAlt", "0");
                command.Parameters.Add("ValorUniOpeMoneda", "0");
                command.Parameters.Add("ValorUniOpeMonedaAlt", "0");
                command.Parameters.Add("ValorUniOpeMonedaOrigen", "0");
                //@pMonedaOrigenId=0,@TasaCambio=0,@UsuarioProceso=' ',@FechaProceso='2014-11-25 00:00:00',@UsuarioAprobacion=' ',@FechaAprobacion='1900-01-01 00:00:00'
                command.Parameters.Add("pMonedaOrigenId", tipoUnidad);
                command.Parameters.Add("TasaCambio", "0");
                command.Parameters.Add("UsuarioProceso", "0");
                command.Parameters.Add("FechaProceso", System.DateTime.Now);
                command.Parameters.Add("UsuarioAprobacion", " ");
                command.Parameters.Add("FechaAprobacion", "1900-01-01");
                //,@UsuarioAnula=' ',@pCreId=146,@pCdiId=0,@pTprId=300,@PryNumero='200                 ',@pPartidaId=0,@FlagValidaRebajaSerie=0,@Id_Usuario='ADM',
                command.Parameters.Add("UsuarioAnula", " ");
                command.Parameters.Add("pCreId", pcreid);
                command.Parameters.Add("pCdiId", "0");
                command.Parameters.Add("pTprId", tipoProyecto);
                command.Parameters.Add("PryNumero", numeroProyecto);
                command.Parameters.Add("pPartidaId", "0");
                command.Parameters.Add("FlagValidaRebajaSerie", "0");
                command.Parameters.Add("Id_Usuario", "ADM");
                //@Id_Funcionalidad='EXIOPEDESPCON',@ValorOpeMoneda=0,@ValorOpeMonedaAlt=0,@ItemDet=0,@CantEntradaStock=0,@CantSalidaStock=1
                command.Parameters.Add("Id_Funcionalidad", "EXIOPEDESPCON");
                command.Parameters.Add("ValorOpeMoneda", "0");
                command.Parameters.Add("ValorOpeMonedaAlt", "0");
                command.Parameters.Add("ItemDet", "0");
                command.Parameters.Add("CantEntradaStock", "0");
                command.Parameters.Add("CantSalidaStock", cantidadSalida);
                //,@OpeLogtDetId=@p49 output,@OpeLogtDetContId=@p50 output,@OpeLogtDetSerLotId=@p51 output,@xFlagValidaBodega=@p52 output

                SqlParameter p1 = new SqlParameter("OpeLogtDetId", SqlDbType.Decimal);
                p1.Direction = ParameterDirection.Output;
                command.Parameters.Add(p1);

                SqlParameter p2 = new SqlParameter("OpeLogtDetContId", SqlDbType.Decimal);
                p2.Direction = ParameterDirection.Output;
                command.Parameters.Add(p2);

                SqlParameter p3 = new SqlParameter("OpeLogtDetSerLotId", SqlDbType.Decimal);
                p3.Direction = ParameterDirection.Output;
                command.Parameters.Add(p3);

                SqlParameter p4 = new SqlParameter("xFlagValidaBodega", SqlDbType.Int);
                p4.Direction = ParameterDirection.Output;
                command.Parameters.Add(p4);






                command.ExecuteNonQuery();
                c.Cerrar();
                return "1";
            }
            catch (Exception)
            {
                c.Cerrar();
                return "-1";
            }



        }


        #endregion


    }
}