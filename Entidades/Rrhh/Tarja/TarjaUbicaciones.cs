using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Rrhh.Tarja
{
    [Table("rrhh_tarja_ubicaciones")]
    public class TarjaUbicaciones
    {
        [Key]
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public int IdSubproyecto { get; set; }

        [ForeignKey("IdSubproyecto")]
        public Sys.SubProyecto SubProyecto{get;set;}

    }
}
