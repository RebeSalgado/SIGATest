using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Contabilidad.GuiaDespacho
{
    [Table("cont_guia_despacho_referencias")]
   public class Referencia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string TipoReferencia { get; set; }

        public string Folio { get; set; }

        public string Fecha { get; set; }

        [Column("IdCabecera")]
        public int idCabecera { get; set; }

        [ForeignKey("idCabecera")]
        public virtual GuiaDespacho GuiaDespacho { get; set; }
    }
}
