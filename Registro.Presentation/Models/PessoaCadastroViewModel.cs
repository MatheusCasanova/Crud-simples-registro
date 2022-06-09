using System.ComponentModel.DataAnnotations;

namespace Registro.Presentation.Models
{
    /// <summary>
    /// classe de modelo de dados para a página de cadastro de pessoas
    /// </summary>
    public class PessoaCadastroViewModel
    {
        [MinLength(7, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(70, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome.")]
        public string Nome { get; set; }

        [MinLength(14, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(14, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o CPF.")]
        public string Cpf { get; set; }

        [MinLength(12, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(12, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o RG.")]
        public string Rg { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de nascimento.")]
        public string DataNascimento { get; set; }

        [Required(ErrorMessage = "Por favor, informe seu sexo.")]
        public int Sexo { get; set; }
    }
}
