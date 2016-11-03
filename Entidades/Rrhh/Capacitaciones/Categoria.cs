using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Rrhh.Capacitaciones
{
    [Table("rrhh_capacitacion_categoria")]
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string ValorCcc { get; set; }
    }
}
