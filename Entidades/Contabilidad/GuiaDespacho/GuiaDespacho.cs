using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Contabilidad.GuiaDespacho
{
    [Table("cont_guia_despacho")]
    public class GuiaDespacho
    {
        [Key]
        public int Id { get; set; }
        public string TipoDocumento { get; set; }
        public string FolioDocumento { get; set; }
        public string FechaEmision { get; set; }
        public  string TipoDespacho { get; set; }
        public string FormaDePago { get; set; }
        public string FechaVencimiento { get; set; }
        public string IndicadorTipoTraslado { get; set; }
        public string Observaciones { get; set; }
        public string rutEmisor { get; set; }
        public string RazonSocial { get; set; }
        public string GiroNegocio { get; set; }
        public string CodigoActividadEconomica { get; set; }
        public string CodigoSucursalSII { get; set; }
        public string DireccionOrigen { get; set; }
        public string ComunaOrigen { get; set; }
        public string CiudadOrigen { get; set; }
        public string CodigoDelVendedor { get; set; }
        public string RutReceptor { get; set; }
        public string CodigoInternoReceptor { get; set; }
        public string GiroReceptor { get; set; }
        public string DireccionReceptor { get; set; }
        public string ComunaReceptor { get; set; }
        public string CiudadReceptor { get; set; }
        public string emalReceptor { get; set; }
        public string Subtotal { get; set; }
        public string TotalDescRec { get; set; }
        public string totalNeto { get; set; }
        public string MontoExento { get; set; }
        public string MontoIva { get; set; }
        public string MontoTotal { get; set; }
        public string TotalAPagar { get; set; }
        public string MontoEscrito { get; set; }
        public string PorcentajeRetencion { get; set; }
        public string MontoRetencion { get; set; }
        public string Patente { get; set; }
        public string RutTransportista { get; set; }
        public string DireccionDestino { get; set; }
        public string ComunaDestino { get; set; }
        public string CiudadDestino { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
       

        public virtual ICollection<Referencia> Referencias { get; set; }

        public virtual ICollection<Detalle> Detalles { get; set; }





    }
}
