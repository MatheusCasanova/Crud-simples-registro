using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Registro.Presentation.Models;
using RegistroWeb.Infra.Data.Entities;
using RegistroWeb.Infra.Data.Interfaces;

namespace Registro.Presentation.Controllers
{
    [Authorize]
    public class RegistroController : Controller
    {
        //atributo
        private readonly IPessoaRepository _pessoaRepository;

        public RegistroController(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        public IActionResult Cadastro()
        {
            return View();
        }
        
        [HttpPost]// annotation que indica que o método será executado no submit
        public IActionResult Cadastro(PessoaCadastroViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //ler o usuário autenticado na sessão
                    var json = HttpContext.Session.GetString("usuario");
                    var usuario = JsonConvert.DeserializeObject<UserIdentityModel>(json);

                    var pessoa = new Pessoa
                    {
                        Id = Guid.NewGuid(),
                        Nome = model.Nome,
                        Cpf = model.Cpf,
                        Rg = model.Rg,
                        DataNascimento = Convert.ToDateTime(model.DataNascimento),
                        Sexo = Convert.ToInt32(model.Sexo),
                        IdUsuario = usuario.Id // foreign key
                    };

                    //gravando no banco de dados
                    _pessoaRepository.Create(pessoa);

                    TempData["MensagemSucesso"] = $"Pessoa {pessoa.Nome}, cadastrado(a) com sucesso.";
                    ModelState.Clear(); //limpando os campos do formulário
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }
            else
            {
                TempData["MensagemAlerta"] = "Ocorreram erros de validação no preenchimento do formulário.";
            }

            return View();
        }

        public IActionResult Consulta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Consulta(PessoaConsultaViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //ler o usuário autenticado em sessão
                    var json = HttpContext.Session.GetString("usuario");
                    var usuario = JsonConvert.DeserializeObject<UserIdentityModel>(json);

                    //realizando a consulta de pessoas
                    model.Pessoas = _pessoaRepository.GetByNome(model.Nome, usuario.Id);

                    //verificando se alguma pessoa foi encontrada
                    if(model.Pessoas.Count > 0)
                    {
                        TempData["MensagemSucesso"] = $"{model.Pessoas.Count} pessoa(s) obtida(s) para a pesquisa";
                    }
                    else
                    {
                        TempData["MensagemErro"] = "Nenhum registro encontrado. Verifique se o nome está digitado corretamente";
                    }
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }
            else
            {
                TempData["MensagemAlerta"] = "Ocorreram erros de validação no preenchimento do formulário.";
            }
            //voltando para a página
            return View(model);
        }

        public IActionResult Edicao(Guid id)
        {
            var model = new PessoaEdicaoViewModel();

            try
            {
                var pessoa = _pessoaRepository.GetById(id);

                model.Id = pessoa.Id;
                model.Nome = pessoa.Nome;
                model.Cpf = pessoa.Cpf;
                model.Rg = pessoa.Rg;
                model.DataNascimento = Convert.ToDateTime(pessoa.DataNascimento).ToString("yyyy-MM-dd");
                model.Sexo = pessoa.Sexo;
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Edicao(PessoaEdicaoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var pessoa = _pessoaRepository.GetById(model.Id);

                    //ler o usuário autenticado na sessão
                    var json = HttpContext.Session.GetString("usuario");
                    var usuario = JsonConvert.DeserializeObject<UserIdentityModel>(json);

                    pessoa.Nome = model.Nome;
                    pessoa.Cpf = model.Cpf;
                    pessoa.Rg = model.Rg;
                    pessoa.DataNascimento = Convert.ToDateTime(model.DataNascimento);
                    pessoa.Sexo = Convert.ToInt32(model.Sexo);
                    pessoa.IdUsuario = usuario.Id;

                    _pessoaRepository.Update(pessoa);

                    TempData["MensagemSucesso"] = "Dados de registro atualizado com sucesso";

                    return RedirectToAction("Consulta");
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }
            else
            {
                TempData["MensagemAlerta"] = "Ocorerram erros de validação no preenchimento do formulário.";
            }

            return View();
        }

        public IActionResult Exclusao(Guid id)
        {
            try
            {
                var pessoa = _pessoaRepository.GetById(id);

                _pessoaRepository.Delete(pessoa);

                TempData["MensagemSucesso"] = $"Registro de '{pessoa.Nome}', excluído(a) com sucesso.";
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return RedirectToAction("Consulta");
        }

        public IActionResult Relatorio()
        {
            return View();
        }
    }
}
