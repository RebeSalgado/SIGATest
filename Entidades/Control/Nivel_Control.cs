using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Control
{
    [Table("Control_GestionIndicadores_Nivel")]
    public class Nivel_Control
    {
        [Key]
        public int Id_Nivel { get; set; }
        [Required(ErrorMessage = "El Nombre del Nivel es necesario.")]

        public string Nombre { get; set; }
    }
}
