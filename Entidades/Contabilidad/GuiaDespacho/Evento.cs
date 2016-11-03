using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Contabilidad.GuiaDespacho
{
    [Table("cont_guia_despacho_evento")]
   public class Evento
    {
        [Key]        
        public int Id { get; set; }

        public string Usuario { get; set; }

        public string Latitud { get; set; }

        public string Longitud { get; set; }

        public int IdCabecera { get; set; }

        public int IdSituacion { get; set; }

        public string Observaciones { get; set; }

        public bool ContieneImagen { get; set; }

      

        public DateTime Fecha { get; set; }

        [ForeignKey("IdCabecera")]
        public virtual GuiaDespacho GuiaDespacho { get; set; }

        [ForeignKey("IdSituacion")]
        public virtual Situacion Situacion { get; set; }




        


    }
}
