using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Sys
{
    [Table("sys_controlDeCambios")]
    public class ControlDeCambios
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        public int IdAplicacion { get; set; }

        public int IdTipoSoporte { get; set; }

       
        public decimal?  Version { get; set; }

        public string CambioRealizado { get; set; }

        [ForeignKey("IdAplicacion")]
        public Aplicaciones Aplicacion { get; set; }

        [ForeignKey("IdTipoSoporte")]
        public TipoSoporte TipoSoporte { get; set; }

    }
}
