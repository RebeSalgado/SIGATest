using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Rrhh.Capacitaciones
{
    [Table("rrhh_Capacitacion_Interna")]
     public  class Capacitacion
    {
        public int Id { get; set; }

        public int idTipoEFC { get; set; }        

        public int CantidadHoras { get; set; }

        public DateTime FechaInicio { get; set; }
    
        public string Usuario { get; set; }

        public string Nombre { get; set; }

        public string Relator { get; set; }

        public string Ubicacion { get; set; }

        public string Estado { get; set; }

        public int IdSubproyecto { get; set; }

        public string Descripcion { get; set; }

        [ForeignKey("idTipoEFC")]
        public TipoEfc TipoEfc { get; set; }

        [ForeignKey("IdSubproyecto")]
        public Sys.SubProyecto SubProyecto { get; set; }
        

    }
}
