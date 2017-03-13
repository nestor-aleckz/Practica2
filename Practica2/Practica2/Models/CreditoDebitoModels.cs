using System;

namespace Practica2.Models
{
    public class CreditoDebitoModels
    {
        public string descripcion { get; set; }
        public double monto { get; set; }
        public DateTime fecha { get; set; }
        public int tipo { get; set; }
        public CuentaModels cuenta { get; set; }
    }
}