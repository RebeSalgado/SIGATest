using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Control
{
    [Table("Control_GestionIndicadores_Area")]
   public class Area
    {
        [Key]
        
        public int Id_Area { get; set; }
        [Required(ErrorMessage = "El Nombre del Area es necesario.")]
        public string Nombre { get; set; }
    }
}
