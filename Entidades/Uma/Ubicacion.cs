using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Uma
{
    [Table("uma_mantenimiento_ubicacion")]
    public class Ubicacion
    {
        public int Id { get; set; }

        public int IdParent {get;set;}

        public string Nombre { get; set; }

        public string RutaCompleta { get; set; }

        public int? creId { get; set; }

        public string creNombre { get; set; }

        [ForeignKey("IdParent")]
        public Ubicacion Padre { get; set; }


    }
}
