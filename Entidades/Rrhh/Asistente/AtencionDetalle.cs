using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Rrhh.Asistente
{
    [Table("rrhh_asistenteSocial_atencion_detalle")]
    public class AtencionDetalle
    {
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaAtencion { get; set; }

        public string UsuarioAtencion { get; set; }

        public int IdAtencion { get; set; }

        [ForeignKey("idAtencion")]
        public Atencion Atencion {get;set;}



    }
}
