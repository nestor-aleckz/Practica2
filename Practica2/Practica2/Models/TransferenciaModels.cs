using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practica2.Models
{
    public class TransferenciaModels
    {
        public double monto { get; set; }
        public DateTime fecha { get; set; }
        public CuentaModels cuenta_destino { get; set; }
        public CuentaModels cuenta_origen { get; set; }
    }
}