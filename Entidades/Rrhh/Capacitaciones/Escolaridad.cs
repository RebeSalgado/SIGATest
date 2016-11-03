using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Rrhh.Capacitaciones
{
   [Table("rrhh_capacitaciones_escolaridad")]
   public class Escolaridad
    {
        
        public int Id { get; set; }

        public string Descripcion { get; set; }

        
    }
}
