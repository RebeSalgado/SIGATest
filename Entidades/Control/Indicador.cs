using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Control
{
    [Table("Control_GestionIndicadores_Indicador")]
    public class Indicador
    {
        [Key]
        public int Id_Indicador { get; set; }

        [Required(ErrorMessage = "El Nombre del Indicador es necesario.")]
        public string Nombre { get; set; }
        public int IdArea { get; set; }
        public int IdNivel { get; set; }
        public int IdTurnos { get; set; }
        public string Frecuencia { get; set; }
        public int IdProyecto { get; set; }
        

        [ForeignKey("IdArea")]
        public virtual Area Area { get; set; }
        [ForeignKey("IdNivel")]
        public virtual Nivel_Control Nivel_Control { get; set; }
        [ForeignKey("IdTurnos")]
        public virtual Turno Turno { get; set; }
        [ForeignKey("IdProyecto")]
        public virtual Proyecto Proyecto { get; set; }



    }
}
