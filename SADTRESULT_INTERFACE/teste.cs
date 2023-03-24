using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SADTRESULT;

namespace SADTRESULT_INTERFACE
{
    public partial class teste : Form
    {
        Conexao Banco = new Conexao();
        Query query = new Query();
        OracleCommand comandoe = new OracleCommand();
        OracleDataAdapter DA = new OracleDataAdapter();
        public teste()
        {
            InitializeComponent();            
        }

        private void btPesquisar_Click(object sender, EventArgs e)
        {
            BuscarArquivos();
        }
  
        private string BuscarArquivos()
        {
            string nm_arquivo = "";
            string diretorio = "";
            List<string> lista = new List<string>();

            //DirectoryInfo drinfo = new DirectoryInfo(@"E:\Integra\Result\");

            DirectoryInfo drinfo = new DirectoryInfo(@"\\10.0.70.17\Sistema\Integra\Result\");
            FileInfo[] Files = drinfo.GetFiles("*", SearchOption.AllDirectories);
            foreach (FileInfo file in Files)
            {
                nm_arquivo = file.FullName.Replace(drinfo.FullName, "").Remove(12, 0);

                //diretorio = @"E:\Integra\Result\" + nm_arquivo;
                diretorio = @"\\10.0.70.17\Sistema\Integra\Result\" + nm_arquivo;
                
                string sourceFile = @"\\10.0.70.17\Sistema\Integra\Result\" + nm_arquivo;
                string destinationFile = @"\\10.0.70.17\Sistema\Integra\Integrafinal\" + nm_arquivo;

                try
                {
                   
                    System.IO.File.Move(sourceFile, destinationFile);

                }
                catch (Exception ex)
                {
                    tbErro.Text =  nm_arquivo + ex.Message + destinationFile + destinationFile;   
                }              
            }
            return nm_arquivo;
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
