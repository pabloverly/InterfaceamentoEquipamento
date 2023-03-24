using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SADTRESULT
{
    public partial class Service1 : ServiceBase
    {
        Query query = new Query();
        Conexao Banco = new Conexao();
        OracleCommand comando = new OracleCommand();
        Resultado resultado = new Resultado();
        string pedido;
        OracleDataAdapter DAPedido = new OracleDataAdapter();
        DataSet DSetPedido = new DataSet();
        OracleDataAdapter DAResulado = new OracleDataAdapter();
        DataSet DSetResulado = new DataSet();

        OracleDataAdapter DAResuladoInsert = new OracleDataAdapter();
        DataSet DSetResuladoInsert = new DataSet();

        OracleDataAdapter DAInsert = new OracleDataAdapter();
        DataSet DSetInsert = new DataSet();
        Timer timer1;
        string nm_arquivo = "";
        string diretorio = "";
        String linha;
        int x = 1;
        DirectoryInfo drinfo = new DirectoryInfo(@"\\10.0.70.17\Sistema\Integra\Result\");
        // DirectoryInfo drinfo = new DirectoryInfo(@"\\192.168.1.4\Integra\Result\");       
        FileInfo[] Files;
        public Service1()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            Messagem("SERVICO RODANDO.:" + DateTime.Now.ToString(), "START");
            //lerArquivo();
            timer1 = new Timer(new TimerCallback(timer1_Tick), null, 1000, 1000);
        }
        protected override void OnStop()
        {
            Messagem("SERVICO PARADO " + DateTime.Now.ToString(), "STOP");
        }
        private void timer1_Tick(object sender)
        {
            try
            {
                DSetPedido.Clear();
                DSetPedido.Tables.Clear();
                DSetResulado.Clear();
                DSetResulado.Tables.Clear();
            }
            catch (Exception erro) { }

            try { buscarArquivo(); } catch { }
            try { popularPedido(); } catch { }
            try { statusArquivo(); } catch { }
            try { acertoStartus(); } catch { }
            try { popularResultado(); } catch { }
            try { insert(); } catch { }
            try { insert(); } catch { }
            try { insert(); } catch { }
            try { insert(); } catch { }

            Messagem("SERVICO ATUALIZADO.:" + DateTime.Now.ToString() + "/n   ", "TIME");
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
                catch (Exception erro)
                { //Messagem("Paramentros:" + nm_arquivo + " Motivo= " + erro.Message, "INSERT ARQUIVO=" + nm_arquivo);
                }

                finally { Banco.FechaConexao(); }
            }
        }
        private void popularPedido()
        {
            try
            {
                DAPedido = query.pedido();
                DAPedido.Fill(DSetPedido, "TBPAC");
            }
            catch (Exception ex)
            {
            }
        }
        private void statusArquivo()
        {
            if (Banco.conn.State == System.Data.ConnectionState.Open)
            {
                Banco.FechaConexao();
            }

            Banco.AbreConexao();
            Banco.RetornaConexao();
            try
            {
                for (int p = 0; p < DSetPedido.Tables[0].Rows.Count; p++)
                {

                    comando.CommandText = "update dbati.resultado set tp_situacao = 'I' where   cd_arquivo like '" + DSetResulado.Tables[0].Rows[p]["arq_resultado"].ToString() + "%'";
                    comando.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                // Messagem(DSetPedido.Tables[0].Rows.Count + " - " + ex.Message, "ERRO_CONSULTAR_RESULTADO");
            }
            Banco.FechaConexao();
        }
        private void popularResultado()
        {
            try
            {
                for (int r = 0; r < DSetPedido.Tables[0].Rows.Count; r++)
                {
                    DAResulado = query.resultado(DSetPedido.Tables[0].Rows[r]["PEDIDO"].ToString());
                    DAResulado.Fill(DSetResulado, "TBRESULTADO");
                }
            }
            catch (Exception erro) { }
            DSetPedido.Dispose();
            DSetPedido.Clear();
            DSetPedido.Tables.Clear();
        }
        private void insert()
        {

            if (Banco.conn.State == System.Data.ConnectionState.Open)
            {
                Banco.FechaConexao();
            }

            Banco.AbreConexao();
            Banco.RetornaConexao();

            for (int y = 0; y < DSetResulado.Tables[0].Rows.Count; y++)
            {
                if (DSetResulado.Tables[0].Rows[y]["RESULTADO"].ToString() != "     ")
                {
                    try
                    {
                        comando.CommandText = "INSERT INTO RES_EXA (CD_PED_LAB ,CD_EXA_LAB,CD_VERSAO ,NM_CAMPO  ,DS_RESULTADO,CD_ORDEM_PERGUNTA  ,CD_ITPED_LAB) values ( " + DSetResulado.Tables[0].Rows[y]["arq_resultado"].ToString() + " ," + DSetResulado.Tables[0].Rows[y]["exame"].ToString() + "," + DSetResulado.Tables[0].Rows[y]["versao"].ToString() + " ,'" + DSetResulado.Tables[0].Rows[y]["nm_campo"].ToString() + "' ,'" + DSetResulado.Tables[0].Rows[y]["resultado"].ToString() + "'," + DSetResulado.Tables[0].Rows[y]["ordem"].ToString() + "  ," + DSetResulado.Tables[0].Rows[y]["itpedido"].ToString() + ")";
                        comando.ExecuteNonQuery();
                        comando.CommandText = "update itped_lab  set  itped_lab.cd_versao =" + DSetResulado.Tables[0].Rows[y]["versao"].ToString() + " ,  cd_usuario_realizado = 'MATRIX', sn_realizado = 'S', dt_laudo = sysdate where itped_lab.cd_exa_lab in('" + DSetResulado.Tables[0].Rows[y]["exame"].ToString() + "') and itped_lab.cd_itped_lab in('" + DSetResulado.Tables[0].Rows[y]["itpedido"].ToString() + "') ";
                        comando.ExecuteNonQuery();
                    }
                    catch (Exception erro)
                    {
                        //  Messagem("Parametros: Arquivo=" + DSetResulado.Tables[0].Rows[y]["arq_resultado"].ToString() + " Exame=" + DSetResulado.Tables[0].Rows[y]["exame"].ToString() + " Versao=" + DSetResulado.Tables[0].Rows[y]["versao"].ToString() + " Campo=" + DSetResulado.Tables[0].Rows[y]["nm_campo"].ToString() + " Resultado=" + DSetResulado.Tables[0].Rows[y]["resultado"].ToString() + " Ordem=" + DSetResulado.Tables[0].Rows[y]["ordem"].ToString() + "  Cod_Item=" + DSetResulado.Tables[0].Rows[y]["itpedido"].ToString() + " ERRO=" + erro.Message, "INSERT/UPDATE=" + DSetResulado.Tables[0].Rows[y]["itpedido"].ToString());

                    }
                    finally { }
                }
            }

            Banco.FechaConexao();

            DSetResulado.Dispose();
            DSetResulado.Clear();
            DSetResulado.Tables.Clear();
        }
        private void acertoStartus()
        {
            Banco.AbreConexao();
            Banco.RetornaConexao();
            try
            {
                comando.CommandText = "update dbati.resultado set  tp_situacao ='I' where tp_situacao = 'A'";
                comando.ExecuteNonQuery();
            }
            catch (Exception erro) { }
            finally { }
            Banco.FechaConexao();
        }

    }
}

