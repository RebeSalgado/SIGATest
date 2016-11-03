using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Global
{
    [Table("rrhh_trabajadores")]
    public class Trabajador
    {

        public int Id { get; set; }
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Cargo { get; set; }
        public string Direccion { get; set; }
        public string Comuna { get; set; }
        public bool Vigente { get; set; }
        public string CentroCosto { get; set; }

        [Column("divCodigo")]
        public string Division { get; set; }

    }
}
