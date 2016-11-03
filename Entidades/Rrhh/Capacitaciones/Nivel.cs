﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Rrhh.Capacitaciones
{
   [Table("rrhh_capacitacion_nivel")]
   public class Nivel
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
    }
}
