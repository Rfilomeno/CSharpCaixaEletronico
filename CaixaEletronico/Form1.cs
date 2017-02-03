using Caelum.CaixaEletronico.Modelo;
using Caelum.CaixaEletronico.Modelo.Contas;
using Caelum.CaixaEletronico.Modelo.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaixaEletronico
{
    public partial class Form1 : Form
    {
        Conta [] contas;
        private int numeroDeContas = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            contas = new Conta[20];

            Conta contaDoVictor = new ContaCorrente();
            contaDoVictor.Titular = new Cliente();
            contaDoVictor.Titular.Nome = "Victor";
            contaDoVictor.Numero = 1;
            contas[numeroDeContas] = contaDoVictor;
            numeroDeContas++;

            Conta contaDoGuilherme = new ContaPoupanca();
            contaDoGuilherme.Titular = new Cliente();
            contaDoGuilherme.Titular.Nome = "Guilherme";
            contaDoGuilherme.Numero = 2;
            contas[numeroDeContas] = contaDoGuilherme;
            numeroDeContas++;

            Conta contaDoMauricio = new ContaInvestimento();
            contaDoMauricio.Titular = new Cliente();
            contaDoMauricio.Titular.Nome = "Mauricio";
            contaDoMauricio.Numero = 3;
            contas[numeroDeContas] = contaDoMauricio;
            numeroDeContas++;

            foreach (Conta conta in this.contas)
            {
                if (conta != null)
                {
                    comboContas.Items.Add(conta);
                    destinoDaTransferencia.Items.Add(conta);
                }
            }
            comboContas.DisplayMember = "Titular";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string textoValorSaque = valorOperacao.Text;

            double valorDeposito = Convert.ToDouble(textoValorSaque);

            int indiceSelecionado = comboContas.SelectedIndex;

            Conta contaSelecionada = this.contas[indiceSelecionado];
            contaSelecionada.Deposita(valorDeposito);

            this.MostraConta(contaSelecionada);
        }

        private void button2_Click(object sender, EventArgs eA)
        {
            string textoValorSaque = valorOperacao.Text;

            double valorSaque = Convert.ToDouble(textoValorSaque);
            Conta contaSelecionada = this.BuscaContaSelecionada();

            try { 
                contaSelecionada.Saca(valorSaque);
                MessageBox.Show("Dinheiro Liberado");
            }
            catch (SaldoInsuficienteException e)
            {
                MessageBox.Show("Saldo insuficiente. " + e.Message);
            }
            catch (ArgumentException e)
            {
                MessageBox.Show("Não é possível sacar um valor negativo. " + e.Message);
            }

            this.MostraConta(contaSelecionada);
        }

        private void MostraConta(Conta conta)
        {
            textoTitular.Text = conta.Titular.Nome;
            textoSaldo.Text = Convert.ToString(conta.Saldo);
            textoNumero.Text = Convert.ToString(conta.Numero);
        }

        private Conta BuscaContaSelecionada()
        {
            int indiceSelecionado = comboContas.SelectedIndex;
            return this.contas[indiceSelecionado];
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           // Conta contaSelecionada = (Conta)comboContas.SelectedItem;
          //  textoTitular.Text = contaSelecionada.Titular.Nome;
          //  textoSaldo.Text = Convert.ToString(contaSelecionada.Saldo);
          //  textoNumero.Text = Convert.ToString(contaSelecionada.Numero);


             Conta contaSelecionada = this.BuscaContaSelecionada();
             //string titularSelecionado = comboContas.Text;

             this.MostraConta(contaSelecionada);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Conta contaSelecionada = this.BuscaContaSelecionada();
  
            int indiceDaContaDestino = destinoDaTransferencia.SelectedIndex;

            Conta contaDestino = this.contas[indiceDaContaDestino];

            string textoValor = valorOperacao.Text;
            double valorTransferencia = Convert.ToDouble(textoValor);
        
            contaSelecionada.TransferePara(contaDestino, valorTransferencia);

            this.MostraConta(contaSelecionada);

        }

        private void testeEquals_click(object sender, EventArgs e)
        {
            Cliente guilherme = new Cliente("Guilherme");
            guilherme.Rg = "12345678-9";

            Cliente paulo = new Cliente("Paulo");
            paulo.Rg = "12345678-9";
            //paulo.Rg = "98765432-1"; teste diferente
            MessageBox.Show(guilherme.ToString() + "\n é igual a \n" + paulo.ToString() + " ? \n\n" + guilherme.Equals(paulo));
        }
        public void adicionaConta(Conta c)
        {
            
            if (this.numeroDeContas == this.contas.Length)
            {
                Conta[] novo = new Conta[this.contas.Length * 2];
                for (int i = 0; i < this.contas.Length; i++)
                {
                    novo[i] = this.contas[i];
                }
                this.contas = novo;

            }
            this.contas[this.numeroDeContas] = c;
            this.numeroDeContas++;
            comboContas.Items.Add(c);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CadastroDeConta cadastro = new CadastroDeConta(this);
            cadastro.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Conta contaSelecionada = this.BuscaContaSelecionada();
            RemoveConta(contaSelecionada);
        }

        private void RemoveConta(Conta c)
        {
            int i;
            comboContas.Items.Remove(c);
            textoTitular.Text = "";
            textoNumero.Text = "";
            textoSaldo.Text = "";
            valorOperacao.Text = "";
            for (i = 0; i < this.contas.Length; i++)
            {
                if (contas[i]==c)
                {
                    break;
                }
            }
            while (i+1 < this.numeroDeContas)
                {
                    this.contas[i] = this.contas[i + 1];
                    i++;
                }
            
        }
    }
    
}