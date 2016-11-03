using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Contabilidad.GuiaDespacho
{
    [Table("cont_guia_despacho_evento_situaciones")]
    public class Situacion
    {

        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Criticidad { get; set; }

    }
}
