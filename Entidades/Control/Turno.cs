using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Control
{
    [Table("Control_GestionIndicadores_Turno")]
    public class Turno
    {
        [Key]
        public int Id_Turno { get; set; }

        [Required(ErrorMessage = "El Nombre del Turno es necesario.")]
        public string Nombre { get; set; }

    }
}
