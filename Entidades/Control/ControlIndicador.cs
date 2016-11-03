using Entidades.Sys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Control
{
    [Table("Control_GestionIndicadores_ControlIndicador")]
    public class ControlIndicador
    {
        [Key]
        public int Id_Control { get; set; }
        public int IdIndicador { get; set; }

        [Required(ErrorMessage = "El Valor de Real es necesaria.")]
        public string Nreal { get; set; }

        [Required(ErrorMessage = "El Valor de Desviación es necesaria.")]
        public string Desviacion { get; set; }

        [Required(ErrorMessage = "La Fecha es necesaria.")]
        public DateTime fecha { get; set; }

        [Required(ErrorMessage = "El Valor de Meta es necesario.")]
        public string Meta { get; set; }
        public string IdUsuario { get; set; }

        [ForeignKey("IdIndicador")]
        public virtual Indicador Indicador { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; }

        


    }

    
    
}
