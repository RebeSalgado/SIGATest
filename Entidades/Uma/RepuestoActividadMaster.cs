using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Uma
{
    [Table("uma_mantenimiento_RepuestosActividadesMaster")]
    public class RepuestosActividadesMaster
    {
        [Key]
        public int iD { get; set; }

        public int idActividad { get; set; }

        public int idRecurso { get; set; }

        public decimal Cantidad { get; set; }

        //[ForeignKey("idActividades")]
        //public virtual Actividades Actividades { get; set; }
    }
}
