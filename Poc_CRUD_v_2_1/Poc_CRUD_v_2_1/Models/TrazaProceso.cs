using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poc_CRUD_v_2_1.Models
{
    public class TrazaProceso
    {
        public Guid Id { get; set; }
        public string MensajeInicial { get; set; }
        public DateTime? FechaMensajeInicial { get; set; }
        public string MensajeRecepcion { get; set; }
        public DateTime? FechaMensajeRecepcion { get; set; }
        public string MensajeResultado { get; set; }
        public DateTime? FechaMensajeResultado { get; set; }

    }
}
