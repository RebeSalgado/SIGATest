using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Rrhh.Capacitaciones
{
    [Table("rrhh_dnc_trabajadores")]
    public class Trabajadores
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Obligatorio")]
        public string RutTrabajador { get; set; }

        public string NombreTrabajador { get; set; }
        [Required(ErrorMessage = "Obligatorio")]
        [Range(1,4)]
        public int Trimestre { get; set; }
        [Required(ErrorMessage = "Obligatorio")]
        [MaxLength(500)]
        public string Objetivo { get; set; }
        [Required(ErrorMessage = "Obligatorio")]
        public int IdTipo { get; set; }
        [Required(ErrorMessage = "Obligatorio")]
        public int IdAspecto { get; set; }
        [Required(ErrorMessage = " Obligatorio")]
        public int IdDnc { get; set; }
        [Required(ErrorMessage = "Obligatorio")]
        public string NombreCapacitacion { get; set; }
        public bool Realizada { get; set; }
        public int ValorCurso { get; set; }
        public int CantidadDeHoras { get; set; }
        public int SubsidioSense { get; set; }
        public int ValorHoraSense { get; set; }
        public DateTime? FechaEjecucionCurso { get; set; }

        
        [ForeignKey("IdTipo")]
        public Tipo Tipo { get; set; }

        [ForeignKey("IdAspecto")]
        public Aspecto Aspecto { get; set; }

        [ForeignKey("IdDnc")]
        public Dnc Dnc { get; set; }
         
    }
}
