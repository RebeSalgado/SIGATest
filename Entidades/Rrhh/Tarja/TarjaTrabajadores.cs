using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Rrhh.Tarja
{
    [Table("rrhh_tarja_trabajadores")]
    public class TarjaTrabajadores
    {
        public int Id { get; set; }
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha { get; set; }
        public string Ubicacion { get; set; }
        public int IdTarja { get; set; }
        public bool Sindicato { get; set; }

        [ForeignKey("IdTarja")]
        public Tarja Tarja { get; set; }

        [ForeignKey("Estado")]
        public TarjaCodigos Codigo { get; set; }
    }
}
