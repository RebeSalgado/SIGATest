using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//----------------------------------------agregadas el 23-02-2016 -------------------------------------------------

namespace Entidades.Uma
{
    [Table("uma_mantenimiento_FallasOt")]
    public class FallasOt
    {
        [Key]
        public int Id { get; set; }

        public int IdOt { get; set; }

        public int IdOrigen { get; set; }

        public int IdFalla { get; set; }

        [ForeignKey("IdOrigen")]
        public OrigenDeFalla OrigenDeFalla { get; set; }

        [ForeignKey("IdFalla")]
        public TiposDeFalla TiposDeFalla { get; set; }

        [ForeignKey("IdOt")]
        public OT OT { get; set; }


    }
}
