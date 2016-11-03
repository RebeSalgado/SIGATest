using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Rrhh.Capacitaciones
{
    [Table("rrhh_capacitaciones_ExternaTrabajadores")]
    public class ExternaTrabajadores
    {
        public int Id { get; set; }

        public int IdCapacitacionExterna { get; set; }

        [DisplayName("Rut o nombre")]
        public string RutTrabajador { get; set; }

        [DisplayName("Nombre")]
        public string NombreTrabajador { get; set; }

        [DisplayName("Fecha Nacimiento")]
        public DateTime FechaNacimientoTrabajador { get; set; }


        public string Sexo { get; set; }

        public int IdComuna { get; set; }

        [DisplayName("Escolaridad")]
        public int IdEscolaridad { get; set; }

        [DisplayName("Nivel")]
        public int IdNivel { get; set; }


        public string Franquicia { get; set; }

        public string Cargo { get; set; }

        [ForeignKey("IdCapacitacionExterna")]
        public Externa Externa {get;set;}

        [ForeignKey("IdComuna")]
        public Comunas Comunas { get; set; }

        [ForeignKey("IdEscolaridad")]
        public Escolaridad Escolaridad { get; set; }

        [ForeignKey("IdNivel")]
        public Nivel Nivel { get; set; }


    }
}
