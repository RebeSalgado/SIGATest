using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Sys
{
    [Table("sys_subMenu")]
    public class SubMenu
    {
        public int Id { get; set; }

        public string NombreAction { get; set; }

        public string NombreControlador { get; set; }

        public int IdMenu { get; set; }

        public string Url { get; set; }

        public string Titulo { get; set; }


        [ForeignKey("IdMenu")]
        public virtual Menu Menu { get; set; }

    }
}
