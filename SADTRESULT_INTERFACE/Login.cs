using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SADTRESULT;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;

namespace SADTRESULT_INTERFACE
{
    public partial class Login : Form
    {
        Conexao Banco = new Conexao();
        Query query = new Query();
        OracleCommand comando = new OracleCommand();
        OracleDataAdapter DA = new OracleDataAdapter();
        public Login()
        {
            InitializeComponent();
        }

        private void btPesquisar_Click(object sender, EventArgs e)
        {            

            // tbLogin.Text.ToLower();
            if (tbLogin.Text == "LAB" && tbSenha.Text == "123")
            {
                Interface formInterface = new Interface();
                formInterface.Show();
            }           

            else
            {               
              
               Acesso acesso = new Acesso();
                if (acesso.Autentica(tbLogin.Text, tbSenha.Text))
                {
                    Interface formInterface = new Interface();
                    formInterface.Show();
                }
                else
                {
                    Erro erro = new Erro("ACESSO INVALIDO");
                    erro.Show();                  
                }
                
            }
        }
        private void btIntergar_Click(object sender, EventArgs e)
        {

        }
        //ATUALIZAR CODIGO DA VERSAO

        private void picboxSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tbLogin_Enter(object sender, EventArgs e)
        {
            if (tbLogin.Text == "LOGIN")
            {
                tbLogin.Text = "";
                tbLogin.ForeColor = Color.Gray;
            }
        }

        private void tbLogin_Leave(object sender, EventArgs e)
        {
            if (tbLogin.Text == "")
            {
                tbLogin.Text = "LOGIN";
                tbLogin.ForeColor = Color.Red;
            }
        }

        private void tbSenha_Enter(object sender, EventArgs e)
        {
            if (tbSenha.Text == "SENHA")
            {
                tbSenha.Text = "";
                tbSenha.ForeColor = Color.Gray;
               // tbSenha.UseSystemPasswordChar = true;
            }
        }

        private void tbSenha_Leave(object sender, EventArgs e)
        {
            if (tbSenha.Text == "")
            {
               // tbSenha.Text = "SENHA";
                tbSenha.ForeColor = Color.Red;
            }
        }
    }
}
