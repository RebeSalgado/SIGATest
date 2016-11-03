using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Rrhh.Capacitaciones
{
    [Table("rrhh_dnc_tipoCurso")]
    public class TipoCapacitacion
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }

    }
}
