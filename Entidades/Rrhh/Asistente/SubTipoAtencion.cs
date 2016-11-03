using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Rrhh.Asistente
{
    [Table("rrhh_asistenteSocial_subTipo_atencion")]
   public class SubTipoAtencion
    {
        public int Id { get; set; }

        public string SubTipo { get; set; }

        public int IdTipo { get; set; }

        [ForeignKey("IdTipo")]
        public Tipo Tipo { get; set; }

    }
}
