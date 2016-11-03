using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Uma
{
    [Table("uma_mantenimiento_OtRepuestosCorrectivosEntregados")]
    public class RepuestoCorrectivosEntregados
    {
      [Key]
        public int Id { get; set; }

        public int IdDetalleCorrectivo { get; set; }

        public int IdRecurso { get; set; }

        public int Cantidad { get; set; }

        public int NumeroPedido { get; set; }

        public DateTime FechaEntrega { get; set; }

        public string Usuario { get; set; }

        public int idRecursoOriginal { get; set; }

        public int? Valor { get; set;}

        [ForeignKey("IdRecurso")]
        public virtual Recurso Recurso { get; set; }

        [ForeignKey("IdDetalleCorrectivo")]
        public virtual OtDetalleCorrectivo DetalleCorrectivo { get; set; }
        
    }
}
