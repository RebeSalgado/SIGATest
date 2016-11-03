using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Rrhh
{
    [Table("rrhh_seleccion2016")]
    public class Seleccion2016
    {
        [Key]
        public int Id { get; set; }

        public string Rut { get; set; }

        public string Nombres { get; set; }

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public int Edad { get; set; }

        public int TelefonoFijo { get; set; }

        public int TelefonoCelular { get; set; }

        public string Email { get; set; }

        public string Direccion { get; set; }

        public string Comuna { get; set; }

        public string Region { get; set; }

        public Boolean EnseñanzaMedia { get; set; }

        public string NombreTitulo { get; set; }

        public string NombreOficio { get; set; }

        public string NombreInstituto { get; set; }

        public string NombreUniversidad { get; set; }

        public string NombrePosTitulo { get; set; }

        public string CargoPostula { get; set; }

        public int AñosExperiencia { get; set; }

        public int ExperienciaMineria { get; set; }

        public string PretencionesRenta { get; set; }

        public Boolean ExGardilcic { get; set; }

    }
}
