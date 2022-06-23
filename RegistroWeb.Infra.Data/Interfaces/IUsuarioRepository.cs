using RegistroWeb.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroWeb.Infra.Data.Interfaces
{
    /// <summary>
    /// Interface de repositorio para a entidade usuario
    /// </summary>
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Usuario GetByEmail(string email);

        Usuario GetByEmailESenha(string email, string senha);
    }
}
