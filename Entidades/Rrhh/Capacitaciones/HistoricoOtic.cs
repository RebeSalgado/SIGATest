using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Rrhh.Capacitaciones
{
    [Table("rrhh_dnc_HistoricoOtic")]
   public  class HistoricoOtic
    {
        public int Id { get; set; }
        public string RutTrabajador { get; set; }
        public int CostoEmpresa { get; set; }
        public string Otec { get; set; }
        public DateTime Fecha { get; set; }
        public string Curso { get; set; }
        public int Asistencia { get; set; }
        public string Planificado { get; set; }
        public int Horas { get; set; }

    }
}
