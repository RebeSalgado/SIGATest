using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Control
{
    [Table("Control_Kpi_Indicadores")]
    public class Indicadores
    {
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Unidad { get; set; }
        public string Nivel { get; set; }
        public decimal Tolerancia1 { get; set; }
        public decimal Tolerancia2 { get; set; }
        public Boolean Estado { get; set; }

        [ForeignKey("Unidad")]
        public virtual Unidades Unidades { get; set; }

    }

}
