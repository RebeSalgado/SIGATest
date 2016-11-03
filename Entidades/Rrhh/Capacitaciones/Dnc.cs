using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Rrhh.Capacitaciones
{
    [Table("rrhh_dnc_registro")]
    public class Dnc
    {
        public int Id { get; set; }

        [Display(Name = "Sub proyecto")]
        public int IdSubproyecto { get; set; }

        [Display(Name = "C. de responsabilidad")]
        public int CreCodigo { get; set; }

        [Display(Name = "Autor")]
        public string usuarioCreador { get; set; }

        [Display(Name = "Año")]
        public int anio { get; set; }

        [Display(Name = "Finalizada")]
        public bool Cerrada { get; set; }

        [ForeignKey("IdSubproyecto")]
        [Display(Name = "Sub proyecto")]
        public Sys.SubProyecto SubProyecto { get; set; }





    }
}
