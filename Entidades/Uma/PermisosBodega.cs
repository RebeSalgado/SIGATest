using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Uma
{
    [Table("uma_mantenimiento_PermisosBodega")]
    public class PermisosBodega
    {
        [Key]
        public int Id { get; set; }

        public string Usuario { get; set; }

        public int Bodega { get; set; }

        public string Rol { get; set; }
    }
}
