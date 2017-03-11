using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practica2.Models
{
    public class CuentaModels
    {
        public int id_cuenta { get; set; }
        public double saldo { get; set; }
        public UsuarioModels usuario { get; set; }
    }
}