using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADTRESULT
{
    public class Resultado
    {
        //1.1
        private string Tipo_Linha;
        private string Prontuario;
        private string Nome;
        private string Data_Nasc;
        private string Sexo;
        private string Cor;
        private string Reservado;
        private string Dv;

        //1.1
        public string tipo_linha { set { Tipo_Linha = value; }   get { return Tipo_Linha; } }
        public string prontuario { set { Prontuario = value; } get { return Prontuario; } }
        public string nome { set { Nome = value; } get { return Nome; } }
        public string data_nsexoasc { set { Sexo = value; } get { return Sexo; } }
        public string cor { set { Cor = value; } get { return Cor; } }
        public string reservado { set { Reservado = value; } get { return Reservado; } }
        public string dv { set { Dv = value; } get { return Dv; } }

    }
}
