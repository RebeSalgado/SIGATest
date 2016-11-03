using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Uma
{
    [Table("uma_mantenimiento_historicoComponentes")]
    public class HistoricoComponentes
    {
        [Key]
        public int Id { get; set; }

        public int IdMaquinaPadre { get; set; }

        public int IdComponente { get; set; }

        public DateTime FechaAsignacion { get; set; }

        public DateTime? FechaDesignacion { get; set; }

        public string Ubicacion { get; set; }

        [ForeignKey("IdMaquinaPadre")]
        public Maquina Maquina { get; set; }

        [ForeignKey("IdComponente")]
        public Maquina Componente { get; set; }
    }
}
