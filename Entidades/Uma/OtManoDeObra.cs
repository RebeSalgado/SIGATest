using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Uma
{
    [Table("uma_mantenimiento_otTrabajadores")]
    public class OtManoDeObra
    {
        [Key]
        public int Id { get; set; }

        public int IdPersonal { get; set; }

        public int Tiempo { get; set; }

        public int Valor { get; set; }

        public int IdOt { get; set; }

        public DateTime HoraInicio { get; set; }

        public DateTime horaTermino { get; set; }
    }
}
