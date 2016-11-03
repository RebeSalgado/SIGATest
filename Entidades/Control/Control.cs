using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Control
{
    [Table("Control_Kpi_Control")]
    public class Control
    {
        [Key]
        public int Id { get; set; }
        public int IdNivel { get; set; }
        public int IdTurno { get; set; }
        public int IdIndicador { get; set; }
        public string NombreOpcional { get; set; }
        public string Desviacion { get; set; }
        public string SupervisadoPor { get; set; }
        public string IngresadoPor { get; set; }
        public string Propietario { get; set; }
        public DateTime FechaInicioMedicion { get; set; }
        public Boolean Activo { get; set; }
        public string CreadoPor { get; set; }
        public string Meta { get; set; }

    }
}
