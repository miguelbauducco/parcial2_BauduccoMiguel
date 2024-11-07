using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComercioLib
{
    [Serializable]
    public abstract class Ticket
    {
        protected int nroOrden;
        private DateTime fechaHora;

        public int VerNro() { return nroOrden; }

        public DateTime VerFecha() 
        {
            return fechaHora.Date;
        }
    }
}
