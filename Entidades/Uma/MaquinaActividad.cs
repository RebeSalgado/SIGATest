using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Uma
{
    [Table("uma_mantenimiento_MaquinaActividades")]
   public class MaquinaActividad
    {
        [Key]
        public int Id { get; set; }

        public int IdMaquina { get; set; }

        public int IdActividad { get; set; }

        public int? UltimoHorometroMantenimiento { get; set; }

        public DateTime? UltimaFechaMantenimiento { get; set; }

        [ForeignKey("IdMaquina")]
        public Maquina Maquina { get; set; }

        [ForeignKey("IdActividad")]
        public Actividades Actividad { get; set; }

    }
}
