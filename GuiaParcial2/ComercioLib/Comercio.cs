using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComercioLib
{
    [Serializable]
    public class Comercio
    {
        public int CantTicket { get { return ListaAtendidos.Count; } }
        private List<CuentaCorriente> cuentasCorrientes = new List<CuentaCorriente>();
        private Queue<Cliente> nuevosClientes = new Queue<Cliente>();
        private Queue<Pago> nuevoP = new Queue<Pago>();
        
        private List<Ticket> ListaAtendidos {  get; set; } = new List<Ticket>();

        public void AgregarTicket(Ticket turno) 
        {
            if (turno != null)
            {
                if (turno is Cliente) nuevosClientes.Enqueue(turno as Cliente);
                if(turno is Pago) nuevoP.Enqueue(turno as Pago);
            }
        }

        public Ticket VerTicketAtendido(int idx) 
        {
            return ListaAtendidos[idx];
        }

        public Ticket AtenderTicket(int tipoTicket) 
        {
            Ticket t = null;

            if(tipoTicket==1 && nuevosClientes.Count > 0)  
                t = nuevosClientes.Dequeue();
            
            if(tipoTicket==0 && nuevoP.Count > 0)
               t = nuevoP.Dequeue();

            return t;
        }

        public CuentaCorriente BuscarCuenta(int nroC) 
        {
            cuentasCorrientes.Sort();
            int busq = cuentasCorrientes.BinarySearch( new CuentaCorriente(nroC,null, 0));
            if (busq >= 0)
            {
                return cuentasCorrientes[busq];
            }
            return null;
            
        }

        public void AgregarCc(int nro, int dni, double saldo) 
        {
           CuentaCorriente cc = BuscarCuenta(nro);
            if(cc == null) 
            {
                cc = new CuentaCorriente(nro,new Cliente(dni),saldo);
                cuentasCorrientes.Add(cc);
            }
            else 
            {
                cc.AgregarSaldo(saldo);
            }

        }

    }
}
