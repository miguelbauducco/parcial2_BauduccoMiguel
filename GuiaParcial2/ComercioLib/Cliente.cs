using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComercioLib
{
    [Serializable]
    public class Cliente : Ticket
    {
        private static int nroInicio;
        private int dni;

        public Cliente(int dni) 
        {
          nroOrden=nroInicio++;
          this.dni = dni;
        }
    }
}
