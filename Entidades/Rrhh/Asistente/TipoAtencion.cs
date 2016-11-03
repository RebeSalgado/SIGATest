using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Rrhh.Asistente
{
   [Table("rrhh_asistenteSocial_tipos_atencion")]
   public class Tipo
    {
        public int Id { get; set; }
        public string TipoAtencion { get; set; }
        public string Color { get; set; }

    }
}
