using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Uma
{
    [Table("uma_mantenimiento_otDetalleCorrectivo")]
    public class OtDetalleCorrectivo
    {
        public int Id {get;set;}

        public string NombreActividad {get;set;}

        public int IdOt {get;set;}

        public int Progreso {get; set;}

        public string Usuario {get; set;}

        public DateTime FechaCreacion { get; set; }

        [ForeignKey("IdOt")]
        public OT Cabecera { get; set; }

    }
}
