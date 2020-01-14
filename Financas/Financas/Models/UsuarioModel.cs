using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Financas.Models
{
    public class UsuarioModel
    {
        [Required]
        public string Nome { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }

        [Compare("Senha")] //adicionar uma nova validação que vai comparar o campo Senha com o campo ConfirmacaoSenha, para isso utilizaremos uma nova anotação chamada CompareAttribute:
        public string ConfirmacaoSenha { get; set; }
    }
}