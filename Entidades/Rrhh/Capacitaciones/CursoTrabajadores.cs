using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Rrhh.Capacitaciones
{
    [Table("rrhh_dnc_Capacitaciontrabajadores")]
    public class CursoTrabajadores
    {
        public int Id { get; set; }

        public string RutTrabajador { get; set; }

        public int idCursoInterno {get;set;}

        public bool Realizada { get; set; }

        public string NombreTrabajador { get; set; }



        [ForeignKey("idCursoInterno")]
        public Capacitacion Interna { get; set; }



    }
}
