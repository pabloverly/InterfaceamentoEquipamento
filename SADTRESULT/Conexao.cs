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
    public class Conexao
    {
        public OracleConnection conn = new OracleConnection();

     
        public void AbreConexao()
        {
           
             conn.ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=))); User Id=; Password=";
                conn.Open();                        
        }

        public OracleConnection RetornaConexao()
        {
            return conn;
        }

        public void FechaConexao()
        {
            conn.Close();
        } 
    }  
}



