using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Sys
{
    [Table("sys_menu")]
    public  class Menu
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Icono { get; set; }
        public int IdModulo { get; set; }

        [ForeignKey("IdModulo")]
        public virtual Modulo Modulo { get; set; }

        public virtual ICollection<SubMenu> Submenu { get; set; }


    }
}
