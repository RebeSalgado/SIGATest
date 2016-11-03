using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Contabilidad.GuiaDespacho
{
  
        [Table("cont_guia_despacho_detalle")]
        public class Detalle
        {
            [Key]
            public int Id { get; set; }
            [MaxLength(5)]
            public string IndicadorExencion { get; set; }
            [MaxLength(50)]
            public string CodigoInternoItem { get; set; }
            [MaxLength(80)]
            public string NombreItem { get; set; }
            [MaxLength(80)]
            public string DescripcionItem { get; set; }
            [MaxLength(50)]
            public string CantidadItem { get; set; }
            [MaxLength(50)]
            public string UnidadMedida { get; set; }
            [MaxLength(50)]
            public string PrecioUnitarioItem { get; set; }
            [MaxLength(50)]
            public string DescuentoEnPorcentaje { get; set; }
            [MaxLength(50)]
            public string DescuentoEnPesos { get; set; }
            [MaxLength(50)]
            public string RecargoPorcentaje { get; set; }
            [MaxLength(50)]
            public string RecargoPesos { get; set; }
            [MaxLength(50)]
            public string MontoItem { get; set; }

            public int? IdCabecera { get; set; }

            [ForeignKey("IdCabecera")]
            public virtual GuiaDespacho GuiaDespacho { get; set; }
        }

  
}
