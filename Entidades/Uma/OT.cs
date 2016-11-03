using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Uma
{
    [Table("uma_mantenimiento_Ot")]
    public class OT
    {
        [Key]
        public int Id { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int IdMaquina { get; set; }

        public string Descripcion { get; set; }

        public int IdTipoOt { get; set; }

        public bool Cerrada { get; set; }
       
        [DataType(DataType.Date)]        
        public DateTime FechaInicio { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaTermino { get; set; }

        public string Usuario { get; set; }

        public int PorcentajeFinalizado { get; set; }

        public int? IdMaquinaPadre { get; set; }

        public int? IdCConsumo { get; set; }

        public string CconsumoDesc { get; set; }

        public int? IdCResp { get; set; }

        public string CRespDes { get; set; }

        public DateTime? HoraAvisoOperaciones { get; set; }

        public DateTime? HoraIntervencionMaquina { get; set; }

        public int? MinutosDeIntervencion { get; set; }

        public DateTime?  FechaTerminoIntervencion { get; set; }

        public bool? QuedoOperativa { get; set; }

        public string Observaciones  { get; set; }

        public int? LugarOt { get; set; }

        [ForeignKey("IdMaquina")]
        public Maquina Maquina { get; set; }

        [ForeignKey("IdTipoOt")]
        public TipoOt Tipo { get; set; }

        [ForeignKey("IdMaquinaPadre")]
        public Maquina MaquinaPadre { get; set; }


        public ICollection<DetalleCorrectivo> Detalles { get; set; }

        public ICollection<FallasOt> Fallas { get; set; }



    }
}
