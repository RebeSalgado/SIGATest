using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Sys
{
   [Table("sys_modulo")]
   public class Modulo
    {
        public int Id { get; set; }
        public string  Nombre { get; set; }
        public string Alias { get; set; }
        public string Glyhicon { get; set; }
        public string Color { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }

    }
}
