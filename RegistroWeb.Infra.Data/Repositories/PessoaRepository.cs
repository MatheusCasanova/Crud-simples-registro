using Dapper;
using RegistroWeb.Infra.Data.Entities;
using RegistroWeb.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroWeb.Infra.Data.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        //atributo
        private readonly string _connectionString;

        //método construtor utilizado para que possamos passar o valor da connectionstring para a classe de repositorio
        public PessoaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Create(Pessoa obj)
        {
            var query = @"
                INSERT INTO PESSOA(ID, NOME, CPF, RG, DATANASCIMENTO, SEXO, IDUSUARIO)
                VALUES(@Id, @Nome, @Cpf, @Rg, @DataNascimento, @Sexo, @IdUsuario)
            ";

            //conectando no banco de dados
            using (var connection = new SqlConnection(_connectionString))
            {
                //executando a gravação de pessoa na base de dados
                connection.Execute(query, obj);
            }
        }

        public void Update(Pessoa obj)
        {
            var quey = @"
                UPDATE PESSOA
                SET
                    NOME = @Nome,
                    CPF = @Cpf,
                    RG = @Rg,
                    DATANASCIMENTO = @DataNascimento,
                    SEXO = @Sexo
                WHERE
                    ID = @Id
            ";

            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(quey, obj);
            }
        }

        public void Delete(Pessoa obj)
        {
            var query = @"
                DELETE FROM PESSOA
                WHERE ID = @Id
            ";

            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, obj);
            }
        }

        public List<Pessoa> GetAll()
        {
            var query = @"
                SELECT * FROM PESSOA
                ORDER BY NOME ASC
            ";

            using(var connection = new SqlConnection(_connectionString))
            {
                return connection
                    .Query<Pessoa>(query)
                    .ToList();
            }
        }

        public Pessoa GetById(Guid id)
        {
            var query = @"
                SELECT * FROM PESSOA
                WHERE ID = @id
            ";

            using(var connection = new SqlConnection(_connectionString))
            {
                return connection
                    .Query<Pessoa>(query, new { id })
                    .FirstOrDefault();
            }
        }

        public List<Pessoa> GetByNome(string nome, Guid idUsuario)
        {
            var query = @"
                SELECT * FROM PESSOA
                WHERE NOME = @nome AND IDUSUARIO = @idUsuario
                ORDER BY NOME ASC
            ";

            using(var connection = new SqlConnection(_connectionString))
            {
                return connection
                    .Query<Pessoa>(query, new { nome, idUsuario })
                    .ToList();
            }
        }
    }
}
