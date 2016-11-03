using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Uma
{
    [Table("uma_mantenimiento_recursoActividades")]
    public class Recurso
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int idActividad { get; set; }
        [Required]
        public int idTipoRecurso { get; set; }
        [Required]
        public int idRecurso { get; set; }
        [Required]
        public decimal cantidad { get; set; }

        [ForeignKey("idActividad")]
        public virtual Actividades Actividades { get; set; }

        [ForeignKey("idTipoRecurso")]
        public virtual TipoRecurso TipoRecurso { get; set; }

    }
}
