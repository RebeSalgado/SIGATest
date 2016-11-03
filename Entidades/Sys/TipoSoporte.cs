using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Sys
{
    [Table("sys_tipoSoporte")]
    public class TipoSoporte
    {
        public int Id { get; set; }

        public string Descripcion { get; set; }

    }
}
