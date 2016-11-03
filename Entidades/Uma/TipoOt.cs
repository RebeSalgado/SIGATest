using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Uma
{
    [Table("uma_mantenimiento_tipoOt")]
    public class TipoOt
    {
        [Key]
        public int Id { get; set; }
        
        public string Nombre { get; set; }

       


     

    }

}
