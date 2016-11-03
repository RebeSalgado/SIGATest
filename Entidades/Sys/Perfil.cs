using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Sys
{
    [Table("AspNetUsersPerfil")]
   public class Perfil
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string NombreReal { get; set; }

    }
}
