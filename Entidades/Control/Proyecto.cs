using Entidades.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Entidades.Control
{
    [Table("Control_GestionIndicadores_Proyecto")]
    public class Proyecto
    {
        [Key]
        public int Id_Proyecto { get; set; }

        [Required(ErrorMessage = "El Nombre del Proyecto es necesario.")]
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }


       
    }

   
}
