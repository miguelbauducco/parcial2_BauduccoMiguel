using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComercioLib
{
    [Serializable]
    public class Pago:Ticket
    {
        private static int nroInicio;
        private CuentaCorriente ficha;

        public Pago(CuentaCorriente f)
        {
            nroOrden = nroInicio++;
            ficha = f;
        }

        public void MontoPago(double valor) 
        {
            ficha.RegistrarPago(valor);
        }
    }
}
