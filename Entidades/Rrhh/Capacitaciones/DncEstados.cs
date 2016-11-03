using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Rrhh.Capacitaciones
{

    [Table("rrhh_dnc_estado")]
    public class DncEstados
    {
        public int Id { get; set; }

        public int Año { get; set; }

       
        public DateTime FechaCierre { get; set; }




    }
}
