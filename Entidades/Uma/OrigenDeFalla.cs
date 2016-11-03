using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//----------------------------------------agregadas el 23-02-2016 -------------------------------------------------

namespace Entidades.Uma
{
    [Table("uma_mantenimiento_OrigenDeFalla")]
    public class OrigenDeFalla
    {
        [Key]
        public int Id { get; set; } 

        public string Descripcion { get; set; }

      
    }
}
