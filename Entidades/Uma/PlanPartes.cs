using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Uma
{
    [Table("uma_mantenimiento_planPartes")]
    public class PlanPartes
    {
        [Key]
        public int id { get; set; }

        public int idPlan { get; set; }

        [MaxLength(250)]
        [Required]
        public string Nombre { get; set; }

        [ForeignKey("idPlan")]
        public virtual Plan Plan { get; set; }



    }
}
