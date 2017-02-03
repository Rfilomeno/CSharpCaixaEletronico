using Caelum.CaixaEletronico.Modelo.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caelum.CaixaEletronico.Modelo.Contas
{
    public abstract class Conta
    {
        public int Numero { get; set; }
        public Cliente Titular { get; set; }
        public double Saldo { get; protected set; }

        public virtual void Deposita(double valor)
        {
            if (valor > 0)
            {
                this.Saldo += valor;
            }
        }

        public abstract void Saca(double valor);

        public void TransferePara( Conta destino, double valor)
        {
            this.Saca(valor);
            destino.Deposita(valor);
        }

        public double CalculaRendimentoAnual()
        {
            double saldoNaqueleMes = this.Saldo;

            for (int i = 0; i < 12; i++)
            {
                saldoNaqueleMes = saldoNaqueleMes * 1.007;
            }

            double rendimento = saldoNaqueleMes - this.Saldo;

            return rendimento;
        }

        public override bool Equals(object obj)
        {
            if(obj != null) {
                Conta outra = (Conta)obj;

                bool numero = (this.Numero == outra.Numero);
                bool titular = (this.Titular.Equals(outra.Titular));

                return numero && titular;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return "Numero: " + this.Numero;
        }
    }
}
