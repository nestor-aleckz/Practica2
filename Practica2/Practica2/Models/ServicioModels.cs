using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practica2.Models
{
    public class ServicioModels
    {
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public double monto { get; set; }
        public DateTime fecha { get; set; }
        public CuentaModels cuenta_origen { get; set; }
        public CuentaModels cuenta_servicio { get; set; }
    }
}