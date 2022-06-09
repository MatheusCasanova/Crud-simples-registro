using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroWeb.Infra.Data.Entities
{
    public class Pessoa
    {
        #region Propriedades

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Sexo { get; set; }

        #endregion
    }
}
