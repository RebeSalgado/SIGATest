using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Uma
{
   [Table("uma_mantenimiento_OtDetalle")]
   public class DetalleCorrectivo
    {
        [Key]
        public int Id { get; set; }

        public int IdOt { get; set; }

        public int IdActividad { get; set; }

        public int Progreso { get; set; }

        [ForeignKey("IdOt")]
        public OT Cabecera { get; set; }

        [ForeignKey("IdActividad")]
        public Actividades Actividad { get; set; }



        



    }



   


}
