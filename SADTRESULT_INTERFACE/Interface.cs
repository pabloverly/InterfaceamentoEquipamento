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
using System.Threading;
using System.IO;

namespace SADTRESULT_INTERFACE
{
    public partial class Interface : Form
    {
        Conexao Banco = new Conexao();
        QueryInter query = new QueryInter();
        OracleCommand comando = new OracleCommand();
        OracleDataAdapter DA = new OracleDataAdapter();
        string nm_arquivo = "";
        string diretorio = "";
        String linha;

        DirectoryInfo drinfo = new DirectoryInfo(@"\\10.0.70.17\Sistema\Integra\Result\");
        // DirectoryInfo drinfo = new DirectoryInfo(@"\\192.168.1.4\Integra\Result\");       
        FileInfo[] Files;

        public Interface()
        {
            InitializeComponent();
            consutarPaciente();
        }
        private void Messagem(string msg, string arquivo)
        {
            try
            {
                StreamWriter vWriter = new StreamWriter(@"c:\temp\" + arquivo + ".txt", true);
                vWriter.Write(msg);
                vWriter.Flush();
                vWriter.Close();
            }
            catch { }
        }
        private void buscarArquivo()
        {
            int x = 1;
            Files = drinfo.GetFiles("*", SearchOption.AllDirectories);

            foreach (FileInfo file in Files)
            {
                nm_arquivo = file.FullName.Replace(drinfo.FullName, "").Remove(12, 0);
                diretorio = @"\\10.0.70.17\Sistema\Integra\Result\" + nm_arquivo;
                // diretorio = @"\\192.168.1.4\Integra\Result\" + nm_arquivo;

                string dirErro = @"\\10.0.70.17\Sistema\Integra\Erro\" + nm_arquivo;
                // string dirErro = @"\\192.168.1.4\Integra\Erro\" + nm_arquivo;
                string destinationFile = @"\\10.0.70.17\Sistema\Integra\Integrafinal\" + nm_arquivo;
                //  string destinationFile = @"\\192.168.1.4\Integra\\Integrafinal\" + nm_arquivo;

                while (File.Exists(diretorio))
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(diretorio))
                        {
                            // Lê linha por linha até o final do arquivo
                            while ((linha = sr.ReadLine()) != null)
                            {
                                insertResultado(nm_arquivo + x, linha);
                                x++;
                            }
                        }
                    }
                    catch { System.IO.File.Move(diretorio, dirErro); }
                    finally { x = 1; }

                    try
                    {
                        System.IO.File.Move(diretorio, destinationFile);
                    }
                    catch { file.Delete(); }
                    finally { }
                }
                x = 1;
            }
        }
        private void insertResultado(string nm_arquivo, string resultado)
        {

            if (Banco.conn.State == System.Data.ConnectionState.Open)
            {
                Banco.FechaConexao();
            }
            else
            {
                try
                {
                    Banco.AbreConexao();
                    comando.Connection = Banco.RetornaConexao();
                    comando.CommandText = " DELETE DBATI.resultado WHERE cd_arquivo like '%" + nm_arquivo + "%'";
                    comando.ExecuteNonQuery();
                    comando.CommandText = " insert into dbati.resultado (cd_arquivo,ds_resultado)  VALUES ('" + nm_arquivo + "','" + resultado + "')";
                    comando.ExecuteNonQuery();
                    Banco.FechaConexao();
                }
                catch (Exception erro) { }

                finally { Banco.FechaConexao(); }
            }
        }
        private void btPesquisar_Click(object sender, EventArgs e)
        {
            //METODO - CASO PESQUISE PELO PEDIDO OU GERAL
            // if (tbPesquisa.Text == "PEDIDO" || tbPesquisa.Text.Length == 0)
            //{
            //     consutarPaciente();
            // }
            // else
            // {
            filtroPaciente(tbPesquisa.Text);
            // }
        }
        //PESQUISAR PACIENTE        
        private void consutarPaciente()
        {
            dgvResultado.DataSource = null;
            dgvAmostra.DataSource = null;
            OracleDataAdapter DA1 = new OracleDataAdapter();
            DataSet DSet1 = new DataSet(); DataSet DSetComp2 = new DataSet();
            try
            {
                DA1 = query.paciente();
                DA1.Fill(DSet1, "TBPACIENTE");
                dgvPaciente.DataSource = DSet1.Tables[0];
            }
            catch (Exception erro)
            {
                //Erro msg = new Erro("ERRO AO GERAR LISTA DE PEDIDOS.\n " + erro.Message);
                //msg.Show();               
            }
            Banco.AbreConexao();
            comando.Connection = Banco.RetornaConexao();

            int total = dgvPaciente.Rows.Count - 1;

            for (int a = 0; a <= total; a++)
            {
                try
                {
                    comando.CommandText = "update dbati.resultado set tp_situacao = 'I' where   cd_arquivo like '" + dgvPaciente.Rows[a].Cells[0].Value.ToString() + "%'";
                    comando.ExecuteNonQuery();
                }
                catch (Exception ERRO) { }
            }
            Banco.FechaConexao();

            /* int li = dgvPaciente.CurrentRow.Index;
             li += 1;
             dgvPaciente.CurrentCell = dgvPaciente.Rows[li].Cells[0];
             dgvPaciente.Rows[li].Selected = true;*/

        }
        //PESQUISAR PACIENTE PELO FILTRO
        private void filtroPaciente(string pedido)
        {
            dgvResultado.DataSource = null;
            dgvAmostra.DataSource = null;
            OracleDataAdapter DA1 = new OracleDataAdapter();
            DataSet DSet1 = new DataSet(); DataSet DSetComp2 = new DataSet();
            try
            {
                DA1 = query.filtroAguardando(pedido);
                DA1.Fill(DSet1, "TDremessaMC");
                dgvPaciente.DataSource = DSet1.Tables[0];

            }
            catch (Exception erro)
            {
                // Erro msg = new Erro("ERRO AO FILTRAR PACIENTE.\n " + erro.Message);
                // msg.Show();                
            }
        }
        //PESQUISAR PACIENTE INTEGRADO
        private void filtrointegrados()
        {
            dgvResultado.DataSource = null;
            dgvAmostra.DataSource = null;
            OracleDataAdapter DA1 = new OracleDataAdapter();
            DataSet DSet1 = new DataSet(); DataSet DSetComp2 = new DataSet();
            try
            {
                //POPULA LABEL COM MC SUS
                DA1 = query.filtroIntegrados();
                DA1.Fill(DSet1, "TDremessaMC");
                dgvPaciente.DataSource = DSet1.Tables[0];
                //lbMCSUS.Text = DSetComp.Tables[0].Rows[0]["TOTAL"].ToString(); 
            }
            catch (Exception erro)
            {
                //Erro msg = new Erro("ERRO AO LISTAR PACIENTES INTEGRADOS.\n " + erro.Message);
                // msg.Show();           
            }
        }

        private void dgvResultado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //dgvResultado.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;            
        }
        //BOTAO  - INTEGRAR PEDIDO
        private void btIntergar_Click(object sender, EventArgs e)
        {
            try
            {
                insertResultado();
                snRealizado();
                versao();
                //Executado msg = new Executado("PACIENTE " + dgvPaciente.CurrentRow.Cells["NOME"].Value.ToString() + " INTEGRADO COM SUCESSO!");
                //msg.Show();
            }
            catch (Exception erro)
            {
                // Erro msg = new Erro("NAO FOI POSSIVEL INTEGRA PACIENTE " + dgvPaciente.CurrentRow.Cells["NOME"].Value.ToString() + "\nErro:" + erro.Message);
                //msg.Show();

            }
            consutarPaciente();
        }
        //ATUALIZAR CODIGO DA VERSAO
        private void versao()
        {
            if (Banco.conn.State == System.Data.ConnectionState.Open)
            {
                Banco.FechaConexao();
            }
            else
            {
                Banco.AbreConexao();
                comando.Connection = Banco.RetornaConexao();
                try
                {
                    int total = dgvResultado.Rows.Count;

                    for (Int32 i = 0; i < total; i++)
                    {

                    }

                    //GERAL              


                }
                catch (Exception erro)
                {
                    // Erro msg = new Erro("ERRO AO INSERIR A VERSAO DE EXAMES.\n " + erro.Message);
                    // msg.Show();
                }

            }
        }
        //ATUALIZAR STATUS APOS A EXECUCAO
        private void atualizarStatus()
        {
            if (Banco.conn.State == System.Data.ConnectionState.Open)
            {
                Banco.FechaConexao();
            }
            else
            {
                Banco.AbreConexao();
                comando.Connection = Banco.RetornaConexao();
                try
                {
                    //GERAL              
                    comando.CommandText = "update dbati.resultado set tp_situacao = 'I' where   cd_arquivo like '" + tbPedido.Text + "%'";
                    comando.ExecuteNonQuery();
                }
                catch (Exception erro)
                {
                    // Erro msg = new Erro("ERRO AO ATUALIZAR STATUS DO ARQUIVO.\n " + erro.Message);
                    // msg.Show();
                }
                Banco.FechaConexao();
            }
        }
        //STATUS REALIZADO
        private void snRealizado()
        {
            OracleCommand cmd = new OracleCommand();
            if (Banco.conn.State == System.Data.ConnectionState.Open)
            {
                Banco.FechaConexao();
            }
            else
            {

                if (dgvResultado.Rows.Count != 0)
                {
                    Banco.AbreConexao();
                    cmd.Connection = Banco.RetornaConexao();
                    Int32 i = 0;
                    try
                    {
                        int contar = dgvResultado.Rows.Count - 1;
                        for (i = 0; i < contar; i++)
                        {
                            cmd.CommandText = "update itped_lab set dt_laudo = sysdate, cd_versao = " + dgvResultado.Rows[i].Cells[2].Value.ToString() + " , cd_usuario_realizado = 'MATRIX', sn_realizado = 'S' where cd_ped_lab = " + dgvResultado.Rows[i].Cells[0].Value.ToString() + " AND itped_lab.cd_itped_lab IN (" + dgvResultado.Rows[i].Cells[6].Value.ToString() + ")  and cd_exa_lab in (" + dgvResultado.Rows[i].Cells[1].Value.ToString() + ")";
                            cmd.ExecuteNonQuery();
                        }
                        //GERAL              

                    }
                    catch (Exception erro) { Messagem("update itped_lab set dt_laudo = sysdate, cd_versao = " + dgvResultado.Rows[i].Cells[2].Value.ToString() + " , cd_usuario_realizado = 'MATRIX', sn_realizado = 'S' where cd_ped_lab = " + dgvResultado.Rows[i].Cells[0].Value.ToString() + " AND itped_lab.cd_itped_lab IN (" + dgvResultado.Rows[i].Cells[6].Value.ToString() + ")  and cd_exa_lab in (" + dgvResultado.Rows[i].Cells[1].Value.ToString() + ")", "ERRO_UPDATE:" + dgvResultado.Rows[i].Cells[0].Value.ToString()); }
                    //catch (Exception erro) { Erro msg = new Erro("ERRO ATUALIZAR STATUS DO PEDIDO(LAUDO).\n " + erro.Message);msg.Show();    }
                    Banco.FechaConexao();
                }
            }
        }
        //INSERIR RESULTADO
        private void insertResultado()
        {

            //GERAL              
            if (dgvResultado.Rows.Count != 0)
            {

                if (Banco.conn.State == System.Data.ConnectionState.Open)
                {
                    Banco.FechaConexao();
                }

              //  comando.CommandText = "alter session set nls_comp = linguistic;";
              //  comando.ExecuteNonQuery();
              //  comando.CommandText = "alter session set nls_sort = binary_ai;";
             //   comando.ExecuteNonQuery();
              
                Int32 i = 0;
                try
                {
                    Banco.AbreConexao();
                    comando.Connection = Banco.RetornaConexao();
                    int total = dgvResultado.Rows.Count - 1;

                    for (i = 0; i <= total; i++)
                    {
                        if (dgvResultado.Rows[i].Cells[4].Value.ToString() != "     ")
                        {

                            comando.CommandText = "INSERT INTO RES_EXA (CD_PED_LAB ,CD_EXA_LAB,CD_VERSAO ,NM_CAMPO  ,DS_RESULTADO,CD_ORDEM_PERGUNTA  ,CD_ITPED_LAB) values ( " + dgvResultado.Rows[i].Cells[0].Value.ToString() + " ," + dgvResultado.Rows[i].Cells[1].Value.ToString() + "," + dgvResultado.Rows[i].Cells[2].Value.ToString() + " ,'" + dgvResultado.Rows[i].Cells[3].Value.ToString() + "' ,'" + dgvResultado.Rows[i].Cells[4].Value.ToString() + "'," + dgvResultado.Rows[i].Cells[5].Value.ToString() + "  ," + dgvResultado.Rows[i].Cells[6].Value.ToString() + ")";
                            comando.ExecuteNonQuery();
                            comando.CommandText = "update itped_lab set  cd_versao = " + dgvResultado.Rows[i].Cells[2].Value.ToString() + " , cd_usuario_realizado = 'MATRIX' , SN_EMITIDO = NULL , SN_LAUDO_CADASTRADO = 'S' , SN_REALIZADO = 'S' , SN_REVISADO = NULL,SN_ASSINADO = NULL , TP_RESULTADO_EXA_LAB = 'F' , HR_LAUDO = SYSDATE , dt_laudo = to_date (sysdate,'dd/mm/yyyy') where cd_ped_lab = " + dgvResultado.Rows[i].Cells[0].Value.ToString() + " AND itped_lab.cd_itped_lab IN (" + dgvResultado.Rows[i].Cells[6].Value.ToString() + ")  and cd_exa_lab in (" + dgvResultado.Rows[i].Cells[1].Value.ToString() + ")";
                            comando.ExecuteNonQuery();
                        }
                    }

                    Banco.FechaConexao();
                }
                catch (Exception erro)
                {
                    Messagem("INSERT INTO RES_EXA (CD_PED_LAB ,CD_EXA_LAB,CD_VERSAO ,NM_CAMPO  ,DS_RESULTADO,CD_ORDEM_PERGUNTA  ,CD_ITPED_LAB) values ( " + dgvResultado.Rows[i].Cells[0].Value.ToString() + " ," + dgvResultado.Rows[i].Cells[1].Value.ToString() + "," + dgvResultado.Rows[i].Cells[2].Value.ToString() + " ,'" + dgvResultado.Rows[i].Cells[3].Value.ToString() + "' ,'" + dgvResultado.Rows[i].Cells[4].Value.ToString() + "'," + dgvResultado.Rows[i].Cells[5].Value.ToString() + "  ," + dgvResultado.Rows[i].Cells[6].Value.ToString() + ")", "ERRO_INSERIR:" + dgvResultado.Rows[i].Cells[0].Value.ToString());
                    comando.CommandText = "update dbati.resultado set tp_situacao = 'D' where cd_arquivo like '" + dgvResultado.Rows[i].Cells[0].Value.ToString() + "_" + dgvResultado.Rows[i].Cells[1].Value.ToString() + "%'";
                    comando.ExecuteNonQuery();
                }
                // catch (Exception erro) {   Erro msg = new Erro("ERRO AO INSERIR RESULTADO.\n " + erro.Message);   msg.Show();   }

            }
            atualizarStatus();
        }

        private void dgvPaciente_SelectionChanged(object sender, EventArgs e)
        {
            dgvResultado.DataSource = null;
            OracleDataAdapter DA2 = new OracleDataAdapter();
            DataSet DSet2 = new DataSet(); DataSet DSetComp2 = new DataSet();
            try
            {
                DA2 = query.amostra(dgvPaciente.CurrentRow.Cells["PEDIDO"].Value.ToString());
                DA2.Fill(DSet2, "TBAMOSTRA");
                dgvAmostra.DataSource = DSet2.Tables[0];

                tbNome.Text = dgvPaciente.CurrentRow.Cells["NOME"].Value.ToString();
                tbPedido.Text = dgvPaciente.CurrentRow.Cells["PEDIDO"].Value.ToString();
                tbPront.Text = dgvPaciente.CurrentRow.Cells["PRONT"].Value.ToString();
            }
            catch (Exception erro)
            {
                // Erro msg = new Erro("ERRO AO CARREGAR AMOSTRAR.\n " + erro.Message);
                //  msg.Show();             
            }

        }
        private void dgvAmostra_SelectionChanged(object sender, EventArgs e)
        {
            OracleDataAdapter DA3 = new OracleDataAdapter();
            DataSet DSet3 = new DataSet(); DataSet DSetComp2 = new DataSet();
            try
            {
                DA3 = query.resultado(dgvPaciente.CurrentRow.Cells["PEDIDO"].Value.ToString());
                DA3.Fill(DSet3, "TDremessaMC");
                dgvResultado.DataSource = DSet3.Tables[0];
            }
            catch (Exception erro)
            {
                // Erro msg = new Erro("ERRO AO CARREGAR AMOSTRAR.\n " + erro.Message);
                //  msg.Show();              
            }
            try
            {
                this.dgvResultado.Columns["ARQ_RESULTADO"].ReadOnly = true;
                this.dgvResultado.Columns["EXAME"].ReadOnly = true;
                this.dgvResultado.Columns["VERSAO"].ReadOnly = true;
                this.dgvResultado.Columns["ITPEDIDO"].ReadOnly = true;
                this.dgvResultado.Columns["ORDEM"].ReadOnly = true;
                this.dgvResultado.Columns["NM_CAMPO"].ReadOnly = true;

                this.dgvResultado.Columns["RESULTADO"].DefaultCellStyle.Format = "C2";

                dgvResultado.Columns[5].DefaultCellStyle.BackColor = Color.CadetBlue;
                dgvResultado.Columns[5].DefaultCellStyle.ForeColor = Color.Yellow;

                tbAmostra.Text = dgvAmostra.CurrentRow.Cells["AMOSTRA"].Value.ToString();
                tbInst.Text = dgvAmostra.CurrentRow.Cells["INSTRUMENTO"].Value.ToString();
                tbMaterial.Text = dgvAmostra.CurrentRow.Cells["MATERIAL"].Value.ToString();

                //REDIMENCIONAR COLUNAS
                foreach (DataGridViewColumn column in dgvPaciente.Columns)
                {
                    if (column.DataPropertyName == "LINHA")
                        column.Width = 50; //tamanho fixo da primeira coluna
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    if (column.DataPropertyName == "NOME")
                        column.Width = 300; //tamanho fixo da primeira coluna
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                foreach (DataGridViewColumn column in dgvAmostra.Columns)
                {
                    if (column.DataPropertyName == "LINHA")
                        column.Width = 50; //tamanho fixo da primeira coluna
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                foreach (DataGridViewColumn column in dgvResultado.Columns)
                {
                    if (column.DataPropertyName == "EXAME")
                        column.Width = 70; //tamanho fixo da primeira coluna
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception erro)
            {
                //Erro msg = new Erro("ERRO AO PERSONALIZAR DATAGRID AMOSTRA.\n " + erro.Message);
                // msg.Show();              
            }
        }
        private void dgvResultado_SelectionChanged(object sender, EventArgs e)
        {

            try
            {
                tbExame.Text = dgvResultado.CurrentRow.Cells["EXAME"].Value.ToString();
                tbVersao.Text = dgvResultado.CurrentRow.Cells["VERSAO"].Value.ToString();
                tbCampo.Text = dgvResultado.CurrentRow.Cells["NM_CAMPO"].Value.ToString();
                tbOrdem.Text = dgvResultado.CurrentRow.Cells["ORDEM"].Value.ToString();
                tbItem.Text = dgvResultado.CurrentRow.Cells["ITPEDIDO"].Value.ToString();
            }
            catch (Exception erro)
            {
                // Erro msg = new Erro("ERRO AO CARREGAR TEXTBOX (RESULTADO).\n " + erro.Message);
                //  msg.Show();                
            }
        }
        private void picboxSair_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void picboxMax_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            picboxMax.Visible = false;
            picboxRest.Visible = true;
        }
        private void picboxMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void picboxRest_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            picboxMax.Visible = true;
            picboxRest.Visible = false;
        }

        private void tbPesquisa_Enter(object sender, EventArgs e)
        {
            if (tbPesquisa.Text == "PEDIDO")
            {
                tbPesquisa.Text = "";
                tbPesquisa.ForeColor = Color.Red;
            }
        }

        private void tbPesquisa_Leave(object sender, EventArgs e)
        {
            if (tbPesquisa.Text == "PEDIDO")
            {
                tbPesquisa.Text = "";
                tbPesquisa.ForeColor = Color.Gray;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //dgvResultado.CurrentCell = dgvResultado.Rows[2].Cells[0];

            filtrointegrados();
            cbIntegrado.Checked = false;
        }

        private void barraTitle_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            consutarPaciente();
            try
            {
                insertResultado();
            }
            catch (Exception erro)
            {

            }
        }
    }
}
