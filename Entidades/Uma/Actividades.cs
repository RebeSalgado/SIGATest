using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Uma
{
    [Table("uma_mantenimiento_actividades")]
    public class Actividades
    {

        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int idPartes { get; set; }
        [MaxLength(1000)]
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public int Frecuencia { get; set; }
        [Required]
        public int MinutosEstimados { get; set; }
        [Required]
        public bool Opcional { get; set; }

        [ForeignKey("idPartes")]
        public virtual PlanPartes PlanPartes{get;set;}


    }
}
