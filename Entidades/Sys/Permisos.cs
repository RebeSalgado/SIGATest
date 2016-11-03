using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Sys
{
    [Table("sys_permisos")]
    public class Permisos
    {
        public int Id { get; set; }

        public string IdUsuario { get; set; }
        
        public int IdSubMenu { get; set; }

        [ForeignKey("IdSubMenu")]
        public Sys.SubMenu SubMenu { get; set; }

        [ForeignKey("IdUsuario")]
        public Sys.Usuario Usuario { get; set; }
       

    }
}
