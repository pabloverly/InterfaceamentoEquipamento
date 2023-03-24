using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;
using System.Data;
using System.IO;


namespace SADTRESULT
{
    public class Query
    {
        Conexao objConn = new Conexao();
        string textoConexao;
        DataSet DSet = new DataSet();
        public OracleConnection conn = new OracleConnection();
        public OracleDataAdapter paciente()
        {
            StreamReader banco = objConn.banco();
            textoConexao = banco.ReadToEnd();
            OracleDataAdapter da_MargContrib = new OracleDataAdapter();
            conn = new OracleConnection(textoConexao);
        //backup   OracleCommand sql = new OracleCommand(" select distinct(substr(cd_arquivo, 0, 6)) Pedido, substr(ds_resultado, 0, 2) Linha, substr(ds_resultado, 3, 12) Pront, substr(ds_resultado, 15, 50) nome, substr(ds_resultado, 65, 8) nasc, substr(ds_resultado, 73, 1) sexo, substr(ds_resultado, 74, 1) cor, substr(ds_resultado, 75, 174) reserv_paciente, substr(ds_resultado, 249, 2) dv_paciente from dbati.resultado where substr(ds_resultado, 0, 2) = 11 and tp_situacao = 'A' ", conn);
            OracleCommand sql = new OracleCommand("select distinct(substr(cd_arquivo, 0, 6)) Pedido, substr(ds_resultado, 0, 2) Linha, substr(ds_resultado, 3, 12) Pront, substr(ds_resultado, 15, 50) Nome, substr(ds_resultado, 65, 8) Nasc, substr(ds_resultado, 73, 1) Sexo, substr(ds_resultado, 74, 1) Cor from dbati.resultado where substr(ds_resultado, 0, 2) = 11 and tp_situacao = 'A' ", conn);
            da_MargContrib.SelectCommand = sql;
            return da_MargContrib;
        }
        public OracleDataAdapter filtroAguardando(string pedido)
        {
            StreamReader banco = objConn.banco();
            textoConexao = banco.ReadToEnd();
            OracleDataAdapter da_MargContrib = new OracleDataAdapter();
            conn = new OracleConnection(textoConexao);
            //backup   OracleCommand sql = new OracleCommand(" select distinct(substr(cd_arquivo, 0, 6)) Pedido, substr(ds_resultado, 0, 2) Linha, substr(ds_resultado, 3, 12) Pront, substr(ds_resultado, 15, 50) nome, substr(ds_resultado, 65, 8) nasc, substr(ds_resultado, 73, 1) sexo, substr(ds_resultado, 74, 1) cor, substr(ds_resultado, 75, 174) reserv_paciente, substr(ds_resultado, 249, 2) dv_paciente from dbati.resultado where substr(ds_resultado, 0, 2) = 11 and tp_situacao = 'A' ", conn);
            OracleCommand sql = new OracleCommand("select * from  (select distinct(substr(cd_arquivo, 0, 6)) Pedido, substr(ds_resultado, 0, 2) Linha, substr(ds_resultado, 3, 12) Pront, substr(ds_resultado, 15, 50) Nome, substr(ds_resultado, 65, 8) Nasc, substr(ds_resultado, 73, 1) Sexo, substr(ds_resultado, 74, 1) Cor from dbati.resultado where substr(ds_resultado, 0, 2) = 11 ) where pedido in ('"+pedido+"') ", conn);
            da_MargContrib.SelectCommand = sql;
            return da_MargContrib;
        }
        public OracleDataAdapter filtroIntegrados()
        {
            StreamReader banco = objConn.banco();
            textoConexao = banco.ReadToEnd();
            OracleDataAdapter da_MargContrib = new OracleDataAdapter();
            conn = new OracleConnection(textoConexao);
            //backup   OracleCommand sql = new OracleCommand(" select distinct(substr(cd_arquivo, 0, 6)) Pedido, substr(ds_resultado, 0, 2) Linha, substr(ds_resultado, 3, 12) Pront, substr(ds_resultado, 15, 50) nome, substr(ds_resultado, 65, 8) nasc, substr(ds_resultado, 73, 1) sexo, substr(ds_resultado, 74, 1) cor, substr(ds_resultado, 75, 174) reserv_paciente, substr(ds_resultado, 249, 2) dv_paciente from dbati.resultado where substr(ds_resultado, 0, 2) = 11 and tp_situacao = 'A' ", conn);
            OracleCommand sql = new OracleCommand("select * from  (select distinct(substr(cd_arquivo, 0, 6)) Pedido, substr(ds_resultado, 0, 2) Linha, substr(ds_resultado, 3, 12) Pront, substr(ds_resultado, 15, 50) Nome, substr(ds_resultado, 65, 8) Nasc, substr(ds_resultado, 73, 1) Sexo, substr(ds_resultado, 74, 1) Cor from dbati.resultado where substr(ds_resultado, 0, 2) = 11 and tp_situacao = 'I')", conn);
            da_MargContrib.SelectCommand = sql;
            return da_MargContrib;
        }
        public OracleDataAdapter amostra(string pedido)
        {
            StreamReader banco = objConn.banco();
            textoConexao = banco.ReadToEnd();
            OracleDataAdapter da_MargContrib = new OracleDataAdapter();
            conn = new OracleConnection(textoConexao);
           //backup OracleCommand sql = new OracleCommand("select * from (select distinct (substr(cd_arquivo, 0, 6)) arq_amostra, substr(ds_resultado, 1, 2) Linha, substr(ds_resultado, 3, 12) Amostra, substr(ds_resultado, 15, 7) reserv_amostra, substr(ds_resultado, 22, 7) diluicao, substr(ds_resultado, 29, 12) agrupamento, substr(ds_resultado, 41, 8) laboratorio, substr(ds_resultado, 49, 6) instrumento, substr(ds_resultado, 55, 12) registro, substr(ds_resultado, 67, 8) origem, substr(ds_resultado, 75, 8) material, substr(ds_resultado, 83, 6) rack, substr(ds_resultado, 89, 8) dt_coleta, substr(ds_resultado, 97, 80) obs, substr(ds_resultado, 177, 6) escaminho, substr(ds_resultado, 183, 66) reservado, substr(ds_resultado, 249, 2) dv_amostra from dbati.resultado where substr(ds_resultado, 0, 2) = 20) where arq_amostra in('" + pedido + "')", conn);
            OracleCommand sql = new OracleCommand("select * from (select distinct (substr(cd_arquivo, 0, 6)) arq_amostra, substr(ds_resultado, 1, 2) Linha, substr(ds_resultado, 3, 12) Amostra, substr(ds_resultado, 22, 7) diluicao, substr(ds_resultado, 49, 6) instrumento, substr(ds_resultado, 75, 8) material, substr(ds_resultado, 89, 8) dt_coleta, substr(ds_resultado, 97, 80) obs from dbati.resultado where substr(ds_resultado, 0, 2) = 20) where arq_amostra in('"+pedido+"')", conn);
            da_MargContrib.SelectCommand = sql;
            return da_MargContrib;
        }
        public OracleDataAdapter resultado(string pedido)
        {
            StreamReader banco = objConn.banco();
            textoConexao = banco.ReadToEnd();
            OracleDataAdapter da_MargContrib = new OracleDataAdapter();
            conn = new OracleConnection(textoConexao);
            OracleCommand sql = new OracleCommand("select arq_resultado,exame,versao,(select VAL_EXA.nm_campo from DBAMV.VAL_EXA  where cd_versao = versao and nr_ordem_de_pergunta = ordem)Nm_campo ,REPLACE (resultado, ' ', '') resultado,ORDEM,    (select distinct (i.cd_itped_lab) from itped_lab i where i.cd_ped_lab in (arq_resultado) and i.cd_exa_lab in (exame)) itpedido, tp_situacao from (select substr(cd_arquivo,0,6) arq_resultado,substr(ds_resultado,1,2) tipo_resultado,replace(lpad(replace(replace(substr(ds_resultado,3,8), '-1', ''), '-1', ''), 5, 0),' ','') exame,substr(ds_resultado,11,8) parametro,substr(ds_resultado,11,4) versao ,substr(ds_resultado,15,2) ORDEM,lpad(replace(replace(substr(ds_resultado,19,80), '.', ','), '.', ','), 61, 0)  resultado ,substr(ds_resultado,99,118) reserv_resultado,substr(ds_resultado,217,8) dt_resultado,substr(ds_resultado,225,4) hr_resultado,substr(ds_resultado,229,8) dt_liberado,substr(ds_resultado,237,4) hr_liberado,substr(ds_resultado,241,8) usuario,substr(ds_resultado,249,2) dv_resultado,  dbati.resultado.tp_situacao from dbati.resultado where  substr(ds_resultado,0,2) = 21 ) where arq_resultado in ('" + pedido + "') ORDER BY arq_resultado,exame,ordem ", conn);
           // OracleCommand sql = new OracleCommand(" select arq_resultado,exame,versao,(select VAL_EXA.nm_campo from DBAMV.VAL_EXA  where cd_versao = versao and nr_ordem_de_pergunta = ordem)Nm_campo ,resultado,ORDEM,    (select distinct (i.cd_itped_lab) from itped_lab i where i.cd_ped_lab in (arq_resultado) and i.cd_exa_lab in (exame)) itpedido, tp_situacao from (select substr(cd_arquivo,0,6) arq_resultado,substr(ds_resultado,1,2) tipo_resultado,replace(lpad(replace(replace(substr(ds_resultado,3,8), '-1', ''), '-1', ''), 5, 0),' ','') exame,substr(ds_resultado,11,8) parametro,substr(ds_resultado,11,4) versao ,substr(ds_resultado,15,2) ORDEM,lpad(replace(replace(substr(ds_resultado,19,80), '.', ','), '.', ','), 5, 0)  resultado ,substr(ds_resultado,99,118) reserv_resultado,substr(ds_resultado,217,8) dt_resultado,substr(ds_resultado,225,4) hr_resultado,substr(ds_resultado,229,8) dt_liberado,substr(ds_resultado,237,4) hr_liberado,substr(ds_resultado,241,8) usuario,substr(ds_resultado,249,2) dv_resultado,  dbati.resultado.tp_situacao from dbati.resultado where  substr(ds_resultado,0,2) = 21 ) where arq_resultado in ('" + pedido+ "') ORDER BY arq_resultado,exame,ordem ", conn);
            da_MargContrib.SelectCommand = sql;
            return da_MargContrib;
        }
        public OracleDataAdapter resultadoInsert(string pedido)
        {
            StreamReader banco = objConn.banco();
            textoConexao = banco.ReadToEnd();
            OracleDataAdapter da_MargContrib = new OracleDataAdapter();
            conn = new OracleConnection(textoConexao);
            OracleCommand sql = new OracleCommand("select 'insert into res_exa (cd_ped_lab,cd_exa_lab,cd_versao,nm_campo,ds_resultado,cd_ordem_pergunta,cd_itped_lab) values (' || arq_resultado || ',' || exame || ',' || versao || ',' || chr(39)||'' || nm_campo || chr(39)||'' || ',' || chr(39)||'' || resultado || chr(39)||'' || ',' || ordem || ',' || itpedido || '' || final resultado from (SELECT ');'final, arq_resultado, exame, versao, (SELECT val_exa.nm_campo FROM dbamv.val_exa WHERE cd_versao = versao AND nr_ordem_de_pergunta = ordem) nm_campo, resultado, ordem, (SELECT DISTINCT (i.cd_itped_lab) FROM itped_lab i WHERE i.cd_ped_lab IN (arq_resultado) AND i.cd_exa_lab IN (exame)) itpedido, tp_situacao FROM (SELECT SUBSTR (cd_arquivo, 0, 6) arq_resultado, SUBSTR (ds_resultado, 1, 2) tipo_resultado, REPLACE (LPAD (REPLACE (REPLACE (SUBSTR (ds_resultado, 3, 8), '-1', '' ), '-1', '' ), 5, 0 ), ' ', '' ) exame, SUBSTR (ds_resultado, 11, 8) parametro, SUBSTR (ds_resultado, 11, 4) versao, SUBSTR (ds_resultado, 15, 2) ordem, LPAD (REPLACE (REPLACE (SUBSTR (ds_resultado, 19, 80), '.', ',' ), '.', ',' ), 5, 0 ) resultado, SUBSTR (ds_resultado, 99, 118) reserv_resultado, SUBSTR (ds_resultado, 217, 8) dt_resultado, SUBSTR (ds_resultado, 225, 4) hr_resultado, SUBSTR (ds_resultado, 229, 8) dt_liberado, SUBSTR (ds_resultado, 237, 4) hr_liberado, SUBSTR (ds_resultado, 241, 8) usuario, SUBSTR (ds_resultado, 249, 2) dv_resultado, dbati.resultado.tp_situacao FROM dbati.resultado WHERE SUBSTR (ds_resultado, 0, 2) = 21) resultado WHERE arq_resultado IN ('" + pedido + "') ORDER BY arq_resultado, exame, ordem) ", conn);
            // OracleCommand sql = new OracleCommand(" select arq_resultado,exame,versao,(select VAL_EXA.nm_campo from DBAMV.VAL_EXA  where cd_versao = versao and nr_ordem_de_pergunta = ordem)Nm_campo ,resultado,ORDEM,    (select distinct (i.cd_itped_lab) from itped_lab i where i.cd_ped_lab in (arq_resultado) and i.cd_exa_lab in (exame)) itpedido, tp_situacao from (select substr(cd_arquivo,0,6) arq_resultado,substr(ds_resultado,1,2) tipo_resultado,replace(lpad(replace(replace(substr(ds_resultado,3,8), '-1', ''), '-1', ''), 5, 0),' ','') exame,substr(ds_resultado,11,8) parametro,substr(ds_resultado,11,4) versao ,substr(ds_resultado,15,2) ORDEM,lpad(replace(replace(substr(ds_resultado,19,80), '.', ','), '.', ','), 5, 0)  resultado ,substr(ds_resultado,99,118) reserv_resultado,substr(ds_resultado,217,8) dt_resultado,substr(ds_resultado,225,4) hr_resultado,substr(ds_resultado,229,8) dt_liberado,substr(ds_resultado,237,4) hr_liberado,substr(ds_resultado,241,8) usuario,substr(ds_resultado,249,2) dv_resultado,  dbati.resultado.tp_situacao from dbati.resultado where  substr(ds_resultado,0,2) = 21 ) where arq_resultado in ('" + pedido+ "') ORDER BY arq_resultado,exame,ordem ", conn);
            da_MargContrib.SelectCommand = sql;
            return da_MargContrib;
        }
        public OracleDataAdapter consultaInsert(string pedido,string item, string ordem)
        {
            StreamReader banco = objConn.banco();
            textoConexao = banco.ReadToEnd();
            OracleDataAdapter da_MargContrib = new OracleDataAdapter();
            conn = new OracleConnection(textoConexao);
            OracleCommand sql = new OracleCommand("SELECT * FROM RES_EXA    WHERE cd_ped_lab     IN("+pedido+")   AND     cd_itped_lab  IN ("+item+" )AND  cd_ordem_pergunta = "+ ordem +"", conn);
            da_MargContrib.SelectCommand = sql;
            return da_MargContrib;
        }
        public OracleDataAdapter pedido()
        {
            StreamReader banco = objConn.banco();
            textoConexao = banco.ReadToEnd();
            OracleDataAdapter da_MargContrib = new OracleDataAdapter();
            conn = new OracleConnection(textoConexao);
            //backup   OracleCommand sql = new OracleCommand(" select distinct(substr(cd_arquivo, 0, 6)) Pedido, substr(ds_resultado, 0, 2) Linha, substr(ds_resultado, 3, 12) Pront, substr(ds_resultado, 15, 50) nome, substr(ds_resultado, 65, 8) nasc, substr(ds_resultado, 73, 1) sexo, substr(ds_resultado, 74, 1) cor, substr(ds_resultado, 75, 174) reserv_paciente, substr(ds_resultado, 249, 2) dv_paciente from dbati.resultado where substr(ds_resultado, 0, 2) = 11 and tp_situacao = 'A' ", conn);
            OracleCommand sql = new OracleCommand("select distinct (substr (cd_arquivo,0,6)) pedido from DBATI.RESULTADO WHERE tp_situacao = 'A'", conn);
            da_MargContrib.SelectCommand = sql;
            return da_MargContrib;
        }
        public OracleDataAdapter consulta()
        {
            StreamReader banco = objConn.banco();
            textoConexao = banco.ReadToEnd();
            OracleDataAdapter da_MargContrib = new OracleDataAdapter();
            conn = new OracleConnection(textoConexao);
            //backup   OracleCommand sql = new OracleCommand(" select distinct(substr(cd_arquivo, 0, 6)) Pedido, substr(ds_resultado, 0, 2) Linha, substr(ds_resultado, 3, 12) Pront, substr(ds_resultado, 15, 50) nome, substr(ds_resultado, 65, 8) nasc, substr(ds_resultado, 73, 1) sexo, substr(ds_resultado, 74, 1) cor, substr(ds_resultado, 75, 174) reserv_paciente, substr(ds_resultado, 249, 2) dv_paciente from dbati.resultado where substr(ds_resultado, 0, 2) = 11 and tp_situacao = 'A' ", conn);
            OracleCommand sql = new OracleCommand("SELECT distinct ( substr(cd_arquivo,0,6)) pedido FROM dbati.resultado", conn);
            da_MargContrib.SelectCommand = sql;
            return da_MargContrib;
        
        }

        public OracleDataAdapter itped_lab(string item, string exame, string ordem)
        {
            StreamReader banco = objConn.banco();
            textoConexao = banco.ReadToEnd();
            OracleDataAdapter da_MargContrib = new OracleDataAdapter();
            conn = new OracleConnection(textoConexao);
            //backup   OracleCommand sql = new OracleCommand(" select distinct(substr(cd_arquivo, 0, 6)) Pedido, substr(ds_resultado, 0, 2) Linha, substr(ds_resultado, 3, 12) Pront, substr(ds_resultado, 15, 50) nome, substr(ds_resultado, 65, 8) nasc, substr(ds_resultado, 73, 1) sexo, substr(ds_resultado, 74, 1) cor, substr(ds_resultado, 75, 174) reserv_paciente, substr(ds_resultado, 249, 2) dv_paciente from dbati.resultado where substr(ds_resultado, 0, 2) = 11 and tp_situacao = 'A' ", conn);
            OracleCommand sql = new OracleCommand("select count(cd_itped_lab)total from res_exa where cd_itped_lab = "+item+"= and cd_exa_lab = "+exame+" and cd_ordem_pergunta = "+ordem+"", conn);
            da_MargContrib.SelectCommand = sql;
            return da_MargContrib;

        }
    }
}



