using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Uma
{
    [Table("uma_mantenimiento_CalendarioOperacionesPermisos")]
    public class CalendarioOperacionesPermisos
    {
        [Key]
        public int Id { get; set; }

        public string Usuario { get; set; }

        public bool GenerarOt { get; set; }
    }
}
