using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Uma
{

    [Table("uma_mantenimiento_tipoRecurso")]
    public class TipoRecurso
    {
        [Key]
        [Required]
        public int id { get; set; }
        [MaxLength(150)]
        [Required]
        public string tipoRecurso { get; set; }

    }
}
