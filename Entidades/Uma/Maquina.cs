using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Uma
{
    [Table("uma_mantenimiento_maquina")]
    public class Maquina
    {
        [Key]
        public int? Id { get; set; }

        public int? IdParent { get; set; }

        public int IdUbicacion { get; set; }

        public int IdTipoMaquina { get; set; }

        public int? IdPlan { get; set; }

        public string Codigo { get; set; }

        public int Promedio { get; set; }

        public int? PromedioIzquierdo { get; set; }

        public int? PromedioDerecho { get; set; }

        public int LecturaActual { get; set; }

        public int LecturaBase { get; set; }

        public string NumeroSerie { get; set; }

        public string Marca { get; set; }

        public string Capacidad { get; set; } 

        public string Modelo { get; set; }

        public int? Año { get; set; }

        public string Proveedor { get; set; }


        [DataType(DataType.Date)]
        public DateTime? FechaLecturaBase { get; set; }

        public int? UltimoContador { get; set; }

        [DataType(DataType.Date)]
        public DateTime? UltimoContadorFecha { get; set; }

       
       


        [ForeignKey("IdParent")]
        public Maquina Padre { get; set; }

        [ForeignKey("IdUbicacion")]
        public Ubicacion Ubicacion { get; set; }

        [ForeignKey("IdTipoMaquina")]
        public TipoMaquina TipoMaquina { get; set; }

        [ForeignKey("IdPlan")]
        public Plan Plan { get; set; }




    }
}
