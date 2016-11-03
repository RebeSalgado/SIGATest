using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Contabilidad.GuiaDespacho
{
    [Table("cont_guia_folios")]
    public class Folios
    {
        [Key]
        public int Id { get; set; }

        public int Folio { get; set; }

        public bool Utilizado { get; set; }

    }
}
