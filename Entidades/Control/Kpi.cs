using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Control
{
    [Table("Control_Kpi_Kpi")]
    public class Kpi
    {
        public int Id { get; set; }
        public int IdSubproyecto { get; set; }
        public int IdIndicador { get; set; }
        public int IntervaloDias {get;set;}
        public string NombreOpcional { get; set; }
        public string Perspectiva { get; set; }
        public string IngresadoPor { get; set; }
        public string supervisadoPor { get; set; }
        public string Propietario { get; set; }
        public DateTime FechaInicioMedicion {get;set;}     
        public bool Activo { get; set; }
        public string CreadoPor { get; set; }


    }
}
