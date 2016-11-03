using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Control
{
    [Table("Control_Kpi_Unidades")]
    public class Unidades
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Codigo { get; set; }
    }
}
