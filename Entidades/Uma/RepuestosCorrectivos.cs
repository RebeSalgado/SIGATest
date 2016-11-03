using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Uma
{
    [Table("uma_Mantenimiento_OtRepuestosCorrectivosSolicitados")]
    public class RepuestosCorrectivosSolicitados
    {
        [Key]
        public int Id { get; set;}

        public int IdDetalleCorrectivo { get; set; }

        public int IdRecurso { get; set; }

        public int Cantidad { get; set; }

        public bool Aprobado { get; set; }

        [ForeignKey("IdRecurso")]
        public virtual Recurso Recurso { get; set; }

        [ForeignKey("IdDetalleCorrectivo")]
        public virtual OtDetalleCorrectivo DetalleCorrectivo { get; set;}

    }
}
