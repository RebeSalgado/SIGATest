using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Rrhh.Tarja
{
    [Table("rrhh_Tarja")]
    public class Tarja
    {
        public int Id { get; set; }        
        public int Fecha {get;set;}
        public string Usuario { get; set; }
        public string NombreTarja { get; set; }
      

        public virtual ICollection<TarjaTrabajadores> TarjaTrabajadores { get; set; }


    }
}
