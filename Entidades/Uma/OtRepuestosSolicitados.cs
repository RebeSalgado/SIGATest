using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Uma
{
    [Table("uma_mantenimiento_OtRepuestosSolicitados")]
    public class OtRepuestosSolicitados
    {
        [Key]
        public int Id { get; set; }

        public int IdOtDetalle { get; set; }

        public int IdRecurso { get; set; }

        public int Valor { get; set; }

        public decimal Cantidad { get; set; }

        public int IdUnidad { get; set; }

        public bool Aprobado { get; set; }

        [ForeignKey("IdRecurso")]
        public virtual Recurso Recurso { get; set; }

        [ForeignKey("IdOtDetalle")]
        public virtual DetalleCorrectivo Detalle { get; set; }

    }
}
