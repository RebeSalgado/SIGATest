using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Rrhh.Asistente
{
    [Table("rrhh_asistenteSocial_atencion_cabecera")]
    public class Atencion
    {
        public int Id { get; set; }

        public string RutTrabajador {get;set;}

        public string UsuarioAtencion { get; set; }

        public DateTime FechaAtencion { get; set; }

        public int IdSubtipoAtencion { get; set; }

        public bool Cerrado { get; set; }

        [ForeignKey("IdSubtipoAtencion")]
        public SubTipoAtencion SubTipoAtencion { get; set; }


    }
}
