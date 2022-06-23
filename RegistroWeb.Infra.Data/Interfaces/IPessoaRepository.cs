using RegistroWeb.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroWeb.Infra.Data.Interfaces
{
    public interface IPessoaRepository : IBaseRepository<Pessoa>
    {
        List<Pessoa> GetByNome(string nome, Guid idUsuario);
    }
}
