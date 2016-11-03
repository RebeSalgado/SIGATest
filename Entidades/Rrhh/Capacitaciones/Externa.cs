using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Rrhh.Capacitaciones
{
    [Table("rrhh_capacitaciones_Externa")]
   public class Externa
    {
        public int Id { get; set; }

        [DisplayName("SubProyecto")]
        public int IdSubproyecto { get; set; }

        public string Otec { get; set; }

        public string NombreCurso { get; set; }
        
        public int CantidadHoras { get; set; }

        public DateTime FechaInicio { get; set;}

        [DisplayName("Categoria")]
        public int IdCategoria {get; set;}

        public string ContactoOtec { get; set; }

        public string Usuario { get; set; }

        public string Estado { get; set; }


        [DisplayName("SubProyecto")]
        [ForeignKey("IdSubproyecto")]
        public Sys.SubProyecto SubProyecto { get; set; }

        [DisplayName("Categoria")]
        [ForeignKey("IdCategoria")]
        public Capacitaciones.Categoria Categoria { get; set; }
        
    }
}
