using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Uma
{
    [Table("uma_mantenimiento_TiposDeFalla")]
    public class TiposDeFalla
    {
        [Key]
        public int Id { get; set; }

        public string DescripcionFalla { get; set; }

        public int IdTipoMaquina { get; set; }

        [ForeignKey("IdTipoMaquina")]
        public TipoMaquina TipoMaquina { get; set; }

       
       
    }
}
