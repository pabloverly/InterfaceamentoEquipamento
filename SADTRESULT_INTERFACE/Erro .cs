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

namespace SADTRESULT_INTERFACE
{
    public partial class Erro : Form
    {
        Conexao Banco = new Conexao();
        Query query = new Query();
        OracleCommand comandoe = new OracleCommand();
        OracleDataAdapter DA = new OracleDataAdapter();
        public Erro(string erro)
        {
            InitializeComponent();
            tbErro.Text = erro;
        }

        private void btPesquisar_Click(object sender, EventArgs e)
        {
            Close();
        } 
        private void picboxSair_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void tbLogin_Enter(object sender, EventArgs e)
        {           
        }
        private void tbLogin_Leave(object sender, EventArgs e)
        {            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
