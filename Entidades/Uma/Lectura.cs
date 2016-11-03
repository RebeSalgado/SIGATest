using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Uma
{
    [Table("uma_mantenimiento_lecturaMaquinas")]
    public class Lecturas
    {
        [Key]
        public int Id { get; set; }

        public int IdMaquina {get;set; }

        public int Lectura { get; set; }

        public DateTime FechaLectura { get; set; }

        [ForeignKey("IdMaquina")]
        public Maquina Maquina { get; set; }


    }
}
