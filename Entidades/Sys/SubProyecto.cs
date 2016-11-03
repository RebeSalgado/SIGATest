using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Sys
{
    [Table("sys_SubProyecto")]
    public class SubProyecto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? codigoCentroCosto { get; set; }
        public bool Visible { get; set; }

    }
}
