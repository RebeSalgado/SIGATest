using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Uma
{
    [Table("uma_mantenimiento_Servicios")]
    public class Servicios
    {
        [Key]
        public int Id { get; set; }

        public int IdOt { get; set; }

        public string RazonSocial { get; set; }

        public int OcDetId { get; set; }

        public string Glosa { get; set; }

        public int Cantidad { get; set; }

        public int Valor { get; set; }

        public DateTime Fecha { get; set; }
    }
}
