using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Rrhh.Tarja
{
    [Table("rrhh_tarja_codigos")]
    public class TarjaCodigos
    {
        [Key]
        public string Codigo { get; set; }

        public string Descripcion { get; set; }
    }
}
