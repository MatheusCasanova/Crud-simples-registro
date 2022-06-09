using RegistroWeb.Infra.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Registro.Presentation.Models
{
    public class PessoaConsultaViewModel
    {
        [Required(ErrorMessage = "Por favor, informe o nome.")]
        public string? Nome { get; set; }

        //lista de eventos que será utilizado para exibir
        //na página o resulta da consulta feita no banco 
        public List<Pessoa>? Pessoas { get; set; }
    }
}
