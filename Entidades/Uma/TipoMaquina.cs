using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Uma
{
    [Table("uma_mantenimiento_TipoMaquina")]
    public class TipoMaquina
    {

        [Key]       
        [Required]
        public int Id { get; set; }       
        [MaxLength(50)]
        [Required]
        public string Nombre { get; set; }

        public bool EsComponente { get; set; }

    }
}
