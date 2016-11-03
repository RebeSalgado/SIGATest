using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Uma
{
    [Table("uma_mantenimiento_RepuestosAlternativos")]
    public class RepuestosAlternativos
    {
        [Key]
        public int Id { get; set; }

        public int IdOriginal { get; set; }

        public string CodigoAlternativo { get; set; }


        [ForeignKey("IdOriginal")]
        public virtual RepuestosOriginales RepuestosOriginal { get; set; }
    }
}
