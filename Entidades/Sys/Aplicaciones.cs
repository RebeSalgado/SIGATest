using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Sys
{
    [Table("sys_Aplicaciones")]
   public class Aplicaciones
    {
        public int Id { get; set; }

        public string Aplicacion { get; set; }


    }
}
