using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Control
{
    [Table("Control_Kpi_Valores")]
    public class Valores
    {
        [Key]
        public int Id { get; set; }
        public int IdControl { get; set; }
        public string Valor { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string IngresadoPor { get; set; }
        public string Observacion { get; set; }

        [ForeignKey("IdControl")]
        public virtual Control Control { get; set; }
    }
}
