using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComercioLib
{
    [Serializable]
    public class CuentaCorriente : IComparable<CuentaCorriente>
    {
        private int nroCuenta;
        private Cliente titular;
        private double saldo;

        public CuentaCorriente(int nro, Cliente titular, double saldo)
        {
            nroCuenta = nro;
            this.titular = titular;
            this.saldo = saldo;
        }

        public Cliente VerTitular() { return titular; }

        public int VerNroCuenta() { return nroCuenta; }

        public void RegistrarPago(double monto)
        {
            saldo -= monto;
        }

        public void RegistrarVenta(double monto)
        {
            saldo += monto;
        }

        public double VerSaldo() { return saldo; }

        public override string ToString()
        {
            return $"{VerNroCuenta()};{VerTitular()};{VerSaldo()}";
        }

        public int CompareTo(CuentaCorriente other)
        {
            if (other != null)
                return nroCuenta.CompareTo(other.nroCuenta);
            return 1;
        }

        public void AgregarSaldo(double saldo) 
        {
            this.saldo += saldo;
            
        }

    }
}
