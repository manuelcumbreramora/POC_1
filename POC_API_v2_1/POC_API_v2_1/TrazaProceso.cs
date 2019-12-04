using System;

namespace POC_API_v2_1
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
