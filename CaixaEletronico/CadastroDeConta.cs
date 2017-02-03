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
    public partial class CadastroDeConta : Form
    {
        private Form1 aplicacaoPrincipal;
        public CadastroDeConta(Form1 aplicacaoPrincipal)
        {
            this.aplicacaoPrincipal = aplicacaoPrincipal;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Conta novaConta = null;
            if (tipoContaCadastro.Text == "Conta Corrente")
            {
                novaConta = new ContaCorrente();
                
            } else if (tipoContaCadastro.Text == "Conta Poupanca")
            {
                novaConta = new ContaPoupanca();
                
            } else if (tipoContaCadastro.Text == "Conta Investimento")
            {
                novaConta = new ContaInvestimento();
               
            }
            novaConta.Titular = new Cliente(titularConta.Text);
            novaConta.Numero = Convert.ToInt32(numeroDaConta.Text);
            this.aplicacaoPrincipal.adicionaConta(novaConta);
        }

        private void CadastroDeConta_Load(object sender, EventArgs e)
        {

        }
    }
}
